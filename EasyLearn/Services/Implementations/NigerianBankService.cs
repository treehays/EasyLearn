using EasyLearn.Models.Entities;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;

namespace EasyLearn.Services.Implementations;

public class NigerianBankService : INigerianBankService
{
    private readonly INigerianBankRepository _nigerianBankRepository;

    public NigerianBankService(INigerianBankRepository nigerianBankRepository)
    {
        _nigerianBankRepository = nigerianBankRepository;
    }

    public async Task<ICollection<AcceptedNigerianBank>> FetchAllNigerianBanks()
    {
        var listOfBanks = await _nigerianBankRepository.GetAllAsync();
        return listOfBanks;
    }
}
