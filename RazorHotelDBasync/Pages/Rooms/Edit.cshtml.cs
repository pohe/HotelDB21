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
    public class EditModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; }// = new Room();

        //[BindProperty(SupportsGet = true)]
        //public int HotelNr { get; set; }

        IRoomService roomService;
        
        [BindProperty]
        public bool EditResult { get; set; }

        public EditModel(IRoomService rService)
        {
            this.roomService = rService;
        }
        public async Task<IActionResult> OnGetAsync(int rid, int hid)
        {
            Room = await roomService.GetRoomFromIdAsync(rid, hid);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EditResult = await roomService.UpdateRoomAsync(Room, Room.RoomNr, Room.HotelNr );
            //return RedirectToPage("GetAllRooms", HotelNr);
            return Page();
        }
    }
}
