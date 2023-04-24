using EasyLearn.Models.Entities;

namespace EasyLearn.Services.Interfaces;

public interface INigerianBankService
{
    Task<ICollection<AcceptedNigerianBank>> FetchAllNigerianBanks();
}
