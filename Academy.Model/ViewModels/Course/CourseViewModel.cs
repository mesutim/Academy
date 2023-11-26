using Academy.Model.Models.CourseModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Model.ViewModels
{
    public class ShowCourseForAdminViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int EpisodeCount { get; set; }

    }
    public class CreateCourseAdminViewModel
    {
        public CreateCourseViewModel Course { get; set; }
        public SelectList Group { get; set; }
        public SelectList? SubGroup { get; set; }
        public SelectList Teacher { get; set; }
        public SelectList Level { get; set; }
        public SelectList Status { get; set; }

    }
    public class CreateCourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int? TeacherId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Demo { get; set; }
        public string Image { get; set; }
        public string Tags { get; set; }
        public int LevelId { get; set; }
        public int StatusId { get; set; }
    }
}
