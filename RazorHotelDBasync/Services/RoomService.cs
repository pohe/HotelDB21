using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorHotelDBasync.Interfaces;
using RazorHotelDBasync.Models;

namespace RazorHotelDBasync.Services
{
    public class RoomService:Connection, IRoomService
    {
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

        public async Task<List<Room>> GetAllRoomAsync(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@HNo", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = command.ExecuteReader();
                    while (await reader.ReadAsync())
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

        public async Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Parameters.AddWithValue("@HNo", hotelNr);
                command.Parameters.AddWithValue("@ID", roomNr);

                await command.Connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.Read())
                {
                    return ReadRoom(reader);
                }
            }
            return null;
        }
        
        private  Room ReadRoom(SqlDataReader reader)
        {
            int roomNr = reader.GetInt32(0);
            int hotelNr = reader.GetInt32(1);
            String s = reader.GetString(2);
            char roomType = s[0];
            double roomPris = reader.GetDouble(3);
            Room room = new Room(roomNr, roomType, roomPris, hotelNr);
            return room;
        }

        public async Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@HNo", hotelNr);
                command.Parameters.AddWithValue("@ID", room.RoomNr);
                command.Parameters.AddWithValue("@RType", room.Types);
                command.Parameters.AddWithValue("@RPrice", room.Pris);
                await command.Connection.OpenAsync();
                int noOfRows = await command.ExecuteNonQueryAsync();
                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);
                command.Parameters.AddWithValue("@HNo", hotelNr);
                command.Parameters.AddWithValue("@ID", roomNr);
                command.Parameters.AddWithValue("@RoomID", room.RoomNr);
                command.Parameters.AddWithValue("@RType", room.Types);
                command.Parameters.AddWithValue("@RPris", room.Pris);
                await command.Connection.OpenAsync();
                int noOfRows = await command.ExecuteNonQueryAsync();
                if (noOfRows == 1)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            Room room = await GetRoomFromIdAsync(roomNr, hotelNr);
            if (room == null)
            {
                return null;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteSql, connection);
                command.Parameters.AddWithValue("@HNo", hotelNr);
                command.Parameters.AddWithValue("@ID", roomNr);
                await command.Connection.OpenAsync();
                int noOfRows = await command.ExecuteNonQueryAsync();
                if (noOfRows == 1)
                {
                    return room;
                }
                return null;
            }
        }
    }
}

