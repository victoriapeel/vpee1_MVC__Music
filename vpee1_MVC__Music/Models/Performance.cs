using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vpee1_MVC__Music.Models
{
    public class Performance
    {
        [Display(Name = "Song")]
        [Required(ErrorMessage ="You must enter a song")]
        public int SongID { get; set; }

        public virtual Song Song { get; set; }

        [Display(Name = "Musician")]
        [Required(ErrorMessage = "You must enter a Musician")]
        public int MusicianID { get; set; }

        public virtual Musician Musician { get; set; }
    }
}
