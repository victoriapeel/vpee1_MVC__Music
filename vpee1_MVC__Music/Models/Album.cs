using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vpee1_MVC__Music.Models
{
    public class Album
    {
        public Album()
        {
            this.Songs = new HashSet<Song>();
        }
        public int AlbumID { get; set; }

        [Display(Name = "Album Name")]
        [Required(ErrorMessage = "You must enter an album name")]
        [StringLength(50, ErrorMessage = "Album name can only be 50 characters long")]
        public string Name { get; set; }

        [Display(Name = "Year Produced")]
        [Required(ErrorMessage = "You must enter a date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime YearProduced { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage ="You must enter a price")]
        [Range(1, 200000, ErrorMessage = "Price must be between $1 and $200,000")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "You must enter a genre")]
        [Display(Name = "Genre")]
        public int GenreID { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
