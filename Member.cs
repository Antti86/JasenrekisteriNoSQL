using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasenrekisteriNoSQL
{
    public class Member
    {

        public Member(string id, string etunimi, string sukunimi, string osoite, string postinumero,
            string puhelin, string sahkoposti, string jasenyydenAlkuPvm) 
        {
            Id = id;
            Etunimi = etunimi;
            Sukunimi = sukunimi;
            Osoite = osoite;
            Postinumero = postinumero;
            Puhelin = puhelin;
            Sahkoposti = sahkoposti;
            JasenyydenAlkuPvm = jasenyydenAlkuPvm;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Etunimi {get; set; }

        public string Sukunimi { get; set; }

        public string Osoite { get; set; }

        public string Postinumero { get; set; }

        public string Puhelin { get; set; }

        public string Sahkoposti { get; set; }

        public string JasenyydenAlkuPvm { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is not Member objAsMember)
            {
                return false;
            }
            else
            {
                return Equals(objAsMember);
            }
        }
        public bool Equals(Member other)
        {
            if (other == null) 
            {
                return false;
            }
            return (this.Id.Equals(other.Id));
        }

        public override int GetHashCode()
        {
           return base.GetHashCode();
        }
    }
}
