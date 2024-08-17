
#nullable disable
using System;
using System.Collections.Generic;

namespace PermissionManager.Shared.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int PermissionTypeId { get; set; }

    public DateOnly PermissionDate { get; set; }

    public virtual PermissionType PermissionType { get; set; }
}