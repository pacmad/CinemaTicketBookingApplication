using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public partial class SeatReservationViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }
    }
}
