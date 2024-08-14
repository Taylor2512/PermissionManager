namespace PermissionManager.Core.Services.Dtos
{
    public record PermissionRequest
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required int PermissionTypeId { get; set; }

    }
}