using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.EmailSenderDTOs;
using EasyLearn.Services.Interfaces;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace EasyLearn.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<BaseResponse> SendEmailAttachment(EmailSenderAttachmentDTO model)
    {
        //var ddd = ;
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


        var htmlContent = $"< html >< body >< h1 > This testing Message {model.Messages} </ h1 > <h4>Thanks for trying it</h4> </ body ></ html > ";

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

        var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

        return new BaseResponse
        {
            Status = true,
            Message = "Email successfully sent..",
        };

        //try
        //{
        //    var result = apiInstance.SendTransacEmail(sendSmtpEmail);
        //}
        //catch (Exception e)
        //{

        //}
    }

    public Task<BaseResponse> SendEmailWithoutAttachment(EmailSenderNoAttachmentDTO model)
    {
        throw new NotImplementedException();
    }
}
