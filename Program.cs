using System;

namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailSender.SendEmail("youremail@gmail.com",
                "yourPassword", "receiveremail@gmail.com",
                "This is just a mail to test EmailSender class",
                "Test Email");
        }
    }
}
