using Academy.Model.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Academy.Model.Models.CourseModels
{
    public class UserCourse
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        #region Relations
        public Course Course { get; set; }
        public User User { get; set; }
        #endregion
    }
}
