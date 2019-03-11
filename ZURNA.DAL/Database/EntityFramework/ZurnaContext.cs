using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZURNA.DAL.Table;

namespace ZURNA.DAL.Database.EntityFramework
{
    public class ZurnaContext:DbContext
    {
        /// <summary>
        /// Migrations Not Created !
        /// </summary>
        public ZurnaContext():base("Data Source=.;Initial Catalog=ZurnaDB_2019;Integrated Security=SSPI;")
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
