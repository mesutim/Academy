﻿
namespace Academy.Model.Models.IdentityModels
{
   public class RolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        #region Relations
        public Role Role { get; set; }
        public Permission Permission { get; set; }
        #endregion
    }
}
