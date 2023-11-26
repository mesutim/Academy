
namespace Academy.Model.Models.CourseModels
{
    public class CourseEpisode
    {
        public int EpisodeId { get; set; }
        public int CourseId { get; set; }
        public string EpisodeTitle { get; set; }
        public TimeSpan EpisodeTime { get; set; }
        public string EpisodeFileName { get; set; }
        public bool IsFree { get; set; }
        
        #region Relations
        public Course Course { get; set; }
        #endregion

    }
}
