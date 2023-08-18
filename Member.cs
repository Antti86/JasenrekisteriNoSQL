using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasenrekisteriNoSQL
{
    public class Member
    {

        public Member(string etunimi, string sukunimi, string osoite, string postinumero,
            string puhelin, string sahkoposti, DateTime jasenyydenAlkuPvm) 
        {
            Etunimi = etunimi;
            Sukunimi = sukunimi;
            Osoite = osoite;
            Postinumero = postinumero;
            Puhelin = puhelin;
            Sahkoposti = sahkoposti;
            JasenyydenAlkuPvm = jasenyydenAlkuPvm;
        }

        public string Etunimi {get; set; }

        public string Sukunimi { get; set; }

        public string Osoite { get; set; }

        public string Postinumero { get; set; }

        public string Puhelin { get; set; }

        public string Sahkoposti { get; set; }

        public DateTime JasenyydenAlkuPvm { get; set; }



    }
}
