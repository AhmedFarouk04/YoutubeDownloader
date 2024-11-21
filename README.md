# YouTube Video Downloader

This project allows you to download videos from YouTube with ease using FFmpeg and C# libraries like YoutubeExplode.

---

## Prerequisites

Before cloning or running this project, please make sure to:

### 1. Download and Install FFmpeg (For Windows):

#### a. Download FFmpeg:
- Visit the [FFmpeg download page](https://ffmpeg.org/download.html).
- Click on "Windows" to expand the section.
- Choose a build (e.g., **Windows builds by BtbN**).
- Download the latest build (e.g., `ffmpeg-master-latest-win64-gpl.zip`).

#### b. Extract the Files:
- Locate the downloaded `.zip` file (typically in your `Downloads` folder).
- Right-click the file and choose **Extract All...**.
- Select a destination for the extracted files (e.g., `C:\ffmpeg`).

#### c. Set Up Environment Variables:
- Open the Start Menu and search for **Environment Variables**.
- Click on **Edit the system environment variables**.
- In the System Properties window, click on the **Environment Variables** button.
- In the **System Variables** section:
  - Find the `Path` variable and select it, then click **Edit...**.
  - Click **New** and add the path to the `bin` folder inside the extracted FFmpeg folder (e.g., `C:\ffmpeg\bin`).
- Click **OK** to close all dialog boxes.

#### d. Verify Installation:
- Open **Command Prompt** (search for `cmd` in the Start Menu).
- Type `ffmpeg -version` and press **Enter**.
- If installed correctly, the version information for FFmpeg will be displayed.

---

### 2. Install Required Libraries

To ensure the project runs correctly, make sure to install the required libraries using **NuGet Package Manager**.

#### a. Open the NuGet Package Manager Console:
- In Visual Studio, go to the **Tools** menu.
- Select **NuGet Package Manager** > **Manage NuGet Packages for Solution...**.

#### b. Install Required Packages:
Run the following commands in the **Package Manager Console**:

```powershell
Install-Package YoutubeExplode
Install-Package System.Linq
Install-Package System.Threading.Tasks
