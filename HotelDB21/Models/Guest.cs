using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Models
{
    public class Guest
    {
        public int GuestNr { get; set; }
        public String Navn { get; set; }
        public String Adresse { get; set; }

        public Guest(int guestNr, string navn, string adresse)
        {
            GuestNr = guestNr;
            Navn = navn;
            Adresse = adresse;
        }

        public Guest()
        {
        }

        public override string ToString()
        {
            return $"{nameof(GuestNr)}: {GuestNr}, {nameof(Navn)}: {Navn}, {nameof(Adresse)}: {Adresse}";
        }
    }
}
