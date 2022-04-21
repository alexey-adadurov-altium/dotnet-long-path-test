namespace LongPathTest
{
    public static class Test
    {
        static void Execute()
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
    }
}
