using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vpee1_MVC__Music.Models
{
    public class Plays
    {
       
        public int InstrumentID { get; set; }
        public virtual Instrument Instrument { get; set; }

        public int MusicianID { get; set; }
        public virtual Musician Musician { get; set; }
    }
}
