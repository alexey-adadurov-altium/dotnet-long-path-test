using Microsoft.Win32;
using static LongPathTest.Utils;

namespace LongPathTest
{
    public static class Win32RegistryHelper
    {
        public static void LogRegistryLongPathEnablingParameter()
        {
            const string LongPathRegKeyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem";
            const string LongPathRegValueName = @"LongPathsEnabled";
            var v = Registry.GetValue(LongPathRegKeyName, LongPathRegValueName, 0);

            Log($"    Registry value {LongPathRegKeyName}::{LongPathRegValueName} is '{v}'");
        }
    }
}