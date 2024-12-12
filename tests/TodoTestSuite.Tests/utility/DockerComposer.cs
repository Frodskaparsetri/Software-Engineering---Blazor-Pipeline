namespace TodoTestSuite.Tests.utility;
using System.IO;
using System.Diagnostics;

public class DockerComposer
{
    public static void Up(string pathToFile)
    {
        Console.WriteLine($"Looking for docker-compose file at: {pathToFile}");
        Console.WriteLine($"File exists: {File.Exists(pathToFile)}");

        var directory = Path.GetDirectoryName(pathToFile);
        if (directory != null)
        {
            Console.WriteLine($"Directory: {directory}");
            Console.WriteLine($"Directory exists: {Directory.Exists(directory)}");
            if (Directory.Exists(directory))
            {
                Console.WriteLine($"Directory contents: {string.Join(", ", Directory.GetFiles(directory))}");
            }
        }

        var workingDirectory = directory ?? throw new InvalidOperationException("Directory path is null");

        var process = new System.Diagnostics.Process
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "docker-compose",
                Arguments = $"-f {pathToFile} up -d --wait --remove-orphans",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory
            }
        };

        Console.WriteLine($"Starting process with working directory: {workingDirectory}");
        Console.WriteLine($"Command: docker-compose {process.StartInfo.Arguments}");

        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        Console.WriteLine($"Process output: {output}");
        Console.WriteLine($"Process error: {error}");

        if (process.ExitCode != 0)
        {
            throw new Exception($"Docker compose failed: {error}\nOutput: {output}");
        }
    }

    public static void Down(string pathToFile)
    {
        var directory = Path.GetDirectoryName(pathToFile);
        var workingDirectory = directory ?? Directory.GetCurrentDirectory();

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/usr/bin/docker-compose",
                Arguments = $"-f {pathToFile} down --remove-orphans",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory
            }
        };

        process.Start();
        process.WaitForExit();
    }
}
