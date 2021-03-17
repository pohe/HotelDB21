using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelRazor21_ADO.Interfaces;
using HotelRazor21_ADO.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HotelRazor21_ADO.Services
{
    public class HotelService : Connection, IHotelService
    {
        private String queryString = "select * from Hotel";
        private String queryNameString = "select * from Hotel where  Hotel_No like @Navn";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private String insertSql = "insert into Hotel Values (@ID, @Navn, @Adresse)";
        private String deleteSql = "delete from Hotel where Hotel_No = @ID";
        private String updateSql = "update Hotel " +
                                   "set Hotel_No= @HotelID, Name=@Navn, Address=@Adresse " +
                                   "where Hotel_No = @ID";

        //private IRoomService roomService;

        
        public HotelService(IConfiguration configuration): base(configuration)
        {
            //roomService = new RoomService(configuration);
        }


        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hoteller = new List<Hotel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int hotelNr = reader.GetInt32(0);
                    String hotelNavn = reader.GetString(1);
                    String hotelAdr = reader.GetString(2);

                    Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);

                    // hent alle værelser til hotellet
                    //hotel.Rooms = roomService.GetAllRoom(hotelNr);

                    hoteller.Add(hotel);
                }

            }
            return hoteller;
        }

        

        public Hotel GetHotelFromId(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Parameters.AddWithValue("@ID", hotelNr);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int hNr = reader.GetInt32(0);
                    String hotelNavn = reader.GetString(1);
                    String hotelAdr = reader.GetString(2);

                    Hotel hotel = new Hotel(hNr, hotelNavn, hotelAdr);
                    return hotel;
                }
            }

            return null;
        }

        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                command.Parameters.AddWithValue("@Navn", hotel.Navn);
                command.Parameters.AddWithValue("@Adresse", hotel.Adresse);

                command.Connection.Open();
                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    bool OK = true;
                    //foreach (Room room in hotel.Rooms)
                    //{
                    //    if (!roomManager.CreateRoom(hotel.HotelNr, room))
                    //        OK = false;
                    //}
                    return OK;
                }
                return false;
            }
        }

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);
                command.Parameters.AddWithValue("@HotelID", hotel.HotelNr);
                command.Parameters.AddWithValue("@Navn", hotel.Navn);
                command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                command.Parameters.AddWithValue("@ID", hotelNr);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    bool OK = true;
                    //foreach (Room room in hotel.Rooms)
                    //{
                    //    if (!roomManager.UpdateRoom(room, room.RoomNr, hotelNr))
                    //        OK = false;
                    //}
                    return OK;
                }
                return false;

            }
        }

        public Hotel DeleteHotel(int hotelNr)
        {
            Hotel hotel = GetHotelFromId(hotelNr);
            if (hotel == null)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteSql, connection);
                command.Parameters.AddWithValue("@ID", hotelNr);

                command.Connection.Open();
                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return hotel;
                }

                return null;
            }
        }


        public List<Hotel> GetHotelsByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
