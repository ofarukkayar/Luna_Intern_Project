using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Luna_Project_Example.Data.ApplicationDbContext
{
    public class cs : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
    
        public cs() : base("name=cs")
        {
        }

        public System.Data.Entity.DbSet<Luna_Project_Example.Models.Meter> Meters { get; set; }

        public System.Data.Entity.DbSet<Luna_Project_Example.Models.Subscriber> Subscribers { get; set; }

    }
}
