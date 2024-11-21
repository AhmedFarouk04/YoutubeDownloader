public class FileHandler
{
    public string HandleExistingFile(string fullFilePath)
    {
        // Ensure the fullFilePath is not null or empty
        if (string.IsNullOrWhiteSpace(fullFilePath))
        {
            throw new ArgumentException("File path cannot be null or empty.", nameof(fullFilePath));
        }

        while (true)
        {
            Console.WriteLine($"File \"{Path.GetFileName(fullFilePath)}\" already exists in the folder. What would you like to do?");
            Console.WriteLine("1. Skip the download");
            Console.WriteLine("2. Rename the file");

            string? choice = Console.ReadLine(); // Use nullable string to handle potential null input
            if (choice == "1")
            {
                Console.WriteLine("Download skipped.");
                Environment.Exit(0);
            }
            else if (choice == "2")
            {
                Console.WriteLine("Enter a new file name:");
                string? newFileName = Console.ReadLine(); // Nullable for user input

                // Check if the new file name is valid
                if (string.IsNullOrWhiteSpace(newFileName))
                {
                    Console.WriteLine("File name cannot be null or empty. Please enter a valid name.");
                    continue; // Prompt the user again for a valid name
                }

                // Construct the new full file path
                string newFullFilePath = Path.Combine(Path.GetDirectoryName(fullFilePath) ?? string.Empty, $"{newFileName}{Path.GetExtension(fullFilePath)}");

                if (!File.Exists(newFullFilePath))
                {
                    return newFullFilePath; // Return the new file path if it doesn't exist
                }
                else
                {
                    Console.WriteLine("The new file name also exists. Please choose a different name.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select 1 or 2.");
            }
        }
    }
}

