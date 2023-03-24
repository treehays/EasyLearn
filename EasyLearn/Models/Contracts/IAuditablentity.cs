namespace EasyLearn.Models.Contracts;

public interface IAuditablentity
{
    public string CreatedBy{get; set;}
    public DateTime CreatedOn{get; set;}
    public string? ModifiedBy{get; set;}
    public DateTime? ModifiedOn{get; set;}
}