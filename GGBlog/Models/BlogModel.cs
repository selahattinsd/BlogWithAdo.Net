using System.ComponentModel.DataAnnotations;

namespace GGBlog.Models
{

    public class Common
    {
        public List<string> Cats { get; set; }
    }

    public class BlogModel
    {
        [Required]
        public string title { get; set; }

        [Required]
        public string summary { get; set; }

        [Required]
        public string content { get; set; }

        [Required]
        public string slug { get; set; }
    }

    public class BlogPost
    {
        public int id { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
        public string content { get; set; }
        public string slug { get; set; }
        public DateTime created_on { get; set; }    
    }

    public class BlogListModel : Common
    {
        public List<BlogPost> BlogPosts { get; set; }
        public List<BlogPostDetail> BlogPostDetails { get;}
    }

    public class BlogPostDetail
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public List<BlogCommentItem> Comments { get; set; }
        public DateTime created_on { get; set; }

    }
    public class BlogUpdateModel : BlogModel
    {
        [Required]
        public int id { get; set; }

    }
    public class BlogKat
    {
        public int kat_id { get; set; }
        public string kat_ad { get; set; }
    }
    public class BlogCommentItem
    {
        public string comment { get; set; }
    }
}
