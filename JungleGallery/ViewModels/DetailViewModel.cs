using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JungleGallery.Models;

namespace JungleGallery.ViewModels
{
    public class DetailViewModel
    {
        public ProjectStory ProjectStory { get; set; }
        public  List<ProjectAsset> ProjectAssets { get; set; }

    }
}