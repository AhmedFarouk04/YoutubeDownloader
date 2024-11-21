using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;

class Program
{
    static async Task Main(string[] args)
    {
        var youtubeClient = new YoutubeClient();

        try
        {
            // Step 1: Collect user input
            UserInputHandler inputHandler = new UserInputHandler();
            string videoUrl = inputHandler.GetValidUrl();
            string formatChoice = inputHandler.GetFormatChoice();
            string downloadPath = inputHandler.GetValidDownloadPath();
            string customFileName = inputHandler.GetCustomFileName();

            // Step 2: Initialize downloader and download
            var downloader = new VideoDownloader(youtubeClient);
            await downloader.DownloadAsync(videoUrl, formatChoice, downloadPath, customFileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

