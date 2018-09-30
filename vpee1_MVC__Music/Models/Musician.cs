using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vpee1_MVC__Music.Models
{
    public class Musician
    {
        public Musician()
        {

            this.Performances = new HashSet<Performance>();
            this.Plays = new HashSet<Plays>();

        }

        public int MusicianID { get; set; }

        [Display(Name = "Musician")]
        public string FullName
        {
            get
            {
                return FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)MiddleName[0]
                    + ". ").ToUpper()) + LastName;
            }
        }
        [Display(Name = "First Name")]
        [Required(ErrorMessage="You must enter a first name")]
        [StringLength(30, ErrorMessage ="First name can only be 30 characters long")]
        public String FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(30, ErrorMessage = "Middle name can only be 30 characters long")]
        public String MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter a last name")]
        [StringLength(50, ErrorMessage = "Last name can only be 50 characters long")]
        public String LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "You must enter a phone number")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Phone Number must be 10 digits")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString ="{0:(###) ###-####}", ApplyFormatInEditMode = false)]
        public Int64 PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "You must enter a DOB")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Display(Name = "SI Number")]
        [Required(ErrorMessage = "You must enter an SIN")]
        [RegularExpression("^\\d{9}$", ErrorMessage = "SIN must contain 10 digits")]
        public String SIN { get; set; }

        [Display(Name = "Instrument ID")]
        [Required(ErrorMessage = "You must enter an instrument ID")]
        public int InstrumentID { get; set; }

        public virtual Instrument Instrument { get; set; }

        public virtual ICollection<Performance> Performances { get; set; }
        public virtual ICollection<Plays> Plays { get; set; }
    }
}
