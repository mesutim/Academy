using Academy.Model.Models.CourseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Model.Models.CourseModels
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDelete { get; set; }
        public int? ParentId { get; set; }

        #region Relations
        public List<CourseCategory> CourseCategories { get; set; }
        [ForeignKey(nameof(ParentId))]
        public  Category Parent { get; set; }
        public  List<Category> Children { get; set; }
        #endregion


    }
}
