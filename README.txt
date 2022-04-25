WHAT IS THIS PROJECT ALL ABOUT?

This project demonstrates several steps needed to support long paths on .NET Framework 4.8 
running on Windows. Specifically:

- successful - when the demo app includes the required application manifest items
  (see https://docs.microsoft.com/en-us/windows/win32/fileio/maximum-file-path-limitation)

- unsuccessful - when the app doesn't use these switches

- successful without a manifest and switches (for an app targeting .NET Core 3.1)

Notice that .NET 4.8 app generally doesn't need to care about setting the AppContext switches
that were relevant for .NET 4.5.2 (see https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/retargeting/4.5.2-4.8)


HOW TO RUN THIS PROJECT

1. Please only use Debug configuration.

2. Build and run LongPathTest.Runner -- it will run the executors for .NET 4.8 
   (with and without application manifest) and .NET Core 3.1 (with .NET Core no manifest is needed).

3. When using on a non-dev system, make sure to copy these files:

   LongPathTest.Runner\bin\Debug\LongPathTest.Runner.exe
   LongPathTest.Runner\bin\Debug\LongPathTest.Runner.exe.config
   LongPathTest.Runner\bin\Debug\executors\*.*

4. System requirements: 
 - .NET 4.8, .NET Core 3.1;
 - long paths enabled on FS level (the registry value mentioned in the Microsoft docs (see link) is set to 1)

Thank you!
