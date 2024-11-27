using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CarRentalSystem.Services
{
    public class EmailService
    {
        private readonly string _sendGridApiKey;

        public EmailService(string configuration)
        {
            _sendGridApiKey = configuration;
            
            if (string.IsNullOrEmpty(_sendGridApiKey))
            {
                throw new Exception("SendGrid API key is not set." + configuration);
            }
        }

        public async Task SendBookingConfirmation(string userEmail, string userName, string carDetails, int rentalDays)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("shashanksola1010@gmail.com", "Car Rental System");
            var subject = "Car Rental Booking Confirmation";
            var to = new EmailAddress(userEmail, userName);
            var plainTextContent = $"Dear {userName},\n\nYou have successfully rented the car: {carDetails} for {rentalDays} days.";
            var htmlContent = $"<strong>Dear {userName},</strong><br><br>You have successfully rented the car: <strong>{carDetails}</strong> for <strong>{rentalDays}</strong> days.";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
