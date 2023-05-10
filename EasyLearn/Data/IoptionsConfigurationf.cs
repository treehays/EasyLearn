namespace EasyLearn.Data;

public class IoptionsConfigurationf
{
}

public class PaystackOptions
{
    public string APIKey { get; set; }
}

public class SendinblueOptions
{
    public string APIKey { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
}
public class CompanyInfoOption
{
    public string CompanyEmail { get; set; }
    public string CompanyName { get; set; }
    public string AdminID { get; set; }
    public string AdminUserID { get; set; }
}