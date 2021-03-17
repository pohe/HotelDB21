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
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Hotel Hotel { get; set; }
        IHotelService hotelService;
        public DeleteModel(IHotelService service)
        {
            this.hotelService = service;
        }

        [BindProperty] public string InfoText { get; set; } = "";
        public async Task OnGetAsync(int id)
        {
            Hotel =await hotelService.GetHotelFromIdAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Hotel returnHotel = await hotelService.DeleteHotelAsync(Hotel.HotelNr);
            if (returnHotel!= null)
                return RedirectToPage("GetAllHotels");
            else
            {
                InfoText = "Error in delete";
                return Page();
            }
        }
    }
}
