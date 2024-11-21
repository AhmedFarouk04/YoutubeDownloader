using System.Diagnostics;

public class FfmpegHelper
{
    public async Task MergeStreamsAsync(string videoPath, string audioPath, string outputPath)
    {
        var process = new Process();
        process.StartInfo.FileName = "ffmpeg";
        process.StartInfo.Arguments = $"-i \"{videoPath}\" -i \"{audioPath}\" -c:v copy -c:a aac \"{outputPath}\"";
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        string ffmpegOutput = await process.StandardError.ReadToEndAsync();
        process.WaitForExit();

        if (process.ExitCode == 0)
        {
            Console.WriteLine($"Merged file created successfully at: {outputPath}");
        }
        else
        {
            Console.WriteLine($"FFmpeg error: {ffmpegOutput}");
        }

        // Clean up temporary files
        if (File.Exists(videoPath)) File.Delete(videoPath);
        if (File.Exists(audioPath)) File.Delete(audioPath);
    }
}

