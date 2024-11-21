using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

public class VideoDownloader
{
    private readonly YoutubeClient _youtubeClient;

    public VideoDownloader(YoutubeClient youtubeClient)
    {
        _youtubeClient = youtubeClient;
    }

    public async Task DownloadAsync(string videoUrl, string formatChoice, string downloadPath, string customFileName)
    {
        var video = await _youtubeClient.Videos.GetAsync(videoUrl);
        var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(videoUrl);

        if (formatChoice == "1") // Audio only
        {
            // Display available audio streams for selection
            var audioStreams = streamManifest.GetAudioOnlyStreams().ToArray();
            Console.WriteLine("Available audio streams:");
            for (int i = 0; i < audioStreams.Length; i++)
            {
                Console.WriteLine($"{i + 1}. Bitrate: {audioStreams[i].Bitrate.KiloBitsPerSecond} kbps, Format: {audioStreams[i].Container}");
            }
            Console.WriteLine("Choose an audio quality (enter the number):");
            int audioChoice = int.Parse(Console.ReadLine()) - 1;

            // Get selected audio quality
            string audioQuality = $"{audioStreams[audioChoice].Bitrate.KiloBitsPerSecond} kbps";

            // Create file name with quality information
            string finalFileName = FileNameGenerator.GenerateFileName(video.Title, customFileName, "", audioQuality);
            string fullFilePath = Path.Combine(downloadPath, $"{finalFileName}.mp3");

            // Check for existing files
            if (File.Exists(fullFilePath))
            {
                var fileHandler = new FileHandler();
                fullFilePath = fileHandler.HandleExistingFile(fullFilePath);
            }

            // Download audio stream
            var audioStream = audioStreams[audioChoice];
            await DownloadAudioOnly(audioStream, fullFilePath);
        }
        else // Video and audio
        {
            // Display available video streams for selection
            var videoStreams = streamManifest.GetVideoOnlyStreams().ToArray();
            Console.WriteLine("Available video streams:");
            for (int i = 0; i < videoStreams.Length; i++)
            {
                Console.WriteLine($"{i + 1}. Resolution: {videoStreams[i].VideoQuality.Label}, Bitrate: {videoStreams[i].Bitrate.KiloBitsPerSecond} kbps, Format: {videoStreams[i].Container}");
            }
            Console.WriteLine("Choose a video quality (enter the number):");
            int videoChoice = int.Parse(Console.ReadLine()) - 1;

            // Display available audio streams for selection
            var audioStreams = streamManifest.GetAudioOnlyStreams().ToArray();
            Console.WriteLine("Available audio streams:");
            for (int i = 0; i < audioStreams.Length; i++)
            {
                Console.WriteLine($"{i + 1}. Bitrate: {audioStreams[i].Bitrate.KiloBitsPerSecond} kbps, Format: {audioStreams[i].Container}");
            }
            Console.WriteLine("Choose an audio quality (enter the number):");
            int audioChoice = int.Parse(Console.ReadLine()) - 1;

            // Get selected quality labels
            string videoQuality = videoStreams[videoChoice].VideoQuality.Label;
            string audioQuality = $"{audioStreams[audioChoice].Bitrate.KiloBitsPerSecond} kbps";

            // Create file name with quality information
            string finalFileName = FileNameGenerator.GenerateFileName(video.Title, customFileName, videoQuality, audioQuality);
            string fileExtension = "mp4"; // As it's video + audio
            string fullFilePath = Path.Combine(downloadPath, $"{finalFileName}.{fileExtension}");

            // Check for existing files
            if (File.Exists(fullFilePath))
            {
                var fileHandler = new FileHandler();
                fullFilePath = fileHandler.HandleExistingFile(fullFilePath);
            }

            // Download video and audio streams
            var videoStream = videoStreams[videoChoice];
            var audioStream = audioStreams[audioChoice];

            await DownloadVideoWithAudio(videoStream, audioStream, fullFilePath);
        }
    }
    private int GetStreamChoice(int maxOption, string type)
    {
        while (true)
        {
            Console.WriteLine($"Choose a {type} (enter the number):");
            string? input = Console.ReadLine(); // Use a nullable string for user input

            // Check for null or empty input
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be null or empty. Please try again.");
                continue; // Prompt the user again
            }

            // Try to parse the input and check if it's within the valid range
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= maxOption)
            {
                return choice - 1; // Convert to 0-based index
            }

            Console.WriteLine($"Invalid choice. Please enter a number between 1 and {maxOption}.");
        }
    }


    private async Task DownloadAudioOnly(IStreamInfo audioStreamInfo, string filePath)
    {
        var audioStream = await _youtubeClient.Videos.Streams.GetAsync(audioStreamInfo);
        await DownloadWithProgressAsync(audioStream, filePath);
    }

    private async Task DownloadVideoWithAudio(IStreamInfo videoStreamInfo, IStreamInfo audioStreamInfo, string outputFilePath)
    {
        // Download video and audio streams
        string tempVideoPath = Path.Combine(Path.GetTempPath(), "temp_video.mp4");
        string tempAudioPath = Path.Combine(Path.GetTempPath(), "temp_audio.mp3");

        await DownloadWithProgressAsync(await _youtubeClient.Videos.Streams.GetAsync(videoStreamInfo), tempVideoPath);
        await DownloadWithProgressAsync(await _youtubeClient.Videos.Streams.GetAsync(audioStreamInfo), tempAudioPath);

        // Merge streams using FFmpeg
        var ffmpegHelper = new FfmpegHelper();
        await ffmpegHelper.MergeStreamsAsync(tempVideoPath, tempAudioPath, outputFilePath);
    }

    private async Task DownloadWithProgressAsync(System.IO.Stream stream, string filePath)
    {
        long totalBytes = stream.Length;
        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            byte[] buffer = new byte[8192];
            long totalRead = 0;
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, bytesRead);
                totalRead += bytesRead;
                double progress = (double)totalRead / totalBytes * 100;
                Console.Write($"\rProgress: {progress:F2}%");
            }
        }
        Console.WriteLine("\nDownload completed.");
    }
}

