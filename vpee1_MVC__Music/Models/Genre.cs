using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vpee1_MVC__Music.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Albums = new HashSet<Album>();
            this.Songs = new HashSet<Song>();
        }
        public int GenreID { get; set; }

        [Display(Name = "Genre")]
        [Required (ErrorMessage ="You must enter a Genre Name")]
        [StringLength(50, ErrorMessage = "Genre Name can only be 50 characters long")]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
