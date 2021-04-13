using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorHotelDBasync.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Hotel
    {
        public int HotelNr { get; set; }
        public String Navn { get; set; }
        public String Adresse { get; set; }
       
        public Hotel()
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hotelNr">
        /// </param>
        /// <param name="navn">
        /// </param>
        /// <param name="adresse">
        /// </param>
        public Hotel(int hotelNr, string navn, string adresse)
        {
            HotelNr = hotelNr;
            Navn = navn;
            Adresse = adresse;
            //Rooms = new List<Room>();
        }

        public override string ToString()
        {
            //String RoomCnt = Rooms.Count.ToString();
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(Navn)}: {Navn}, {nameof(Adresse)}: {Adresse}";
        }
    }
}
