using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace JungleGallery.Models
{
    public class ContextDB : DbContext
    {
        public ContextDB():base("JungleGallery")
        {
            Configuration.ProxyCreationEnabled = false;
        }


        public DbSet<ProjectStory> ProjectStory { get; set; }
        public DbSet<ProjectAsset> ProjectAsset { get; set; }
        public DbSet<User> Users { get; set; }
 
    }
}