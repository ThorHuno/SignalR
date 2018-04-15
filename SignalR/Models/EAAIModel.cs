using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SignalR.Models
{
    public class EAAIModel: DbContext
    {
        public EAAIModel() : base("name=EAAIContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Vuelo> Vuelos { get; set; }
    }
}