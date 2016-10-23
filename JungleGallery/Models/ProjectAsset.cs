using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JungleGallery.Models
{
    public class ProjectAsset
    {
        [Key]
        public int ProjectAssetID { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Alphanumerics Only")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Details is required")]
        public string Details { get; set; }

        
        public string Location { get; set; }



        public int ProjectStoryID { get; set; }
        public virtual ProjectStory ProjectStory { get; set; }

        public static Models.ProjectAsset FromIQueryable<ProjectAsset>(IQueryable<Models.ProjectAsset> v)
        {
            throw new NotImplementedException();
        }
    }
}