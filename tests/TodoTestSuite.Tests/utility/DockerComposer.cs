namespace TodoTestSuite.Tests.utility;
using System.IO;

public class DockerComposer
{
    public static void Up(string pathToFile)
    {
        var workingDirectory = Path.GetDirectoryName(pathToFile);

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

        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new Exception($"Docker compose failed: {error}");
        }
    }

    public static void Down(string pathToFile)
    {
        var workingDirectory = Path.GetDirectoryName(pathToFile);

        var process = new System.Diagnostics.Process
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "docker-compose",
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
