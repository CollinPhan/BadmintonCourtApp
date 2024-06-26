﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class BadmintonCourt
{
    public int CourtId { get; set; }

    public int? LocationId { get; set; }

    public string CourtName { get; set; }

    public int? Capacity { get; set; }

    public string Description { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Location Location { get; set; }

    public virtual ICollection<VenueServiceTime> VenueServiceTimes { get; set; } = new List<VenueServiceTime>();
}