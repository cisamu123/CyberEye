/*
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
       ! WARNING ! This function will work in the next version of the RAT.
*/
/*
using System.Windows.Forms;
namespace TelegramRAT.modded
{
    internal sealed class CustomMsgBox
    {
        // All data must be in base64 format, for example, the message title and other details. Additionally, the size of the message box should be larger than the image.
        // Converting from base64 to text.
        // The sound should be in WAV file format. You can convert it to this file type using the website https://audio.online-convert.com/convert-to-wav.
        public static void CustomMessageBox(string message, string title, int x, int y, int width, int height, bool isUserWantsImage, string photoLink, System.Drawing.Color backgroundColor)
        {
            Form form = new Form();
            form.Text = title;
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new System.Drawing.Point(x, y);
            form.Size = new System.Drawing.Size(width, height);

            if (isUserWantsImage)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.ImageLocation = photoLink;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Dock = DockStyle.Fill;

                form.Controls.Add(pictureBox);
            }
            else
            {
                form.BackColor = backgroundColor;
            }

            Label label = new Label();
            label.Text = message;
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(10, 10);

            Button button = new Button();
            button.Text = "OK";
            button.Location = new System.Drawing.Point(10, 40);
            button.Click += (sender, e) =>
            {
                form.Close();
            };

            form.Controls.Add(label);
            form.Controls.Add(button);

            Application.Run(form);
        }
    }
}
*/