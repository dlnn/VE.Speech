using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Concentus.Oggfile;
using Concentus.Structs;
using Dlnn.ToolBox.Utils;
using NAudio.Wave;
using Newtonsoft.Json.Linq;

namespace VE.Speech
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private const string ossSavePath = @".\OggCache";
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(ossSavePath))
            {
                Directory.CreateDirectory(ossSavePath);
            }

            btn_UpdateVoiceType_Click(sender, e);

        }


        private void trackBar_Speed_Scroll(object sender, EventArgs e)
        {
            lb_Speed.Text = $"Speed:{trackBar_Speed.Value}";
        }

        private void trackBar_Pitch_Scroll(object sender, EventArgs e)
        {
            lb_Pitch.Text = $"Pitch:{trackBar_Pitch.Value}";
        }

        private async void btn_Speech_Click(object sender, EventArgs e)
        {
            btn_Speech.Enabled = false;
            try
            {
                var curSelectVoiceType = voiceTypeInfoList[comboBox_VoiceType.SelectedIndex];

                var oggResult = await Speech.Talk(textBox_content.Text, curSelectVoiceType.Id, curSelectVoiceType.Rate, trackBar_Speed.Value, trackBar_Pitch.Value);

                if (oggResult.status)
                {
                    var oggFileFullPath = Path.Combine(ossSavePath, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}");
                    File.WriteAllBytes($"{oggFileFullPath}.ogg", oggResult.oggBytes);
                    textBox_Log.AppendText($"[Output] => {oggFileFullPath}.ogg\r\n");


                    using (var fileIn = new MemoryStream(oggResult.oggBytes))
                    {
                        using (var pcmStream = new MemoryStream())
                        {
                            OpusDecoder decoder = OpusDecoder.Create(48000, 1);
                            OpusOggReadStream oggIn = new OpusOggReadStream(decoder, fileIn);
                            while (oggIn.HasNextPacket)
                            {
                                short[] packet = oggIn.DecodeNextPacket();
                                if (packet != null)
                                {
                                    for (int i = 0; i < packet.Length; i++)
                                    {
                                        var bytes = BitConverter.GetBytes(packet[i]);
                                        pcmStream.Write(bytes, 0, bytes.Length);
                                    }
                                }
                            }

                            pcmStream.Position = 0;
                            var wavStream = new RawSourceWaveStream(pcmStream, new WaveFormat(48000, 1));
                            var sampleProvider = wavStream.ToSampleProvider();

                            WaveFileWriter.CreateWaveFile16($"{oggFileFullPath}.wav", sampleProvider);
                            textBox_Log.AppendText($"[Output] => {oggFileFullPath}.wav\r\n");

                            var waveOut = new WaveOut();
                            var reader = new WaveFileReader(new FileStream($"{oggFileFullPath}.wav", FileMode.Open));
                            waveOut.Init(reader);
                            waveOut.Play();

                        }

                    }

                }
                else
                {
                    textBox_Log.AppendText($"[Error] => {oggResult.errMsg}\r\n");
                }
            }
            catch
            {

            }

            btn_Speech.Enabled = true;

        }

        private List<VoiceTypeInfo> voiceTypeInfoList = new List<VoiceTypeInfo>();
        private void btn_UpdateVoiceType_Click(object sender, EventArgs e)
        {
            var httpHelper = new HttpHelper();
            var httpItem = new HttpItem()
            {
                URL =
                    $"https://effect.snssdk.com/effect/api/v3/effects?access_key=0051d530508b11e9b441ed975323ebf8&device_id={Guid.NewGuid().ToString("N")}&device_type=x86_64&device_platform=windows&region=default&sdk_version=10.4.0&app_version=2.4.5&channel=jianyingpro_0&aid=1059&app_language=zh-hans&os_version=10.0.18363&platform_ab_params=0&panel=tone",
                Method = "GET",
            };

            var httpResult = httpHelper.GetHtml(httpItem);

            if (httpResult.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(httpResult.Html);
                voiceTypeInfoList.Clear();
                foreach (var effect in json["data"]["effects"].ToList())
                {
                    var tmpItem = new VoiceTypeInfo()
                    {
                        Name = effect["name"].ToString(),

                    };

                    var extra = effect["extra"].ToString();
                    tmpItem.Id = extra.Substring("\\\"voice_type\\\": \\\"", "\\\",");
                    tmpItem.Rate = int.Parse(extra.Substring("\\\"rate\\\": \\\"", "\\\","));
                    voiceTypeInfoList.Add(tmpItem);
                }

                comboBox_VoiceType.Items.Clear();
                foreach (var voiceTypeInfo in voiceTypeInfoList)
                {
                    comboBox_VoiceType.Items.Add(voiceTypeInfo.Name);
                }

                comboBox_VoiceType.SelectedIndex = 0;

            }


        }


    }

    public static class StringExtensions
    {
        public static string Substring(this string self, string left, string right)
        {
            int _leftLen = left.Length;
            int _left = self.IndexOf(left);
            if (_left >= 0)
            {
                int _right = self.IndexOf(right, _left + _leftLen);
                if (_right >= 0)
                {
                    return self.Substring(_left + _leftLen, _right - _left - _leftLen);
                }
            }
            return string.Empty;
        }
    }

    public class VoiceTypeInfo
    {
        public string Name;
        public string Id;
        public int Rate;
    }
}
