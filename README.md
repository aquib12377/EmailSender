# Email Sender Application

## Overview

The Email Sender Application is a simple and efficient tool for sending emails with or without attachments. It leverages the .NET framework's `System.Net.Mail` library to handle the email sending process securely and reliably. This application is ideal for developers looking to integrate email functionality into their .NET projects.

## Features

- **Send Emails**: Easily send emails to one or multiple recipients.
- **Attachments Support**: Attach multiple files to your emails.
- **Email Validation**: Ensures that all provided email addresses are valid.
- **Secure Credentials**: Utilizes `SecureString` to handle email passwords securely.

## Prerequisites

- [.NET Framework](https://dotnet.microsoft.com/download/dotnet-framework)
- [Visual Studio](https://visualstudio.microsoft.com/) or any other C# IDE
- Internet connection for sending emails via SMTP

## Getting Started

### Installation

1. **Clone the repository**
    ```sh
    git clone https://github.com/yourusername/emailsender.git
    cd emailsender
    ```

2. **Open the project in Visual Studio**
    - Launch Visual Studio.
    - Open the `emailsender` solution file.

### Configuration

1. **Set Sender Email and Password**
    - Open `Program.cs`.
    - Replace `"Sender email"` with your actual email address.
    - Replace `"Sender password"` with your actual email password.

2. **Set Receiver Email Addresses**
    - Add the receiver email addresses to the `ReceiverEmails` list.

3. **Set Email Subject and Body**
    - Modify the `Subject` and `Body` fields with your desired email subject and body content.

4. **Add Attachments (Optional)**
    - If you want to send attachments, add the file paths to the `Files` list.

### Usage

1. **Run the Application**
    - Press `F5` or click on `Start` in Visual Studio to run the application.
    - The application will attempt to send an email to the specified recipients.

### Example

Here's a basic example of how to use the application:

```csharp
class Program
{
    private static readonly string SenderEmail = "your-email@example.com";
    private static readonly SecureString SenderPassword = GetSecureString("your-email-password");
    private static readonly string Subject = "Test Email";
    private static readonly string Body = "This is a test email body.";
    private static readonly List<string> ReceiverEmails = new List<string> { "receiver1@example.com", "receiver2@example.com" };
    private static readonly List<string> Files = new List<string> { @"C:\path\to\attachment1.pdf", @"C:\path\to\attachment2.docx" };

    static void Main(string[] args)
    {
        var res1 = EmailSender.SendEmail(SenderEmail, SenderPassword, ReceiverEmails, Body, Subject);
        var res2 = EmailSender.SendEmail(SenderEmail, SenderPassword, ReceiverEmails, Body, Subject, Files);
    }

    private static SecureString GetSecureString(string plainText)
    {
        SecureString secureString = new SecureString();
        foreach (char c in plainText)
        {
            secureString.AppendChar(c);
        }
        secureString.MakeReadOnly();
        return secureString;
    }
}
```

## Project Structure

- `Program.cs`: Contains the main program logic for sending emails.
- `EmailSender.cs`: Contains the `EmailSender` class with methods for sending emails.
- `EmailResponse.cs`: Defines the `EmailResponse` class for handling email response statuses and messages.

## Contributing

Contributions are welcome! If you have any improvements or suggestions, please create a pull request or open an issue.

### Steps to Contribute

1. **Fork the Repository**
2. **Clone the Forked Repository**
    ```sh
    git clone https://github.com/yourusername/emailsender.git
    ```
3. **Create a New Branch**
    ```sh
    git checkout -b feature/your-feature-name
    ```
4. **Make Your Changes**
5. **Commit Your Changes**
    ```sh
    git commit -m "Add some feature"
    ```
6. **Push to the Branch**
    ```sh
    git push origin feature/your-feature-name
    ```
7. **Open a Pull Request**

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

If you have any questions or feedback, feel free to reach out:

- **Email**: [ansarimohammedaquib@gmail.com](mailto:ansarimohammedaquib@gmail.com)
- **GitHub**: [aquib12377](https://github.com/aquib12377)

## Support
If you find this project helpful and would like to support its development, you can buy me a coffee!
[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/aquib12377){:target="_blank"}

---

*This README.md was generated with the help of ChatGPT.*
