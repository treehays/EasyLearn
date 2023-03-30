using EasyLearn.Models.DTOs;
using EasyLearn.Services.Interfaces;
using EasyLearn.Models.DTOs.RoleDTOs;
using EasyLearn.Repositories.Interfaces;
using EasyLearn.Models.Entities;
using System.Security.Claims;

namespace EasyLearn.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public RoleService(IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor = null)
    {
        _roleRepository = roleRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<BaseResponse> Create(CreateRoleRequestModel model)
    {
        var role = await _roleRepository.GetAsync(x => x.RoleName == model.RoleName && !x.IsDeleted);
        if (role != null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Role already exist.",
            };
        }

        var createRole = new Role
        {
            RoleName = model.RoleName,
            Description = model.Description,
            CreatedOn = DateTime.Now,
            CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            Id = model.RoleName,
            //Id = Guid.NewGuid().ToString(),
        };
        await _roleRepository.AddAsync(createRole);
        await _roleRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Status = true,
            Message = "Role Created successfully.",
        };
    }

    public async Task<BaseResponse> Delete(string id)
    {
        var role = await _roleRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
        if (role == null)
        {
            return new BaseResponse
            {
                Status = false,
                Message = "Role already exist.",
            };
        }

        role.IsDeleted = true;
        role.DeletedOn = DateTime.Now;
        role.DeletedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _roleRepository.SaveChangesAsync();
        return new BaseResponse
        {
            Status = true,
            Message = "Role deleted successfully.",
        };

    }

    public async Task<RoleResponseModel> GetById(string id)
    {

        var role = await _roleRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
        if (role == null)
        {
            return new RoleResponseModel
            {
                Status = false,
                Message = "Role does not exist.",
            };
        }

        var roleModel = new RoleResponseModel
        {
            Message = "role retrieved succssfully",
            Status = true,
            Data = new RoleDTOs
            {
                RoleName = role.RoleName,
                Description = role.Description,
            },
        };
        return roleModel;

    }

    public async Task<RolesResponseModel> GetAll()
    {

        var role = await _roleRepository.GetAllAsync();
        if (role == null)
        {
            return new RolesResponseModel
            {
                Status = false,
                Message = "Role does not exist.",
            };
        }

        var roleModel = new RolesResponseModel
        {
            Message = "role retrieved succssfully",
            Status = true,
            Data = role.Select(x => new RoleDTOs
            {
                Id = x.Id,
                RoleName = x.RoleName,
                Description = x.Description,
            }),
        };
        return roleModel;

    }

    public async Task<BaseResponse> UpdateRole(UpdateRoleProfileRequestModel model)
    {
        var role = await _roleRepository.GetAsync(x => x.Id == model.Id && !x.IsDeleted);
        if (role == null)
        {
            return new RoleResponseModel
            {
                Status = false,
                Message = "Role does not exist.",
            };
        }

        role.RoleName = model.RoleName ?? role.RoleName;
        role.Description = model.Description ?? role.Description;
        role.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        role.ModifiedOn = DateTime.Now;
        await _roleRepository.SaveChangesAsync();

        return new BaseResponse
        {
            Status = true,
            Message = "Role updated successfully.",
        };

    }
}