using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vpee1_MVC__Music.Models
{
    public class Album: Auditable, IValidatableObject
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
        [Required(ErrorMessage = "You cannot leave the Year Produced blank.")]
        [RegularExpression("^\\d{4}$", ErrorMessage = "The Year Produced must be entered as exactly 4 numeric digits.")]
        [StringLength(4)]//DS Note: we only include this to limit the size of the database field to 10
        public string YearProduced { get; set; }

        [ScaffoldColumn(false)]
        public string AlbumSummary
        {
            get
            {
                return Name + " - " + YearProduced;
                    
            }
        }

        [Display(Name = "Price")]
        [Required(ErrorMessage ="You must enter a price")]
        [Range(1.0, 200000.00, ErrorMessage = "Price must be between $1 and $200,000")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [ScaffoldColumn(false)]
        [Timestamp]
        public Byte[] RowVersion { get; set; }

        [Required(ErrorMessage = "You must enter a genre")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Genre")]
        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }

        public virtual ICollection<Song> Songs { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if((DateTime.Today.Year - DateTime.Parse(YearProduced).Year) > -1)
            {
                yield return new ValidationResult("Year Produced cannot be more than 1 year in the future.", new[] { "Year Produced" });
            }

        }
    }
}
