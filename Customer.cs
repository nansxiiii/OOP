using System;

// CUSTOMER CLASS
class Customer
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int RoomNumber { get; set; }

    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
    public double TotalPrice { get; set; }
    public DateTime ReservationDate { get; set; }
}
