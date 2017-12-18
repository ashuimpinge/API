using BooksAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BooksAPI.Data
{
    public class BooksContext : DbContext
    {
        public BooksContext() : base("DbConnection")
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}