using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Valentino_Marco.Models
{
    public class Ville
    {
        public string Nom { get; set; }
        public string Id { get; set; }
        public string Commentaire { get; set; }
        public string Cp { get; set; }
        public Pays Pays { get; set; }
    }
}
