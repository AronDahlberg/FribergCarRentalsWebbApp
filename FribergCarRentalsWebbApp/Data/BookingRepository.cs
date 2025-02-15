﻿using FribergCarRentalsWebbApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FribergCarRentalsWebbApp.Data
{
    public class BookingRepository(ApplicationDbContext context) : IBooking
    {
        private readonly ApplicationDbContext _context = context;

        public void Add(Booking booking)
        {
            _context.Add(booking);
        }

        public IEnumerable<Booking> All()
        {
            return _context.Bookings.AsEnumerable();
        }

        public Booking? Find(Expression<Func<Booking, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Booking? Get(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public Booking? EagerGet(int id)
        {
            return _context.Bookings
                    .Include(b => b.Car)
                    .Include(b => b.CustomerAccount)
                    .Include(b => b.Payments)
                    .FirstOrDefault(b => b.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Booking booking)
        {
            _context.Update(booking);
        }

        public IEnumerable<Booking> EagerAll()
        {
            return [.. _context.Bookings
                    .Include(b => b.Car)
                        .ThenInclude(c => c.Images)
                    .Include(b => b.Car)
                        .ThenInclude(c => c.Prices)
                    .Include(b => b.CustomerAccount)
                    .Include(b => b.Payments)];
        }
    }
}
