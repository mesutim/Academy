using Academy.Model.Models.CourseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Model.Models.IdentityModels
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }
        public string UserAvatar { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsDelete { get; set; }

        #region Relations
        public List<UserRole> UserRoles { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<Course> Courses { get; set; }
        #endregion


    }
}
