using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vpee1_MVC__Music.Models
{
    public class Song
    {
        public Song()
        {

            this.Performances = new HashSet<Performance>();

        }
        public int SongID { get; set; }

        [Display(Name = "Song Title")]
        [Required(ErrorMessage = "You must enter a title")]
        [StringLength(80, ErrorMessage = "Album name must be less than 80 characters long")]
        public string Title { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "You must enter a genre")]
        public int GenreID { get; set; }

        public virtual Genre Genre { get; set; }

        [Display(Name = "Album")]
        [Required(ErrorMessage = "You must enter an album")]
        public int AlbumID { get; set; }

        public virtual Album Album { get; set; }

        public virtual ICollection<Performance> Performances { get; set; }
    }
}
