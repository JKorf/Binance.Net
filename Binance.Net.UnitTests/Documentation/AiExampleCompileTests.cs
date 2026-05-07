using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Binance.Net.UnitTests.Documentation
{
    [TestFixture]
    public class AiExampleCompileTests
    {
        [Test]
        public async Task AiFriendlyExamples_ShouldCompileAsConsolePrograms()
        {
            var repositoryRoot = GetRepositoryRoot();
            var examplesDirectory = Path.Combine(repositoryRoot, "Examples", "ai-friendly");
            var examples = Directory.GetFiles(examplesDirectory, "*.cs")
                .OrderBy(x => x)
                .ToArray();

            Assert.That(examples, Is.Not.Empty, "No AI-friendly examples found.");

            var outputRoot = Path.Combine(repositoryRoot, "obj", "ai-example-compile");
            if (Directory.Exists(outputRoot))
                Directory.Delete(outputRoot, true);

            Directory.CreateDirectory(outputRoot);

            var failures = new List<string>();
            foreach (var example in examples)
            {
                var exampleName = Path.GetFileNameWithoutExtension(example);
                var projectDirectory = Path.Combine(outputRoot, exampleName);
                Directory.CreateDirectory(projectDirectory);

                File.Copy(example, Path.Combine(projectDirectory, "Program.cs"));
                await File.WriteAllTextAsync(Path.Combine(projectDirectory, $"{exampleName}.csproj"), CreateProjectFile());

                var result = await RunDotnetBuildAsync(projectDirectory, $"{exampleName}.csproj");
                if (result.ExitCode != 0)
                {
                    failures.Add(
                        $"{Path.GetFileName(example)} failed to compile.{Environment.NewLine}" +
                        result.Output);
                }
            }

            Assert.That(failures, Is.Empty, string.Join(Environment.NewLine + Environment.NewLine, failures));
        }

        private static string CreateProjectFile()
        {
            var references = Directory.GetFiles(AppContext.BaseDirectory, "*.dll")
                .OrderBy(x => x)
                .Select(x =>
                    $"""    <Reference Include="{SecurityElement.Escape(Path.GetFileNameWithoutExtension(x))}" HintPath="{SecurityElement.Escape(x)}" />""");

            return $"""
                <Project Sdk="Microsoft.NET.Sdk">
                  <PropertyGroup>
                    <OutputType>Exe</OutputType>
                    <TargetFramework>net10.0</TargetFramework>
                    <ImplicitUsings>enable</ImplicitUsings>
                    <Nullable>enable</Nullable>
                    <IsPackable>false</IsPackable>
                  </PropertyGroup>
                  <ItemGroup>
                {string.Join(Environment.NewLine, references)}
                  </ItemGroup>
                </Project>
                """;
        }

        private static async Task<(int ExitCode, string Output)> RunDotnetBuildAsync(string workingDirectory, string projectFileName)
        {
            var output = new StringBuilder();
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            startInfo.ArgumentList.Add("build");
            startInfo.ArgumentList.Add(projectFileName);
            startInfo.ArgumentList.Add("--nologo");
            startInfo.ArgumentList.Add("/p:UseAppHost=false");
            startInfo.ArgumentList.Add("/p:OutDir=obj/build/");

            using var process = new Process { StartInfo = startInfo };
            process.OutputDataReceived += (_, args) =>
            {
                if (args.Data != null)
                    output.AppendLine(args.Data);
            };
            process.ErrorDataReceived += (_, args) =>
            {
                if (args.Data != null)
                    output.AppendLine(args.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(60));
            try
            {
                await process.WaitForExitAsync(timeout.Token);
            }
            catch (OperationCanceledException)
            {
                try
                {
                    process.Kill(true);
                }
                catch
                {
                    // Process already exited.
                }

                output.AppendLine("dotnet build timed out after 60 seconds.");
                return (-1, output.ToString());
            }

            return (process.ExitCode, output.ToString());
        }

        private static string GetRepositoryRoot()
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory != null)
            {
                if (File.Exists(Path.Combine(directory.FullName, "Binance.Net.sln")))
                    return directory.FullName;

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException("Could not find repository root containing Binance.Net.sln.");
        }
    }
}
