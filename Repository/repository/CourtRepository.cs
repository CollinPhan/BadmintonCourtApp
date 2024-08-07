﻿using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class CourtRepository : BaseRepository<BadmintonCourt>
    {
        private readonly DBContext _context;
        public CourtRepository(DBContext context) : base(context)
        {
            _context = context;
        }
        public List<BadmintonCourt> getAll()
        {
            var huh = _context.BadmintonCourts.Include(c => c.VenueServiceTimes).Include(c => c.Location);
            return huh.ToList();
        }
        public List<BadmintonCourt> getByFilter(int id, String locationname, String courtname)
        {
            List<BadmintonCourt> court;
            if (id == 0)
            {
                court = _context.BadmintonCourts.Include(c => c.VenueServiceTimes).Include(c => c.Location)
                .Where(s => s.Location.Name.Contains(locationname)
                && s.CourtName.Contains(courtname)).ToList();
            }
            else
            {
                court= _context.BadmintonCourts.Include(c => c.VenueServiceTimes).Include(c => c.Location)
                                .Where(s => s.CourtId == id 
                                && s.Location.Name.Contains(locationname) 
                                && s.CourtName.Contains(courtname)).ToList();
            }
            
            return court;
        }

        public List<VenueServiceTime> getAllV(int id)
        {
            return _context.VenueServiceTimes.Include(c => c.TimeSlot).Where(c => c.CourtId == id).ToList();
        }
        public bool IsTimeSlotBooked(int courtId, int timeSlotId, DateTime date)
        {
            var targetDate = DateOnly.FromDateTime(date);
            return _context.Bookings
                .Include(b => b.BookingSlots)
                .Any(b => b.CourtId == courtId &&
                          b.BookingSlots.Any(bs => bs.Vstid == timeSlotId && bs.BookDate == targetDate));
        }
        public List<Location> getAllLocation()
        {
            return _context.Locations.ToList();
        }
        public TimeSlot GetTimeSlotById(int id)
        {
            return _context.TimeSlots.FirstOrDefault(c => c.TimeSlotId == id);
        }

        public void AddBooking(Booking newBooking)
        {
            _context.Bookings.Add(newBooking);
            _context.SaveChanges();
        }
    }
}
