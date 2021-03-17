using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Models
{
    public class Room
    {
        public int RoomNr { get; set; }
        public char Types { get; set; }
        public double Pris { get; set; }
        public Hotel InHotel { get; set; }

        public Room()
        {

        }

        public Room(int nr, char types, double pris)
        {
            RoomNr = nr;
            Types = types;
            Pris = pris;
        }

        public Room(int nr, char types, double pris, Hotel hotel) : this(nr, types, pris)
        {
            InHotel = hotel;
        }

        public override string ToString()
        {
            return $"Room = {RoomNr}, Types = {Types}, Pris = {Pris}";
        }
    }
}
