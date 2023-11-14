using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Model.Models.CourseModels
{
    public class CourseLevel
    {
        public int LevelId { get; set; }
        public string LevelTitle { get; set; }

        #region Relations
        public List<Course> Courses { get; set; }
        #endregion
    }
}
