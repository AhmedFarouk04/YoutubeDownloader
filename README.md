# YouTube Video Downloader

This project allows you to download videos from YouTube with ease using FFmpeg and Python.

---

## Prerequisites

Before cloning or running this project, please make sure to:

### For Windows:

#### 1. Download FFmpeg:
- Visit the [FFmpeg download page](https://ffmpeg.org/download.html).
- Click on "Windows" to expand the section.
- Choose a build (e.g., **Windows builds by BtbN**).
- Download the latest build (e.g., `ffmpeg-master-latest-win64-gpl.zip`).

#### 2. Extract the Files:
- Locate the downloaded `.zip` file (typically in your `Downloads` folder).
- Right-click the file and choose **Extract All...**.
- Select a destination for the extracted files (e.g., `C:\ffmpeg`).

#### 3. Set Up Environment Variables:
- Open the Start Menu and search for **Environment Variables**.
- Click on **Edit the system environment variables**.
- In the System Properties window, click on the **Environment Variables** button.
- In the **System Variables** section:
  - Find the `Path` variable and select it, then click **Edit...**.
  - Click **New** and add the path to the `bin` folder inside the extracted FFmpeg folder (e.g., `C:\ffmpeg\bin`).
- Click **OK** to close all dialog boxes.

#### 4. Verify Installation:
- Open **Command Prompt** (search for `cmd` in the Start Menu).
- Type `ffmpeg -version` and press **Enter**.
- If installed correctly, the version information for FFmpeg will be displayed.

---

Now you're ready to clone and run the project!
