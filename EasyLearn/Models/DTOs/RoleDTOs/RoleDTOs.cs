namespace EasyLearn.Models.DTOs.RoleDTOs
{
    public class RoleDTO
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }

    public class CreateRoleRequestModel
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
    }

    public class UpdateRoleProfileRequestModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }

    public class RoleResponseModel : BaseResponse
    {
        public RoleDTO Data { get; set; }
    }

    public class RolesResponseModel : BaseResponse
    {
        public IEnumerable<RoleDTO> Data { get; set; }
    }

}
