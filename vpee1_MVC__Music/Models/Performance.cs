using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vpee1_MVC__Music.Models
{
    public class Performance
    {
        
        public int SongID { get; set; }
        public virtual Song Song { get; set; }

        public int MusicianID { get; set; }
        public virtual Musician Musician { get; set; }
    }
}
