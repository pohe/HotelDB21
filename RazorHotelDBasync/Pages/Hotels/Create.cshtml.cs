using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDBasync.Interfaces;
using RazorHotelDBasync.Models;

namespace RazorHotelDBasync.Pages.Hotels
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
        public async Task<IActionResult> OnPostAsync(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await hotelService.CreateHotelAsync(hotel);
            return RedirectToPage("GetAllHotels");
        }
    }
}
