namespace PermissionManager.Core.Services.Dtos
{
    public record PermissionDto
    {
        public int Id { get; set; }
        public required string EmployeeForename { get; set; }
        public required string EmployeeSurname { get; set; }
        public required string PermissionTypeName { get; set; }
        public int PermissionTypeId { get; set; }

        public DateTime PermissionDate { get; set; }
        public virtual PermissionTypeDto? PermissionType { get; set; }
    }
}