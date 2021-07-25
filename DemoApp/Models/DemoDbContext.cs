using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoApp.Models
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext() : base("MyConnectionString")
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }    
}