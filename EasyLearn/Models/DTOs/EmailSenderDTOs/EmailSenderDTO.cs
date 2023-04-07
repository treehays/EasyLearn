namespace EasyLearn.Models.DTOs.EmailSenderDTOs;

public class EmailSenderDTO
{
    public string ReceiverName { get; set; }
    public string ReceiverEmail { get; set; }
    public string Body { get; set; }
    public string Subject { get; set; }
    public string File { get; set; }
    public string FIleName { get; set; }
}

public class EmailSenderAttachmentDTO : BaseResponse
{
    public string ReceiverName { get; set; }
    public string ReceiverEmail { get; set; }
    public string Body { get; set; }
    public string Subject { get; set; }
    public string File { get; set; }
    public string FIleName { get; set; }
}

public class EmailSenderNoAttachmentDTO : BaseResponse
{
    public string ReceiverName { get; set; }
    public string Body { get; set; }
    public string ReceiverEmail { get; set; }
    public string Subject { get; set; }
}
