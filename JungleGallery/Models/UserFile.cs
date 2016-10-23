using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JungleGallery.Models
{
    public class UserFile
    {
        public int UserFileID { get; set; }


        [StringLength(255)]
        public string FileName { get; set; }


        [StringLength(10)]
        public string FileType { get; set; }

    }
}