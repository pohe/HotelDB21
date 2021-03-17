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
    public class EditModel : PageModel
    {
        [BindProperty]
        public Hotel Hotel { get; set; }

        IHotelService hotelService;
        public EditModel(IHotelService service)
        {
            this.hotelService = service;
        }
        public async Task OnGetAsync(int id)
        {
            Hotel = await hotelService.GetHotelFromIdAsync(id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await hotelService.UpdateHotelAsync(Hotel, Hotel.HotelNr);
            return RedirectToPage("GetAllHotels");
        }
    }
}
