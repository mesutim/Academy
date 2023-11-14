using Academy.Model.Models.CourseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Academy.Model.Models.IdentityModels
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionTitle { get; set; }
        public int? ParentID { get; set; }

        #region Relations
        public List<RolePermission> RolePermissions { get; set; }

        [ForeignKey(nameof(ParentID))]
        public virtual Permission Parent { get; set; }
        public virtual List<Permission> Children { get; set; }
        #endregion
    }
}
