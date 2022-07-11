using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using GlobExpressions;
using static Bullseye.Targets;
using static SimpleExec.Command;

const string Clean = "clean";
const string Format = "format";
const string Build = "build";
const string Test = "test";
const string Publish = "publish";

Target(
    Clean,
    ForEach("**/bin", "**/obj"),
    dir =>
    {
        IEnumerable<string> GetDirectories(string d)
        {
            return Glob.Directories(".", d);
        }

        void RemoveDirectory(string d)
        {
            if (Directory.Exists(d))
            {
                Console.WriteLine(d);
                Directory.Delete(d, true);
            }
        }

        foreach (var d in GetDirectories(dir))
        {
            RemoveDirectory(d);
        }
    }
);

Target(
    Format,
    () =>
    {
        Run("dotnet", "tool restore", ".");
        Run("dotnet", "csharpier --check .", ".");
    }
);

Target(
    Build,
    DependsOn(Format),
    () =>
    {
        Run("dotnet", "build src/SparkPost.sln -c Release");
    }
);

Target(
    Test,
    DependsOn(Build),
    () =>
    {
        Run("dotnet", "test src/SparkPost.sln --no-restore --no-build");
    }
);

Target(
    Publish,
    DependsOn(Test),
    () =>
    {
        Run("dotnet", "pack src/SparkPost/SparkPost.csproj -c Release -o artifacts/");
    }
);

Target("default", DependsOn(Publish), () => Console.WriteLine("Done!"));

await RunTargetsAndExitAsync(args);
