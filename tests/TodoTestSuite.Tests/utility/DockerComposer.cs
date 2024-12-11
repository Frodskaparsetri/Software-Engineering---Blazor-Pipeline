namespace TodoTestSuite.Tests.utility;

public class DockerComposer
{
  public static void Up(string pathToFile = "./docker-compose.yml")
  {
    var process = new System.Diagnostics.Process
    {
      StartInfo = new System.Diagnostics.ProcessStartInfo
      {
        FileName = "docker-compose",
        Arguments = $"-f {pathToFile} up -d --wait --remove-orphans",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false
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

  public static void Down(string pathToFile = "./docker-compose.yml")
  {
    var process = new System.Diagnostics.Process
    {
      StartInfo = new System.Diagnostics.ProcessStartInfo
      {
        FileName = "docker-compose",
        Arguments = $"-f {pathToFile} down --remove-orphans",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false
      }
    };

    process.Start();
    process.WaitForExit();
  }
}
