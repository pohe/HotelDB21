using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelRazor21_ADO.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime DatoFra { get; set; }
        public DateTime DatoTil { get; set; }
        public Room ForRoom { get; set; }
        public Guest ForGuest { get; set; }

        public Booking()
        {
        }

        public Booking(int id, DateTime datoFra, DateTime datoTil, Room forRoom, Guest forGuest)
        {
            Id = id;
            DatoFra = datoFra;
            DatoTil = datoTil;
            ForRoom = forRoom;
            ForGuest = forGuest;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(DatoFra)}: {DatoFra}, {nameof(DatoTil)}: {DatoTil}, {nameof(ForRoom)}: {ForRoom}, {nameof(ForGuest)}: {ForGuest}";
        }
    }
}
