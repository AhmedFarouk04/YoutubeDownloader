public static class FileNameGenerator
{
    public static string GenerateFileName(string videoTitle, string? customFileName, string videoQuality, string audioQuality)
    {
        if (string.IsNullOrWhiteSpace(videoTitle))
        {
            throw new ArgumentException("Video title cannot be null or empty.", nameof(videoTitle));
        }

        string baseFileName = string.IsNullOrEmpty(customFileName)
            ? string.Join("_", videoTitle.Split(Path.GetInvalidFileNameChars())) // Clean the video title
            : string.Join("_", customFileName.Split(Path.GetInvalidFileNameChars())); // Clean the custom file name

        return $"{baseFileName} (Video {videoQuality}, Audio {audioQuality})";
    }
}

