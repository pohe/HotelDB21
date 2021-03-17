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
    public class GetAllRoomsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int MaxAmount { get; set; }
        public List<Room> Rooms { get; private set; }

        [BindProperty]
        public Hotel Hotel { get; set; }

        IRoomService roomService;
        public GetAllRoomsModel(IRoomService service)
        {
            this.roomService = service;
        }
        //public void OnGetAsync()
        //{
        //    Rooms = roomService.GetAllRoom(1);
        //}
        public void OnGetMyRooms(int cid)
        {
            Rooms = roomService.GetAllRoom(cid);
        }
    }
}
