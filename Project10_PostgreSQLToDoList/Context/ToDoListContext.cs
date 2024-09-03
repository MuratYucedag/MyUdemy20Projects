using Project10_PostgreSQLToDoList.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project10_PostgreSQLToDoList.Context
{
    public class ToDoListContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public ToDoListContext():base("MyPostgreConnection")
        {
            
        }
    }
}
