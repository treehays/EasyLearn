using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.EmailSenderDTOs;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace EasyLearn.GateWays.Email;

public class SendInBlueEmailService : ISendInBlueEmailService
{
    private readonly IConfiguration _configuration;

    public SendInBlueEmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<BaseResponse> CourseCompletionEmailTemplate(EmailSenderDetails model, string baseUrl)
    {
        //var ddd = ;
        var key = _configuration.GetSection("SendinblueAPIKey")["APIKey"];
        var senderName = _configuration.GetSection("SendinblueAPIKey")["SenderName"];
        var senderEmail = _configuration.GetSection("SendinblueAPIKey")["SenderEmail"];

        Configuration.Default.ApiKey.Clear();
        Configuration.Default.ApiKey.Add("api-key", key);
        var apiInstance = new TransactionalEmailsApi();

        var emailSender = new SendSmtpEmailSender(senderName, senderEmail);

        var emailReciever = new SendSmtpEmailTo(model.ReceiverEmail, model.ReceiverName);

        var emailRecievers = new List<SendSmtpEmailTo>
        {
            emailReciever
        };

        var replyTo = new SendSmtpEmailReplyTo("treehays90@gmail.com", "Do not reply");

        var subject = $"Course Completion Notification";


        var htmlContent = EmailTemplates.CourseCompletionEmailTemplate(model, baseUrl);
        var sendSmtpEmail = new SendSmtpEmail
        {
            Sender = emailSender,
            HtmlContent = htmlContent,
            Subject = subject,
            ReplyTo = replyTo,
            To = emailRecievers,
        };

        try
        {
            var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

            return new BaseResponse
            {
                Status = true,
                Message = "Email successfully sent..",
            };
        }
        catch (Exception)
        {

            return new BaseResponse
            {
                Status = false,
                Message = "Email not sent..",
            };
        }

    }
    public async Task<BaseResponse> EmailVerificationTemplate(EmailSenderDetails model, string baseUrl)
    {
        //var ddd = ;
        var key = _configuration.GetSection("SendinblueAPIKey")["APIKey"];
        var senderName = _configuration.GetSection("SendinblueAPIKey")["SenderName"];
        var senderEmail = _configuration.GetSection("SendinblueAPIKey")["SenderEmail"];

        Configuration.Default.ApiKey.Clear();
        Configuration.Default.ApiKey.Add("api-key", key);
        var apiInstance = new TransactionalEmailsApi();

        var emailSender = new SendSmtpEmailSender(senderName, senderEmail);

        var emailReciever = new SendSmtpEmailTo(model.ReceiverEmail, model.ReceiverName);

        var emailRecievers = new List<SendSmtpEmailTo>
        {
            emailReciever
        };

        var replyTo = new SendSmtpEmailReplyTo("treehays90@gmail.com", "Do not reply");

        var subject = $"Email verification {DateTime.Now}";


        var htmlContent = EmailTemplates.ConfirmationEmailTemplate(model, baseUrl);
        var sendSmtpEmail = new SendSmtpEmail
        {
            Sender = emailSender,
            HtmlContent = htmlContent,
            Subject = subject,
            ReplyTo = replyTo,
            To = emailRecievers,
        };

        try
        {
            var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

            return new BaseResponse
            {
                Status = true,
                Message = "Email successfully sent..",
            };
        }
        catch (Exception)
        {

            return new BaseResponse
            {
                Status = false,
                Message = "Email not sent..",
            };
        }
    }

    public async Task<BaseResponse> EnrollmentEmailTemplate(EmailSenderDetails model, string baseUrl)
    {
        //var ddd = ;
        var key = _configuration.GetSection("SendinblueAPIKey")["APIKey"];
        var senderName = _configuration.GetSection("SendinblueAPIKey")["SenderName"];
        var senderEmail = _configuration.GetSection("SendinblueAPIKey")["SenderEmail"];

        Configuration.Default.ApiKey.Clear();
        Configuration.Default.ApiKey.Add("api-key", key);
        var apiInstance = new TransactionalEmailsApi();

        var emailSender = new SendSmtpEmailSender(senderName, senderEmail);

        var emailReciever = new SendSmtpEmailTo(model.ReceiverEmail, model.ReceiverName);

        var emailRecievers = new List<SendSmtpEmailTo>
        {
            emailReciever
        };

        var replyTo = new SendSmtpEmailReplyTo("treehays90@gmail.com", "Do not reply");

        var subject = $"Course enrollment {DateTime.Now}";


        var htmlContent = EmailTemplates.EnrollmentEmailTemplate(model, baseUrl);
        var sendSmtpEmail = new SendSmtpEmail
        {
            Sender = emailSender,
            HtmlContent = htmlContent,
            Subject = subject,
            ReplyTo = replyTo,
            To = emailRecievers,
        };

        try
        {
            var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

            return new BaseResponse
            {
                Status = true,
                Message = "Email successfully sent..",
            };
        }
        catch (Exception)
        {

            return new BaseResponse
            {
                Status = false,
                Message = "Email not sent..",
            };
        }

    }

