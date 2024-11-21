public class UserInputHandler
{
    public string GetValidUrl()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Enter the YouTube video URL:");
                string videoUrl = Console.ReadLine() ?? throw new ArgumentNullException("videoUrl", "YouTube URL cannot be null.");

                if (string.IsNullOrEmpty(videoUrl) || !Uri.IsWellFormedUriString(videoUrl, UriKind.Absolute))
                {
                    Console.WriteLine("Invalid YouTube URL. Please try again.");
                    continue;
                }
                return videoUrl;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public string GetFormatChoice()
    {
        while (true)
        {
            Console.WriteLine("Choose download format:\n1. MP3 (Audio only)\n2. MP4 (Audio and Video)");
            string? formatChoice = Console.ReadLine(); // Use nullable string to handle null input

            if (string.IsNullOrWhiteSpace(formatChoice))
            {
                throw new ArgumentException("Format choice cannot be null or empty.", nameof(formatChoice));
            }

            if (formatChoice == "1" || formatChoice == "2")
            {
                return formatChoice; // Return the valid format choice
            }

            Console.WriteLine("Invalid choice. Please select 1 or 2.");
        }
    }


    public string GetValidDownloadPath()
    {
        while (true)
        {
            Console.WriteLine("Enter the folder path where you want to save the files:");
            string? downloadPath = Console.ReadLine();
            if (string.IsNullOrEmpty(downloadPath))
            {
                Console.WriteLine("Path cannot be null or empty. Please enter a valid path.");
                continue;
            }

            if (!Directory.Exists(downloadPath))
            {
                Console.WriteLine("Invalid path. Please enter a valid path.");
                continue;
            }

            return downloadPath;
        }
    }

    public string GetCustomFileName()
    {
        Console.WriteLine("Enter a custom file name (without extension) or press Enter to keep the default name:");
        string? customFileName = Console.ReadLine(); // Nullable to handle potential null input

        // Return an empty string if customFileName is null, meaning use the default name
        return customFileName ?? string.Empty;
    }
}

