namespace youtube;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using System.Text.RegularExpressions;

class Program
{
    static async Task Main(string[] args)
    {
        // Get the YouTube video URL from the user
        Console.Write("Enter the YouTube video URL: ");
        string videoUrl = Console.ReadLine();

        // Get the file extension from the user
        Console.Write("Enter the file extension (mp4, mp3, etc.): ");
        string fileExtension = Console.ReadLine();

        // Create an instance of the YoutubeClient
        var youtube = new YoutubeClient();

        try
        {
            // Get video information
            var video = await youtube.Videos.GetAsync(videoUrl);
            Console.WriteLine($"Downloading: {video.Title}");

            // Get available streams (audio and video)
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);

            // Ensure the stream manifest contains streams
            if (!streamManifest.GetMuxedStreams().Any() && !streamManifest.GetAudioOnlyStreams().Any())
            {
                Console.WriteLine("No suitable streams found for the given file extension.");
                return;
            }

            // Choose a stream based on the user's file extension input
            IStreamInfo streamInfo;

            if (fileExtension.Equals("mp4", StringComparison.OrdinalIgnoreCase))
            {
                // Get the highest quality video + audio stream
                streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            }
            else if (fileExtension.Equals("mp3", StringComparison.OrdinalIgnoreCase))
            {
                // Get the highest quality audio stream
                streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            }
            else
            {
                Console.WriteLine("Unsupported file extension.");
                return;
            }

            // Sanitize the video title to be used as a valid file name
            string sanitizedTitle = Regex.Replace(video.Title, @"[<>:""/\|?*]", "_");

            // Define the file name and path
            var filePath = Path.Combine(Environment.CurrentDirectory, $"{sanitizedTitle}.{fileExtension}");

            // Download the stream to the file
            await youtube.Videos.Streams.DownloadAsync(streamInfo, filePath);

            Console.WriteLine($"Download completed: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
