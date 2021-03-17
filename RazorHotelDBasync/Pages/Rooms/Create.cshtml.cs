using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDBasync.Interfaces;
using RazorHotelDBasync.Models;

namespace RazorHotelDBasync.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; }// = new Room();

        [BindProperty(SupportsGet = true)]
        public int HotelNr { get; set; }

        IRoomService roomService;
        //IHotelService hotelService;

        [BindProperty]
        public bool createResult { get; set; }

        //public CreateModel(IRoomService rService, IHotelService hService)
        public CreateModel(IRoomService rService)
        {
            this.roomService = rService;
            //hotelService = hService;
        }
        public IActionResult OnGet(int id)
        {
            //Room.HotelNr = id;
            HotelNr = id;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            createResult = await roomService.CreateRoomAsync(HotelNr, Room);
            //return RedirectToPage("GetAllRooms", HotelNr);
            return Page();
        }
    }
}
