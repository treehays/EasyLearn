using EasyLearn.Models.DTOs;
using EasyLearn.Models.DTOs.AdminDTOs;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Services.Interfaces;

namespace EasyLearn.Services.Implementations;


public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IUserRepository _userRepository;

    public AdminService(IAdminRepository adminRepository, IUserRepository userRepository)
    {
        _adminRepository = adminRepository;
        _userRepository = userRepository;
    }


    public async Task<BaseResponse> CreateAdmin(CreateAdminRequestModel model)
    {
        throw new NotImplementedException();
    }
}