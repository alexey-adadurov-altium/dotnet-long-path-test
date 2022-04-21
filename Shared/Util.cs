using System;

namespace LongPathTest
{
    public static class Utils
    {
        public static void WaitEnterKeyHit(string prompt = null)
        {
            Log("");
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                Log(prompt);
            }
            Log("Press ENTER to continue...");
            Console.ReadLine();
            Log("");
        }

        public static void Log(string message = null)
        {
            Console.WriteLine(message);
        }
    }
}