using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LongPathTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var pathBase = $@"C:\Net\Data\ARKService\{Guid.NewGuid()}";
                var veryLongPath = Path.Combine(pathBase, @"s\e\ockewyld__2018-11-16T21_17_34_UTC\Parts\Nordic Semiconductor\nRF52840 Development Kit - Hardware files 1_0_0\PCA10056-nRF52840 Development Board 1_0_0\Altium Designer files\pca10056_sheet1_cover.SchDoc.tmp");

                var root = Path.GetPathRoot(veryLongPath);

                Log($"This test will attempt to create a multiple enclosed directories with a long path starting at {root}");
                Log(@"You may need admin permissions to successfully complete the test.");
                Log("");
                LogLongPathConfigSwitches();
                Log("");
                WaitEnterKeyHit();

                Log($"Creating a directory with overall path length of {veryLongPath.Length} characters...");
                Directory.CreateDirectory(veryLongPath);
                Log($"    >>>> SUCCEEDED!!!    Remember to delete the directory!!!");

                Log($"Enumerating the content of {pathBase}...");
                GetTotalSize(new DirectoryInfo(pathBase));
                Log($"    >>>> SUCCEEDED!!!    ");
                
                RunWithoutManifest();
            }
            catch (Exception ex)
            {
                Log($"    >>>> FAILED!!!");
                Log();
                Log(ex.ToString());
                WaitEnterKeyHit();
            }
        }

        private static void RunWithoutManifest()
        {
            WaitEnterKeyHit("Next step: run the same tests using LongPathTest.NoManifest.exe (without win32 manifest).");
            Log("===========================================================");
            var p = new Process();

            p.StartInfo = new ProcessStartInfo(@"LongPathTest.NoManifest.exe")
            {
                UseShellExecute = false
            };

            p.Start();
            p.WaitForExit();
        }

        private static void WaitEnterKeyHit(string prompt = null)
        {
            Log("");
            if (! string.IsNullOrWhiteSpace(prompt))
            {
                Log(prompt);
            }
            Log("Press ENTER to continue...");
            Console.ReadLine();
            Log("");
        }

        private static void Log(string message = null)
        {
            Console.WriteLine(message);
        }

        private static void LogLongPathConfigSwitches()
        {
            void LogSwitchValue(string switchName)
            {
                if (AppContext.TryGetSwitch(switchName, out var switchValue))
                {
                    Log($"    AppContext switch {switchName} is set to '{switchValue}'");
                }
                else
                {
                    Log($"    AppContext switch {switchName} IS NOT SET");
                }
            }

            void LogRegistryLongPathEnablingParameter()
            {
                const string LongPathRegKeyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem";
                const string LongPathRegValueName = @"LongPathsEnabled";
                var v = Registry.GetValue(LongPathRegKeyName, LongPathRegValueName, 0);

                Log($"    Registry value {LongPathRegKeyName}::{LongPathRegValueName} is '{v}'");
            }

            Log($"YOUR SYSTEM CONFIGURATION OPTIONS RELATED TO LONG PATHS:");

            Log($"    CLR version: {Environment.Version}");
            LogSwitchValue("Switch.System.IO.UseLegacyPathHandling");
            LogSwitchValue("Switch.System.IO.BlockLongPaths");
            LogRegistryLongPathEnablingParameter();
        }

        public static long GetTotalSize(DirectoryInfo rootFolder)
        {
            var totalSize = rootFolder.EnumerateFiles().Sum(file => file.Length);
            totalSize += rootFolder.EnumerateDirectories().Sum(subfolder => GetTotalSize(subfolder));
            return totalSize;
        }
    }
}
