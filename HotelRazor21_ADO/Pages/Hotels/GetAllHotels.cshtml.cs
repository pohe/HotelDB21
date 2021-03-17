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
    public class GetAllHotelsModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Hotel> Hotels { get; private set; }

        private IHotelService hotelService;

        public GetAllHotelsModel(IHotelService hService)
        {
            this.hotelService = hService;
        }
        public async Task OnGetAsync()
        {
            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Hotels =  hotelService.GetHotelsByName(FilterCriteria);
            }
            else
                Hotels =  hotelService.GetAllHotel();
        }

    }
}
