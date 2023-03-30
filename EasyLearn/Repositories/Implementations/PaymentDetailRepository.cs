using EasyLearn.Data;
using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;

namespace EasyLearn.Repositories.Implementations;

public class PaymentDetailRepository : BaseRepository<PaymentDetails>, IPaymentDetailRepository
{

    public PaymentDetailRepository(EasyLearnDbContext context)
    {
        _context = context;
    }
}