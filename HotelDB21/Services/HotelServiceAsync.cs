using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    class HotelServiceAsync: Connection, IHotelServiceAsync
    {

        private String queryString = "select * from Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private String insertSql = "insert into Hotel Values (@ID, @Navn, @Adresse)";
        private String deleteSql = "delete from Hotel where Hotel_No = @ID";
        private String updateSql = "update Hotel " +
                                   "set Hotel_No= @HotelID, Name=@Navn, Address=@Adresse " +
                                   "where Hotel_No = @ID";

        
        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                using(SqlCommand command = new SqlCommand(queryString, connection))
                {
                    await command.Connection.OpenAsync();

                    Thread.Sleep(1000);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (reader.Read())
                    {
                            int hotelNr = reader.GetInt32(0);
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);

                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);

                            hoteller.Add(hotel);
                    }
                }
                
            }
            return hoteller;
        }

        public Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                command.Parameters.AddWithValue("@Navn", hotel.Navn);
                command.Parameters.AddWithValue("@Adresse", hotel.Adresse);

                await command.Connection.OpenAsync();
                Thread.Sleep(2000);
                int noOfRows = await command.ExecuteNonQueryAsync();

                if (noOfRows == 1)
                {
                    bool OK = true;
                    return OK;
                }
                return false;
            }
        }

        public Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            throw new NotImplementedException();
        }

        public Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
