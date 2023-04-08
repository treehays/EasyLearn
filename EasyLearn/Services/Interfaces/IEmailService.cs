using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.EmailSenderDTOs;

namespace EasyLearn.Services.Interfaces;

public interface IEmailService
{
    Task<BaseResponse> SendEmailWithoutAttachment(EmailSenderNoAttachmentDTO model);
    Task<BaseResponse> EmailVerificationTemplate(EmailSenderDetails model);
    Task<BaseResponse> SendEmailAttachment(EmailSenderAttachmentDTO model);

}
