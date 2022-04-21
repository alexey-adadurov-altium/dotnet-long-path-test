using System;
using System.Diagnostics;
using static LongPathTest.Utils;

namespace LongPathTest.Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TryRun(".NET Framework 4.8 app with manifest", "LongPathTest.Net48.WithManifest.exe");

            TryRun(".NET Framework 4.8 app WITHOUT manifest", "LongPathTest.Net48.NoManifest.exe");

            TryRun(".NET Core 3.1 app (WITHOUT manifest)", "LongPathTest.NetCore31.exe");

            WaitEnterKeyHit("All tests completed. Please examine the output!");
        }

        private static void TryRun(string appDescription, string executableName)
        {
            try
            {
                WaitEnterKeyHit($"Running long path test using {appDescription} - {executableName}");
                Log("===========================================================");
                var p = new Process();

                p.StartInfo = new ProcessStartInfo(@"executors\" + executableName)
                {
                    UseShellExecute = false
                };

                p.Start();
                p.WaitForExit();

                Log($"Exit code is:  {p.ExitCode}");
            }
            catch (Exception ex)
            {
                Log($"Failed to run test with {appDescription} - {executableName}.");
                Log("Error: " + ex.GetType().Name);
                Log(ex.Message);
            }
        }
    }
}
