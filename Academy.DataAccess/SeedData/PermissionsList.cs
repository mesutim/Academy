using Academy.Model.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DataAccess.SeedData
{
    public static class PermissionsList
    {
        private static readonly List<Permission> _permissions = new List<Permission>
        {
            #region Admin
            new Permission(){PermissionId=1, PermissionTitle="پنل مدیریت"},
            new Permission(){PermissionId=2, PermissionTitle="مدیریت کاربران", ParentID=1},
            new Permission(){PermissionId=3, PermissionTitle="لیست کاربر", ParentID=2},
            new Permission(){PermissionId=4, PermissionTitle="ایجاد کاربر جدید", ParentID=2},
            new Permission(){PermissionId=5, PermissionTitle="ویرایش کاربر", ParentID=2},
            new Permission(){PermissionId=6, PermissionTitle="حذف کاربر", ParentID=2},
            new Permission(){PermissionId=7, PermissionTitle="مدیریت نقش ها", ParentID=1},
            new Permission(){PermissionId=8, PermissionTitle="لیست نقش ها", ParentID=7},
            new Permission(){PermissionId=9, PermissionTitle="افزودن نقش جدید", ParentID=7},
            new Permission(){PermissionId=10, PermissionTitle="ویرایش نقش", ParentID=7},
            new Permission(){PermissionId=11, PermissionTitle="حذف نقش", ParentID=7},
            #endregion
            //last id used => 14
        };
        public static List<Permission> Permissions => _permissions;
    }
}
