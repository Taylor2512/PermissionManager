
#nullable disable
using System;
using System.Collections.Generic;

namespace PermissionManager.Shared.Models;

public partial class PermissionType
{
    public int Id { get; set; }

    public string Description { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
  
}