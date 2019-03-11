using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZURNA.DAL.Table
{
    [Table("ZURNA.USER.TABLE")]
    public class Users
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public Location location { get; set; }
        public DateTime registerdate { get; set; }
        public string deviceid { get; set; }
        public DateTime lastactivitydate { get; set; }
        public bool isbanned { get; set; }
        public string phone { get; set; }
    }
    public class Location
    {
        public string lat { get; set; }
        public string lon { get; set; }
        public string country { get; set; }
        public string city { get; set; }
    }
}
