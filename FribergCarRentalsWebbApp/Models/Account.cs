﻿namespace FribergCarRentalsWebbApp.Models
{
    public abstract class Account
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}
