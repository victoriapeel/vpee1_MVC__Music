using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vpee1_MVC__Music.Models
{
    public class Instrument
    {
        public Instrument()
        {

            this.Plays = new HashSet<Plays>();
            this.Musicians = new HashSet<Musician>();

        }

        public int InstrumentID { get; set; }

        [Display(Name = "Instrument")]
        [Required(ErrorMessage = "You must enter an instrument name")]
        [StringLength(50, ErrorMessage = "Instrument name can only be 50 characters long")]
        public string Name { get; set; }

        public virtual ICollection<Musician> Musicians { get; set; }
        public virtual ICollection<Plays>Plays { get; set; }
}
}
