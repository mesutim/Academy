using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Model.Models.IdentityModels
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
        public bool IsDelete { get; set; }

        #region Relations
        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
