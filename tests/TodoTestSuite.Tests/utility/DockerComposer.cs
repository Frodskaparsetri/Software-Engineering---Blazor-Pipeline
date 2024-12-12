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
                Console.WriteLine($"Directory contents: {string.Join(", ", Directory.GetFiles(directory).Take(5))}");
            }
        }

        Console.WriteLine("Checking docker-compose location:");
        var dockerComposePath = "/usr/bin/docker-compose";
        Console.WriteLine($"docker-compose path: {dockerComposePath}");

        var workingDirectory = directory ?? Directory.GetCurrentDirectory();
        Console.WriteLine($"Starting process with working directory: {workingDirectory}");
        Console.WriteLine($"Command: {dockerComposePath} -f {pathToFile} up -d --wait --remove-orphans");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = dockerComposePath,
                Arguments = $"-f {pathToFile} up -d --wait --remove-orphans",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory
            }
        };

        process.Start();
        process.WaitForExit();
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
