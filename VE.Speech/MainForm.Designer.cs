namespace VE.Speech
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Speech = new System.Windows.Forms.Button();
            this.lb_Pitch = new System.Windows.Forms.Label();
            this.trackBar_Speed = new System.Windows.Forms.TrackBar();
            this.lb_Speed = new System.Windows.Forms.Label();
            this.trackBar_Pitch = new System.Windows.Forms.TrackBar();
            this.comboBox_VoiceType = new System.Windows.Forms.ComboBox();
            this.lb_VoiceType = new System.Windows.Forms.Label();
            this.btn_UpdateVoiceType = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_content = new System.Windows.Forms.TextBox();
            this.textBox_Log = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Pitch)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Speech
            // 
            this.btn_Speech.Location = new System.Drawing.Point(14, 153);
            this.btn_Speech.Name = "btn_Speech";
            this.btn_Speech.Size = new System.Drawing.Size(774, 67);
            this.btn_Speech.TabIndex = 0;
            this.btn_Speech.Text = "Speech";
            this.btn_Speech.UseVisualStyleBackColor = true;
            this.btn_Speech.Click += new System.EventHandler(this.btn_Speech_Click);
            // 
            // lb_Pitch
            // 
            this.lb_Pitch.AutoSize = true;
            this.lb_Pitch.Location = new System.Drawing.Point(12, 63);
            this.lb_Pitch.Name = "lb_Pitch";
            this.lb_Pitch.Size = new System.Drawing.Size(53, 12);
            this.lb_Pitch.TabIndex = 1;
            this.lb_Pitch.Text = "Pitch:10";
            // 
            // trackBar_Speed
            // 
            this.trackBar_Speed.Location = new System.Drawing.Point(71, 12);
            this.trackBar_Speed.Maximum = 20;
            this.trackBar_Speed.Minimum = 1;
            this.trackBar_Speed.Name = "trackBar_Speed";
            this.trackBar_Speed.Size = new System.Drawing.Size(291, 45);
            this.trackBar_Speed.TabIndex = 2;
            this.trackBar_Speed.Value = 10;
            this.trackBar_Speed.Scroll += new System.EventHandler(this.trackBar_Speed_Scroll);
            // 
            // lb_Speed
            // 
            this.lb_Speed.AutoSize = true;
            this.lb_Speed.Location = new System.Drawing.Point(12, 12);
            this.lb_Speed.Name = "lb_Speed";
            this.lb_Speed.Size = new System.Drawing.Size(53, 12);
            this.lb_Speed.TabIndex = 3;
            this.lb_Speed.Text = "Speed:10";
            // 
            // trackBar_Pitch
            // 
            this.trackBar_Pitch.Location = new System.Drawing.Point(71, 63);
            this.trackBar_Pitch.Maximum = 20;
            this.trackBar_Pitch.Minimum = 1;
            this.trackBar_Pitch.Name = "trackBar_Pitch";
            this.trackBar_Pitch.Size = new System.Drawing.Size(291, 45);
            this.trackBar_Pitch.TabIndex = 4;
            this.trackBar_Pitch.Value = 10;
            this.trackBar_Pitch.Scroll += new System.EventHandler(this.trackBar_Pitch_Scroll);
            // 
            // comboBox_VoiceType
            // 
            this.comboBox_VoiceType.FormattingEnabled = true;
            this.comboBox_VoiceType.Location = new System.Drawing.Point(77, 111);
            this.comboBox_VoiceType.Name = "comboBox_VoiceType";
            this.comboBox_VoiceType.Size = new System.Drawing.Size(121, 20);
            this.comboBox_VoiceType.TabIndex = 5;
            // 
            // lb_VoiceType
            // 
            this.lb_VoiceType.AutoSize = true;
            this.lb_VoiceType.Location = new System.Drawing.Point(12, 114);
            this.lb_VoiceType.Name = "lb_VoiceType";
            this.lb_VoiceType.Size = new System.Drawing.Size(59, 12);
            this.lb_VoiceType.TabIndex = 6;
            this.lb_VoiceType.Text = "VoiceType";
            // 
            // btn_UpdateVoiceType
            // 
            this.btn_UpdateVoiceType.Location = new System.Drawing.Point(204, 109);
            this.btn_UpdateVoiceType.Name = "btn_UpdateVoiceType";
            this.btn_UpdateVoiceType.Size = new System.Drawing.Size(62, 23);
            this.btn_UpdateVoiceType.TabIndex = 7;
            this.btn_UpdateVoiceType.Text = "Update";
            this.btn_UpdateVoiceType.UseVisualStyleBackColor = true;
            this.btn_UpdateVoiceType.Click += new System.EventHandler(this.btn_UpdateVoiceType_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Content";
            // 
            // textBox_content
            // 
            this.textBox_content.Location = new System.Drawing.Point(370, 27);
            this.textBox_content.Multiline = true;
            this.textBox_content.Name = "textBox_content";
            this.textBox_content.Size = new System.Drawing.Size(418, 105);
            this.textBox_content.TabIndex = 9;
            // 
            // textBox_Log
            // 
            this.textBox_Log.Location = new System.Drawing.Point(14, 226);
            this.textBox_Log.Multiline = true;
            this.textBox_Log.Name = "textBox_Log";
            this.textBox_Log.Size = new System.Drawing.Size(776, 212);
            this.textBox_Log.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox_Log);
            this.Controls.Add(this.textBox_content);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_UpdateVoiceType);
            this.Controls.Add(this.lb_VoiceType);
            this.Controls.Add(this.comboBox_VoiceType);
            this.Controls.Add(this.trackBar_Pitch);
            this.Controls.Add(this.lb_Speed);
            this.Controls.Add(this.trackBar_Speed);
            this.Controls.Add(this.lb_Pitch);
            this.Controls.Add(this.btn_Speech);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Pitch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Speech;
        private System.Windows.Forms.Label lb_Pitch;
        private System.Windows.Forms.TrackBar trackBar_Speed;
        private System.Windows.Forms.Label lb_Speed;
        private System.Windows.Forms.TrackBar trackBar_Pitch;
        private System.Windows.Forms.ComboBox comboBox_VoiceType;
        private System.Windows.Forms.Label lb_VoiceType;
        private System.Windows.Forms.Button btn_UpdateVoiceType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_content;
        private System.Windows.Forms.TextBox textBox_Log;
    }
}

