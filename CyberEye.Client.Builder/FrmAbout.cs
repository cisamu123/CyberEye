/* 
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberEye.Client.Builder
{
    public partial class FrmAbout : Form
    {
        private int i = 0;
        private string aboutText =
            "Project: CyberEye Telegram RAT" + Environment.NewLine +
            "Version: 1.0.2" + Environment.NewLine +
            "Coded & Modded by: Cisamu AKA cisamu123" + Environment.NewLine +
            "Telegram API: HTTP Requests" + Environment.NewLine +
            "Keylogger: Custom implementation based on .NET low-level hooks" + Environment.NewLine +
            "Screen Capture: .NET Graphics API" + Environment.NewLine +
            "Webcam Support: CommandCam" + Environment.NewLine +
            "File Manager: Remote file browsing and manipulation" + Environment.NewLine +
            "Special Thanks to: LimerBoy, njq8 ,open-source community and the creator of NJRat 0.7D Horror Edition 2022 for providing the open-source code that helped in the development of CyberEye." + Environment.NewLine +
            "GitHub: https://github.com/cisamu123/CyberEye" + Environment.NewLine +
            "Telegram Channel: https://t.me/Cisamu" + Environment.NewLine +
            "Telegram Contact: https://t.me/CodQu" + Environment.NewLine +
            "----------------------------------------------------------";
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
            label1.Text = string.Empty;
            timer1.Start();
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i < aboutText.Length)
            {
                label1.Text += aboutText[i];
                i++;
            }
            else
            {
                timer1.Stop();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (label1.ForeColor == Color.White)
                label1.ForeColor = Color.Yellow;
            else if (label1.ForeColor == Color.Yellow)
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.White;
        }
    }
}
