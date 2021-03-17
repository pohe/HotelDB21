using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelDBConsole21.Models;
using HotelDBConsole21.Services;

namespace HotelDBConsole21
{
    public static class MainMenu
    {

        public static void showOptions()
        {
            Console.Clear();
            Console.WriteLine("Vælg et menupunkt");
            Console.WriteLine("1) List hoteller");
            Console.WriteLine("1a) List hoteller async");
            Console.WriteLine("2) Opret nyt Hotel");
            Console.WriteLine("2a) Opret nyt Hotel async");
            Console.WriteLine("3) Fjern Hotel");
            Console.WriteLine("4) Søg efter hotel udfra hotelnr");
            Console.WriteLine("5) List alle værelser");
            Console.WriteLine("6) List alle til et bestemt hotel");
            Console.WriteLine("7) Opret nyt værelse");
            Console.WriteLine("8) Fjern et værelse");
            Console.WriteLine("9) Søg efter et givent værelse");
            Console.WriteLine("10) Opdater et værelse");
            Console.WriteLine("11) kommer snart");
            Console.WriteLine("12) kommer snart");
            Console.WriteLine("Q) Afslut");
        }

        public static bool Menu()
        {
            showOptions();
            switch (Console.ReadLine())
            {
                case "1":
                    ShowHotels();
                    return true;
                case "1a":
                    ShowHotelsAsync();
                    DoSomething();
                    return true;
                case "2":
                    CreateHotel();
                    return true;
                case "2a":
                    CreateHotelAsync();
                    DoSomething();
                    return true;
                case "3":
                    DeleteHotel();
                    return true;
                case "Q": 
                case "q": return false;
                default: return true;
            }

        }

        private static void DoSomething()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i + " i GUI");
            }
        }

        private static void DeleteHotel()
        {
            Console.Clear();
            Console.WriteLine("Fjern hotel");
            Console.WriteLine("Indlæs hotelnr:");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            HotelService hs = new HotelService();
            Hotel h = hs.DeleteHotel(hotelnr);
            if (h != null)
                Console.WriteLine($"Hotellet blev fjernet");
            else
            {
                Console.WriteLine("Hotellet blev ikke fjernet");
            }
        }

        private static void CreateHotel()
        {
            Console.Clear();
            Console.WriteLine("Opret hotel");
            Console.WriteLine("Indlæs hotelnr:");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs hotel navn:");
            string navn = Console.ReadLine();
            Console.WriteLine("Indlæs hotel adresse:");
            string adresse = Console.ReadLine();
            HotelService hs = new HotelService();
            bool ok = hs.CreateHotel(new Hotel(hotelnr, navn, adresse));
            if (ok)
                Console.WriteLine("Hotellet blev oprettet");
            else
            {
                Console.WriteLine("Hotellet blev ikke oprettet");
            }
        }

        private async static Task CreateHotelAsync()
        {
            Console.Clear();
            Console.WriteLine("Opret hotel");
            Console.WriteLine("Indlæs hotelnr:");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs hotel navn:");
            string navn = Console.ReadLine();
            Console.WriteLine("Indlæs hotel adresse:");
            string adresse = Console.ReadLine();
            HotelServiceAsync hs = new HotelServiceAsync();
            bool ok = await hs.CreateHotelAsync(new Hotel(hotelnr, navn, adresse));
            if (ok)
                Console.WriteLine("Hotellet blev oprettet");
            else
            {
                Console.WriteLine("Hotellet blev ikke oprettet");
            }
        }

        private async static Task ShowHotelsAsync()
        {
            Console.Clear();
            HotelServiceAsync hs = new HotelServiceAsync();
            List<Hotel> hotels =  await hs.GetAllHotelAsync();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.HotelNr} Address {hotel.Adresse}");
            }
        }

        private static void ShowHotels()
        {
            Console.Clear();
            HotelService hs = new HotelService();
            List<Hotel> hotels = hs.GetAllHotel();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr {hotel.HotelNr} Name {hotel.HotelNr} Address {hotel.Adresse}");
            }
        }
    }
}
