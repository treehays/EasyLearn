namespace EasyLearn.Models.Enums
{
    public enum PaymentStatus
    {
        Pending,
        Processing,
        Authorized,
        Captured,
        Declined,
        Refunded,
        Chargeback,
        Voided,
    }
}


/*
 Pending: The payment is awaiting approval or verification.
Processing: The payment is being processed by the payment gateway.
Authorized: The payment has been authorized by the payment gateway but is not yet captured.
Captured: The payment has been successfully processed and captured.
Declined: The payment has been declined due to insufficient funds, incorrect details, or other reasons.
Refunded: The payment has been refunded to the customer.
Chargeback: The payment has been disputed by the customer and is under review for chargeback.
Voided: The payment has been cancelled before being processed or captured.
 */