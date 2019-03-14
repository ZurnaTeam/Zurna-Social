using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURNA.DAL.Table
{
    [Table("ZURNA.POST.TABLE")]
    public class Post
    {
        [Key]
        public Guid id { get; set; }
        public Content content { get; set; }
        public List<Comment> comments { get; set; }
        public Guid userid { get; set; }
    }
    public class Content
    {
        public string text { get; set; }
        public string image { get; set; }
        public string hashtag { get; set; }
        public Location location { get; set; }
        public DateTime time { get; set; }
        public long like { get; set; }
        public long dislike { get; set; }
        public long view { get; set; }
    }
    public class Comment
    {
        public string text { get; set; }
        public DateTime time { get; set; }
        public Guid userid { get; set; }
    }
}
