using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class UsrRolePermission
    {
        public int Id { get; set; }
        public string Role { get; set; } = null!;
        public string? PermissionCode { get; set; }
        public bool IsEnable { get; set; }

        public virtual UsrAccountRole RoleNavigation { get; set; } = null!;
    }
}
