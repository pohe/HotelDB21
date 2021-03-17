using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelRazor21_ADO.Interfaces;
using HotelRazor21_ADO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelRazor21_ADO.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; } = new Room();

        [BindProperty(SupportsGet = true)]
        public int HotelNr { get; set; } 

        IRoomService roomService;
        IHotelService hotelService;

        public CreateModel(IRoomService rService, IHotelService hService)
        {
            this.roomService = rService;
            hotelService = hService;
        }
        public IActionResult OnGet(int id)
        {
            //Room.HotelNr = id;
            HotelNr = id; 
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //roomService.CreateRoom(Room.HotelNr, Room);
            roomService.CreateRoom(HotelNr, Room);
            return RedirectToPage("GetAllRooms");
        }
    }
}
