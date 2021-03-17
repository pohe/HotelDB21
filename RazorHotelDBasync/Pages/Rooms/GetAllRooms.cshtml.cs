using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RazorHotelDBasync.Interfaces;
using RazorHotelDBasync.Models;

namespace RazorHotelDBasync.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int MaxAmount { get; set; }
        public List<Room> Rooms { get; private set; }

        [BindProperty]
        public int Hotel_nr { get; set; }

        IRoomService roomService;
        public GetAllRoomsModel(IRoomService service)
        {
            this.roomService = service;
            
        }


        public async Task OnGetMyRooms(int cid)
        {
            await getData(cid);
        }

        private async Task getData(int cid)
        {
            Hotel_nr = cid;
            Rooms = await roomService.GetAllRoomAsync(cid);
        }

        public async Task<IActionResult> OnPostDelete(int rid, int hid)
        {
            //
            await roomService.DeleteRoomAsync(rid, hid);
            Rooms = await roomService.GetAllRoomAsync(hid);
            return Page();
        }
    }
}