    public async Task<BaseResponse> SendEmailWithoutAttachment(EmailSenderNoAttachmentDTO model)
    {
        var key = _configuration.GetSection("SendinblueAPIKey")["APIKey"];
        var senderName = _configuration.GetSection("SendinblueAPIKey")["SenderName"];
        var senderEmail = _configuration.GetSection("SendinblueAPIKey")["SenderEmail"];

        Configuration.Default.ApiKey.Clear();
        Configuration.Default.ApiKey.Add("api-key", key);
        var apiInstance = new TransactionalEmailsApi();

        var emailSender = new SendSmtpEmailSender(senderName, senderEmail);

        var emailReciever = new SendSmtpEmailTo(model.ReceiverEmail, model.ReceiverEmail);

        var emailRecievers = new List<SendSmtpEmailTo>();
        emailRecievers.Add(emailReciever);

        var replyTo = new SendSmtpEmailReplyTo("treehays90@gmail.com", "Do not reply");

        var subject = $"My Sample {model.Subject}";


        var htmlContent = $"< html >< body >< h1 > This testing Message {model.Body} </ h1 > <h4>Thanks for trying it</h4> </ body ></ html > ";

        var stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
        var content = Convert.FromBase64String(stringInBase64);
        var attachmentContent = new SendSmtpEmailAttachment
        {
            Content = content,
            Name = "Attachment.txt"
        };
        var attachment = new List<SendSmtpEmailAttachment>();
        attachment.Add(attachmentContent);

        var sendSmtpEmail = new SendSmtpEmail
        {
            Sender = emailSender,
            HtmlContent = htmlContent,
            Subject = subject,
            ReplyTo = replyTo,
            To = emailRecievers,
            Attachment = attachment,
        };
        //var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        try
        {
            //var result1 = apiInstance.SendTransacEmail(sendSmtpEmail);
            var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

            return new BaseResponse
            {
                Status = true,
                Message = "Email successfully sent..",
            };
        }
        catch (Exception)
        {

            return new BaseResponse
            {
                Status = true,
                Message = "Email not sent..",
            };
        }
    }

    public async Task<BaseResponse> WithdrawalConfirmationEmailTemplate(EmailSenderDetails model, string baseUrl)
    {
        //var ddd = ;
        var key = _configuration.GetSection("SendinblueAPIKey")["APIKey"];
        var senderName = _configuration.GetSection("SendinblueAPIKey")["SenderName"];
        var senderEmail = _configuration.GetSection("SendinblueAPIKey")["SenderEmail"];

        Configuration.Default.ApiKey.Clear();
        Configuration.Default.ApiKey.Add("api-key", key);
        var apiInstance = new TransactionalEmailsApi();

        var emailSender = new SendSmtpEmailSender(senderName, senderEmail);

        var emailReciever = new SendSmtpEmailTo(model.ReceiverEmail, model.ReceiverName);

        var emailRecievers = new List<SendSmtpEmailTo>
        {
            emailReciever
        };

        var replyTo = new SendSmtpEmailReplyTo("treehays90@gmail.com", "Do not reply");

        var subject = $"Withdrawal confirmation{DateTime.Now}";


        var htmlContent = EmailTemplates.WithdrawalComfirmationEmailTemplate(model, baseUrl);
        var sendSmtpEmail = new SendSmtpEmail
        {
            Sender = emailSender,
            HtmlContent = htmlContent,
            Subject = subject,
            ReplyTo = replyTo,
            To = emailRecievers,
        };

        try
        {
            var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

            return new BaseResponse
            {
                Status = true,
                Message = "Email successfully sent..",
            };
        }
        catch (Exception)
        {

            return new BaseResponse
            {
                Status = false,
                Message = "Email not sent..",
            };
        }

    }
}














