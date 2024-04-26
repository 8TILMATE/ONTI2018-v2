using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONTI2018v_.Models
{
    public class LectiiModel
    {
        public int IdLectie { get; set; }
        public int IdUser { get; set; }
        public string TitluLectie { get; set; }
        public string Regiune { get; set; }
        public DateTime DataCreare { get; set; }
        public string NumeImagine { get; set; }
    }
}
