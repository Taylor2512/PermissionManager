
#nullable disable
using System;
using System.Collections.Generic;

namespace PermissionManager.Shared.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string EmployeeForename { get; set; }

    public string EmployeeSurname { get; set; }

    public int PermissionTypeId { get; set; }

    public DateTime PermissionDate { get; set; } = DateTime.UtcNow;

    public virtual PermissionType PermissionType { get; set; }
}