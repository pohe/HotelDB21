﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelRazor21_ADO.Interfaces;
using HotelRazor21_ADO.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HotelRazor21_ADO.Services
{
    public class RoomService : Connection, IRoomService
    {
        public const bool WithRooms = true;
        public const bool WithOutRooms = false;

        private String queryString = "select * from Room where Hotel_no = @HNo";
        private String queryStringFromID = "select * from Room where Hotel_no = @HNo AND Room_No = @ID";
        private String insertSql = "insert into Room Values (@ID, @HNo, @RType, @RPrice)";
        private String deleteSql = "delete from Room where Hotel_no = @HNo AND Room_No = @ID";
        private String updateSql = "update Room " +
                                   "set Room_No= @RoomID, Types=@RType, Price=@RPris " +
                                   "where Hotel_no = @HNo AND Room_No = @ID";

        public RoomService(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Room> GetAllRoom(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@HNo", hotelNr);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int roomNr = reader.GetInt32(0);
                    int hotel_nr = reader.GetInt32(1);
                    string types = reader.GetString(2);
                    double price = reader.GetDouble(3);
                    Room room = new Room(roomNr, types[0], price, hotel_nr);
                    rooms.Add(room);
                }
            }
            return rooms;
        }


        public Room GetRoomFromId(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Parameters.AddWithValue("@HNo", hotelNr);
                command.Parameters.AddWithValue("@ID", roomNr);

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return ReadRoom(reader);
                }

            }


            return null;
        }

        public bool CreateRoom(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@HNo", hotelNr);
                command.Parameters.AddWithValue("@ID", room.RoomNr);
                command.Parameters.AddWithValue("@RType", room.Types);
                command.Parameters.AddWithValue("@RPrice", room.Pris);


                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return true;
                }
                return false;

                // return (noOfRows == 1)
            }
        }

        public bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);
                command.Parameters.AddWithValue("@HNo", hotelNr);
                command.Parameters.AddWithValue("@ID", roomNr);
                command.Parameters.AddWithValue("@RoomID", room.RoomNr);
                command.Parameters.AddWithValue("@RType", room.Types);
                command.Parameters.AddWithValue("@RPris", room.Pris);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return true;
                }
                return false;

            }
        }

        public bool UpdateRoom(Room room, int roomNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);
                command.Parameters.AddWithValue("@ID", roomNr);
                command.Parameters.AddWithValue("@RoomID", room.RoomNr);
                command.Parameters.AddWithValue("@HNo", room.HotelNr);
                command.Parameters.AddWithValue("@RType", room.Types);
                command.Parameters.AddWithValue("@RPris", room.Pris);
                command.Connection.Open();
                int noOfRows = command.ExecuteNonQuery();
                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public Room DeleteRoom(int roomNr, int hotelNr)
        {
            Room room = GetRoomFromId(roomNr, hotelNr);
            if (room == null)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteSql, connection);
                command.Parameters.AddWithValue("@HNo", hotelNr);
                command.Parameters.AddWithValue("@ID", roomNr);

                command.Connection.Open();

                int noOfRows = command.ExecuteNonQuery();

                if (noOfRows == 1)
                {
                    return room;
                }
                return null;
            }
        }


        private static Room ReadRoom(SqlDataReader reader)
        {
            //int roomNr = reader.GetInt32((int)RoomField.RoomNo);
            //String s = reader.GetString((int)RoomField.Types);
            //char roomType = s[0];
            //double roomPris = reader.GetDouble((int)RoomField.Price);
            //int hotelNr = reader.GetInt32((int)RoomField.HotelNo);

            //ManageHotel hotelManager = new ManageHotel();
            //Hotel hotel = hotelManager.GetHotelFromId(hotelNr, WithOutRooms);
            //Room room = new Room(roomNr, roomType, roomPris, hotel);
            //return room;
            return null;
        }
    }
}
