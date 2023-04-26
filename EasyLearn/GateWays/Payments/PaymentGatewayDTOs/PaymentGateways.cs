namespace EasyLearn.GateWays.Payments.PaymentGatewayDTOs;

public class PaymentGateways
{
}

public class PaysatckInitializePayment
{

}


public class InitializePaymentRequestModel
{
    public double CoursePrice { get; set; }
    public string RefrenceNo { get; set; }
    public string Email { get; set; }
    public string CallbackUrl { get; set; }

}

public class InitializePaymentData
{
    public string authorization_url { get; set; }
    public string access_code { get; set; }
    public string reference { get; set; }

    //"authorization_url": "https://checkout.paystack.com/0peioxfhpn",
    //"access_code": "0peioxfhpn",
    //"reference": "7PVGX8MEk85tgeEpVDtD"

}

public class InitializePaymentResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public InitializePaymentData data { get; set; }
    //  "status": true,
    //"message": "Authorization URL created",
    //"data":
}


public class VerifyAccountNumberRequestModel
{
    public string AccountNumber { get; set; }
    public string BankCode { get; set; }

}

public class VerifyAccountNumberData
{
    public string account_name { get; set; }
    public string account_number { get; set; }
    //public string bank_id { get; set; }
    //"data\":{\"account_number\":\"0159192507\",\"account_name\":\"ABDULSALAM AHMAD AYOOLA\",\"bank_id\":9}
}

public class VerifyAccountNumberResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public VerifyAccountNumberData data { get; set; }
    //"{\"status\":true,\"message\":\"Account number resolved\",\"data\":{\"account_number\":\"0159192507\",\"account_name\":\"ABDULSALAM AHMAD AYOOLA\",\"bank_id\":9}}"
}

//public class VerifyTransactionRequestModel
//{
//    public string ReferenceNumber { get; set; }

//}

public class VerifyTransactionData
{
    public uint id { get; set; }
    public string domain { get; set; }
    public string status { get; set; }
    public string gateway_response { get; set; }
    public string reference { get; set; }
    public int amount { get; set; }
    public DateTime paid_at { get; set; }
    public DateTime created_at { get; set; }
    public string currency { get; set; }
    public string channel { get; set; }
    public string ip_address { get; set; }
    //public string bank_id { get; set; }
    //"data\":{\"account_number\":\"0159192507\",\"account_name\":\"ABDULSALAM AHMAD AYOOLA\",\"bank_id\":9}
}
public class VerifyTransactionResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public VerifyTransactionData data { get; set; }
    //"{\"status\":true,\"message\":\"Account number resolved\",\"data\":{\"account_number\":\"0159192507\",\"account_name\":\"ABDULSALAM AHMAD AYOOLA\",\"bank_id\":9}}"
}

public class CreateTransferRecipientRequestModel
{
    public string Type { get; set; } = "nuban";
    public string Name { get; set; }
    public string Description { get; set; }
    public string AccountNumber { get; set; }
    public string BankCode { get; set; }
    public string Currency { get; set; } = "NGN";


}

public class CreateTransferRecipientDataDetails
{
    public string authorization_code { get; set; }
    public string account_number { get; set; }
    public string account_name { get; set; }
    public string bank_code { get; set; }
    public string bank_name { get; set; }
}
public class CreateTransferRecipientData
{
    public bool active { get; set; }
    public DateTime createdAt { get; set; }
    public string domain { get; set; }
    public uint id { get; set; }
    public int integration { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string recipient_code { get; set; }
    public DateTime updatedAt { get; set; }
    public bool is_deleted { get; set; }
    public CreateTransferRecipientDataDetails details { get; set; }

}

public class CreateTransferRecipientResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public CreateTransferRecipientData data { get; set; }
    //"{\"status\":true,\"message\":\"Account number resolved\",\"data\":{\"account_number\":\"0159192507\",\"account_name\":\"ABDULSALAM AHMAD AYOOLA\",\"bank_id\":9}}"
}

public class TransferMoneyToUserResponseModel
{
    public bool status { get; set; }
    public string message { get; set; }
    public TransferMoneyToUserData data { get; set; }
}

public class TransferMoneyToUserRequestModel
{
    public CreateTransferRecipientResponseModel model { get; set; }
    public string TransferSourse { get; set; } = "balance";
    public string TransferReason { get; set; }
    public string TransferReference { get; set; } = Guid.NewGuid().ToString().Replace('-', 'y');
    public string Currency { get; set; } = "NGN";
    public string Recipient { get; set; }
    public int Amount { get; set; }

}

public class TransferMoneyToUserData
{
    public int integration { get; set; }
    public int amount { get; set; }
    public int recipient { get; set; }
    public int id { get; set; }
    public string domain { get; set; }
    public string currency { get; set; }
    public string source { get; set; }
    public string reason { get; set; }
    public string status { get; set; }
    public string createdAt { get; set; }
    public string updatedAt { get; set; }
}
