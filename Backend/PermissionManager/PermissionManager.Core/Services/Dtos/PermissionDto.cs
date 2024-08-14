namespace PermissionManager.Core.Services.Dtos
{
    public record PermissionDto
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string PermissionTypeName { get; set; }

        public DateOnly PermissionDate { get; set; }
    }
}