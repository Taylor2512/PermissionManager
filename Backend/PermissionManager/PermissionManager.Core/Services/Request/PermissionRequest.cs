namespace PermissionManager.Core.Services.Request
{
    public record PermissionRequest
    {
        public required string EmployeeForename { get; set; }

        public required string EmployeeSurname { get; set; }

        public required int PermissionTypeId { get; set; }
    }
}