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
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Hotel Hotel { get; set; }

        IHotelService hotelService;
        public CreateModel(IHotelService service)
        {
            this.hotelService = service;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            hotelService.CreateHotel(hotel);
            return RedirectToPage("GetAllHotels");
        }
    }
}
