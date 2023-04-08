﻿using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.UserDTOs;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginRequestModel> Login(LoginRequestModel model)
        {
            var user = await _userRepository.GetFullDetails(x => x.UserName.ToUpper() == model.Email.ToUpper() || x.Email.ToUpper() == model.Email.ToUpper());


            if (user == null)
            {
                return new LoginRequestModel
                {
                    Message = "invalid login details",
                    Status = false,
                };
            }

            if (!user.EmailConfirmed)
            {
                return new LoginRequestModel
                {
                    Message = "kindly Confirm your email...",
                    Status = false,
                };
            }

            var verifyPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

            if (!verifyPassword)
            {
                return new LoginRequestModel
                {
                    Message = "incorrect login detail...",
                    Status = false,
                };
            }

            var instructorId = user.Instructor != null ? user.Instructor.Id : null;
            var moderatorId = user.Moderator != null ? user.Moderator.Id : null;
            var adminId = user.Admin != null ? user.Admin.Id : null;
            var studentId = user.Student != null ? user.Student.Id : null;
            var loginModel = new LoginRequestModel
            {
                Message = "Login successfully..",
                Status = true,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId,
                LastName = user.LastName,
                FirstName = user.FirstName,
                ProfilePicture = user.ProfilePicture,
                Id = instructorId ?? moderatorId ?? adminId ?? studentId,
                UserId = user.Id,
            };
            return loginModel;
        }


        public async Task<BaseResponse> EmailVerification(string emailToken)
        {

            var user = await _userRepository.GetAsync(x => x.EmailToken == emailToken);

            if (user == null)
            {
                return new BaseResponse
                {
                    Message = "Wrong verification code...",
                    Status = false,
                };
            }

            if (user.EmailConfirmed)
            {
                return new BaseResponse
                {
                    Message = "Account already verified, proceed to login...",
                    Status = false,
                };
            }

            var date = DateTime.Now;
            var modifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            user.EmailConfirmed = true;
            user.ModifiedOn = date;
            user.ModifiedBy = modifiedBy;

            await _userRepository.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Account activated...",
                Status = true,
            };
        }

        public async Task<bool> Testing(string email, string OTPKey)
        {

            //var tokeenKey = _configuration.TokenKey;
            Configuration.Default.ApiKey.Clear();
            //Configuration.Default.ApiKey.Add("api-keyj", tokeenKey);



            Configuration.Default.ApiKey.Add("api-key", "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");

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "Ahmad Sender Name";
            string SenderEmail = "treehays90@gmail.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);//emailsender


            string ToEmail = "aymoneyay@gmail.com";
            string ToName = "Abdulsalam TO Name";
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);//emailreciever


            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);//bulkemail


            string BccName = "Akin BCC Name";
            string BccEmail = "abdulsalamayoola@gmail.com";
            SendSmtpEmailBcc BccData = new SendSmtpEmailBcc(BccEmail, BccName);
            List<SendSmtpEmailBcc> Bcc = new List<SendSmtpEmailBcc>();
            Bcc.Add(BccData);



            string CcName = "Ayo CCNAme";
            string CcEmail = "treehays90@yahoo.com";
            SendSmtpEmailCc CcData = new SendSmtpEmailCc(CcEmail, CcName);
            List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>();
            Cc.Add(CcData);



            string HtmlContent = "<html><body><h1>This is my first transactional email {{params.parameter}}</h1></body></html>";


            string TextContent = null;




            string Subject = "My {{params.subject}}";
            string ReplyToName = "Mad Reply to ";
            string ReplyToEmail = "treehays90@gmail.com";
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail, ReplyToName);




            string AttachmentUrl = null;
            string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            byte[] Content = System.Convert.FromBase64String(stringInBase64);
            string AttachmentName = "test.txt";
            SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            List<SendSmtpEmailAttachment> Attachment = new List<SendSmtpEmailAttachment>();
            Attachment.Add(AttachmentContent);




            JObject Headers = new JObject();
            Headers.Add("Some-Custom-Name", "unique-id-1234");
            long? TemplateId = null;
            JObject Params = new JObject();
            Params.Add("parameter", "My param value");
            Params.Add("subject", "New Subject");
            List<string> Tags = new List<string>();
            Tags.Add("mytag");
            SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(ToEmail, ToName);
            List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>();
            To1.Add(smtpEmailTo1);
            Dictionary<string, object> _parmas = new Dictionary<string, object>();
            _parmas.Add("params", Params);
            SendSmtpEmailReplyTo1 ReplyTo1 = new SendSmtpEmailReplyTo1(ReplyToEmail, ReplyToName);
            SendSmtpEmailMessageVersions messageVersion = new SendSmtpEmailMessageVersions(To1, _parmas, Bcc, Cc, ReplyTo1, Subject);
            List<SendSmtpEmailMessageVersions> messageVersiopns = new List<SendSmtpEmailMessageVersions>();
            messageVersiopns.Add(messageVersion);
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, Bcc, Cc, HtmlContent, TextContent, Subject, ReplyTo, Attachment, Headers, TemplateId, Params, messageVersiopns, Tags);


                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Debug.WriteLine(result.ToJson());
                Console.WriteLine(result.ToJson());
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            return true;
        }

    }
}
