using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VE.Speech
{
    public class Speech
    {
        private const string url = @"wss://speech.bytedance.com/api/v1/tts/ws_binary";

        public static async Task<(bool status, byte[] oggBytes, string errMsg)> Talk(string content, string type, int rate, int spped = 10, int pitch = 10)
        {
            var ttsPacket = new TTSRequestPacket()
            {
                app = new App()
                {
                    appid = "JianYingPro",
                    token = "access_token",
                    cluster = "videocut_cpu",
                    backend_cluster = "videocut_cpu",
                },
                user = new User()
                {

                    uid = Guid.NewGuid().ToString("N"),
                },
                audio = new Audio()
                {
                    voice = "other",
                    voice_type = type,
                    speed = spped,
                    volume = 10,
                    pitch = pitch,
                    rate = rate,
                    encoding = "ogg_opus",
                    compression_rate = 10,
                },
                request = new Request()
                {
                    reqid = Guid.NewGuid().ToString("N"),
                    workflow = "synthesize,codec",
                    text = content,
                    text_type = "plain",
                    operation = "submit",
                    concurrency = 0,
                    business = ""
                },

            };

            var jsonBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(ttsPacket));

            var cln = new ClientWebSocket();
            cln.ConnectAsync(new Uri(url), new CancellationToken()).Wait();


            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(Endian.SwapInt32(0x11101000));
                    bw.Write(Endian.SwapInt32(jsonBytes.Length));
                    bw.Write(jsonBytes);

                    await cln.SendAsync(new ArraySegment<byte>(ms.ToArray()), WebSocketMessageType.Binary, true, new CancellationToken());

                }
            }



            var packDict = new Dictionary<int, RecvPacket>();
            var bytesReceived = new ArraySegment<byte>(new byte[1024 * 1024 * 10]);
            var isError = false;
            var errMsg = string.Empty;
            while (true)
            {
                var result = await cln.ReceiveAsync(bytesReceived, CancellationToken.None);


                //粘包问题没解决自己看着办
                var pack = RecvPacket.Parse(bytesReceived, result.Count);

                if (pack == null)
                    continue;



                if (pack.contentSize > 0 && pack.seq != 0 && pack.type == 0xB0)
                {
                    packDict.Add(pack.seq, pack);
                }

                if (pack.isEnd || pack.type != 0xB0)
                {
                    await cln.CloseAsync((WebSocketCloseStatus)4998, "", new CancellationToken());
                    isError = pack.type != 0xB0;
                    JObject json = null;
                    switch (pack.type)
                    {
                        case 0xF0:
                            Console.WriteLine(Encoding.UTF8.GetString(pack.content));
                            json = JObject.Parse(Encoding.UTF8.GetString(pack.content));
                            errMsg = json["error"].ToString();

                            break;

                    }

                    break;
                }

            }

            if (isError)
            {
                return (false, null, errMsg);
            }

            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    var keys = packDict.Keys.ToList();
                    keys.Sort();
                    foreach (var k in keys)
                    {
                        bw.Write(packDict[k].content);
                    }

                    return (true, ms.ToArray(), "Succ");
                }
            }


        }

    }

    public class RecvPacket
    {
        public int op;
        public int type;
        public int seq;
        public int contentSize;
        public byte[] content;
        public bool isEnd;

        public static RecvPacket Parse(ArraySegment<byte> bytes, int count)
        {
            if (count < 8 && bytes.Array[0] == 0x11)
            {
                return null;
            }

            var pack = new RecvPacket();

            using (var ms = new MemoryStream(bytes.Array, 0, count))
            {
                using (var br = new BinaryReader(ms))
                {
                    pack.op = Endian.SwapInt32(br.ReadInt32());
                    pack.seq = Endian.SwapInt32(br.ReadInt32());

                    pack.type = (pack.op >> 16) & 0xF0;


                    if (pack.op == 0x11B30000)
                    {
                        pack.isEnd = true;
                        pack.seq = Math.Abs(pack.seq);
                    }

                    if (ms.Position != ms.Length)
                    {
                        pack.contentSize = Endian.SwapInt32(br.ReadInt32());
                        pack.content = br.ReadBytes(pack.contentSize);
                    }
                }
            }


            return pack;
        }

    }

    public static class Endian
    {
        public static short SwapInt16(this short n)
        {
            return (short)(((n & 0xff) << 8) | ((n >> 8) & 0xff));
        }

        public static ushort SwapUInt16(this ushort n)
        {
            return (ushort)(((n & 0xff) << 8) | ((n >> 8) & 0xff));
        }

        public static int SwapInt32(this int n)
        {
            return (int)(((SwapInt16((short)n) & 0xffff) << 0x10) |
                         (SwapInt16((short)(n >> 0x10)) & 0xffff));
        }

        public static uint SwapUInt32(this uint n)
        {
            return (uint)(((SwapUInt16((ushort)n) & 0xffff) << 0x10) |
                          (SwapUInt16((ushort)(n >> 0x10)) & 0xffff));
        }

        public static long SwapInt64(this long n)
        {
            return (long)(((SwapInt32((int)n) & 0xffffffffL) << 0x20) |
                          (SwapInt32((int)(n >> 0x20)) & 0xffffffffL));
        }

        public static ulong SwapUInt64(this ulong n)
        {
            return (ulong)(((SwapUInt32((uint)n) & 0xffffffffL) << 0x20) |
                           (SwapUInt32((uint)(n >> 0x20)) & 0xffffffffL));
        }
    }

    //如果好用，请收藏地址，帮忙分享。
    public class App
    {
        /// <summary>
        /// 
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cluster { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backend_cluster { get; set; }
    }

    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public string uid { get; set; }
    }

    public class Audio
    {
        /// <summary>
        /// 
        /// </summary>
        public string voice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string voice_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int speed { get; set; }
        /// <summary>
        /// 音量
        /// </summary>
        public int volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pitch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encoding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int compression_rate { get; set; }
    }

    public class Request
    {
        /// <summary>
        /// 
        /// </summary>
        public string reqid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string workflow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string text_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int concurrency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string business { get; set; }
    }

    public class TTSRequestPacket
    {
        /// <summary>
        /// 
        /// </summary>
        public App app { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Audio audio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Request request { get; set; }
    }
}
