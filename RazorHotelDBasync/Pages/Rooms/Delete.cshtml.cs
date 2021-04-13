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
    public class DeleteModel : PageModel
    {
        [BindProperty] public Room Room { get; set; }
        IRoomService roomService;

        public DeleteModel(IRoomService service)
        {
            this.roomService = service;
        }

        [BindProperty] public string InfoText { get; set; } = "";

        public async Task OnGetAsync(int rid, int hid)
        {
            Room = await roomService.GetRoomFromIdAsync(rid, hid);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Room returnRoom = await roomService.DeleteRoomAsync(Room.RoomNr, Room.HotelNr);
            if (returnRoom != null)
                return RedirectToPage("GetAllRooms", "MyRooms", new {cid = Room.HotelNr});
            else
            {
                InfoText = "Error in delete";
                return Page();
            }
        }
    }
}
