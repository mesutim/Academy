using System.ComponentModel.DataAnnotations.Schema;

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
