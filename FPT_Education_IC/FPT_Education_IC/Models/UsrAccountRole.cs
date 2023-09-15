using System;
using System.Collections.Generic;

namespace FPT_Education_IC.Models
{
    public partial class UsrAccountRole
    {
        public UsrAccountRole()
        {
            UsrAccounts = new HashSet<UsrAccount>();
            UsrRolePermissions = new HashSet<UsrRolePermission>();
        }

        public string RoleCode { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<UsrAccount> UsrAccounts { get; set; }
        public virtual ICollection<UsrRolePermission> UsrRolePermissions { get; set; }
    }
}
