using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Model.Models.CourseModels
{
    public class CourseCategory
    {
        public int CategoryId { get; set; }
        public int CourseId { get; set; }
        
        #region Relations
        public Category Category { get; set; }
        public Course Course { get; set; }
        #endregion

    }
}
