using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Model.Models.CourseModels
{
    public class CourseStatus
    {
        public int StatusId { get; set; }
        public string StatusTitle { get; set; }

        #region Relations
        public List<Course> Courses { get; set; }
        #endregion
    }
}
