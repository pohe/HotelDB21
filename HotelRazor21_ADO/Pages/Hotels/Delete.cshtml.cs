using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelRazor21_ADO.Interfaces;
using HotelRazor21_ADO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelRazor21_ADO.Pages.Hotels
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Hotel Hotel { get; set; }
        IHotelService hotelService;
        public DeleteModel(IHotelService service)
        {
            this.hotelService = service;
        }
        public void OnGetAsync(int id)
        {
            Hotel = hotelService.GetHotelFromId(id);
        }

        public IActionResult OnPostAsync()
        {
           hotelService.DeleteHotel(Hotel.HotelNr);
            return RedirectToPage("GetAllHotels");
        }
    }
}
