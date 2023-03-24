using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class PaymentDetailsRepository : BaseRepository<PaymentDetails>, IPaymentDetailsRepository
{

    public PaymentDetailsRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}