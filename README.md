This project is a lightweight, fast, and elegant OCR (Optical Character Recognition) app.
It enables users to:

Upload an image file (e.g., JPG, PNG).

Use Azure Cognitive Services to analyze the image.

Extract printed or handwritten text from the image.

View and edit the extracted text directly inside the app.

Save the extracted text into a .txt file.

Enjoy smooth animations and UI interactions.
 Technologies Used

Technology	Purpose
Avalonia UI	Cross-platform desktop app UI
.NET SDK (C#)	Backend app logic and structure
Azure Computer Vision API	OCR text extraction
Git & GitHub	Version control and hosting

Features
Upload Image: Choose any local image from your device.

Real-Time OCR: Send the image to Azure's OCR service and retrieve extracted text instantly.

Loading Animation: Smooth progress bar while OCR is running.

Edit Text: TextBox where users can make edits to the OCR output.

Save Results: Download the edited text into a local .txt file.

Modern UI: Minimalistic and responsive design using Avalonia.

⚡ How to Run
Clone this repo:

bash
Copy
Edit
git clone https://github.com/aditiShukla27/AzureOcrDesktopApp.git
cd AzureOcrDesktopApp
Run the app:

bash
Copy
Edit
dotnet run
Make sure you have the .NET SDK installed on your Mac!

What I Learned
How to build a cross-platform desktop app using Avalonia.

How to integrate Azure Cognitive Services into a real-world application.

Best practices around UI/UX design for desktop apps.

Managing API keys securely and handling error messages.

How to push projects to GitHub and use Personal Access Tokens for authentication.

Future Improvements
Drag-and-drop image upload functionality.

Support for multi-language OCR.

Dark mode for the UI.

More detailed error handling and retry logic.

Acknowledgements
Avalonia UI Documentation

Azure Cognitive Services

Microsoft Docs on OCR

