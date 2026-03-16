using System;
using System.Collections.Generic;

/////////////////////////////////////////////////////
// ENUM FOR ROOM STATUS
/////////////////////////////////////////////////////
enum RoomStatus
{
    Available,
    Reserved
}

/////////////////////////////////////////////////////
// ABSTRACTION
/////////////////////////////////////////////////////
abstract class Room
{
    private int roomNumber;

    public int RoomNumber
    {
        get { return roomNumber; }
        set
        {
            if (value > 0)
                roomNumber = value;
        }
    }

    public string Type { get; set; }
    public double Price { get; set; }
    public RoomStatus Status { get; set; }

    public Room(int number, string type, double price)
    {
        RoomNumber = number;
        Type = type;
        Price = price;
        Status = RoomStatus.Available;
    }

    public abstract void DisplayRoom();
}

/////////////////////////////////////////////////////
// STANDARD ROOM
/////////////////////////////////////////////////////
class StandardRoom : Room
{
    public StandardRoom(int number)
        : base(number, "Standard", 1200)
    {
    }

    public override void DisplayRoom()
    {
        Console.WriteLine($"{RoomNumber} | {Type} | {Price} | {Status}");
    }
}

/////////////////////////////////////////////////////
// DELUXE ROOM
/////////////////////////////////////////////////////
class DeluxeRoom : Room
{
    public DeluxeRoom(int number)
        : base(number, "Deluxe", 2000)
    {
    }

    public override void DisplayRoom()
    {
        Console.WriteLine($"{RoomNumber} | {Type} | {Price} | {Status}");
    }
}

/////////////////////////////////////////////////////
// CUSTOMER CLASS
/////////////////////////////////////////////////////
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

/////////////////////////////////////////////////////
// HOTEL SYSTEM
/////////////////////////////////////////////////////
class HotelSystem
{
    private List<Room> rooms = new List<Room>();
    private List<Customer> customers = new List<Customer>();

    private static DateOnly ReadDate(string label)
    {
        while (true)
        {
            Console.Write($"{label} (yyyy-MM-dd): ");
            string input = Console.ReadLine();

            if (DateOnly.TryParseExact(
                input,
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateOnly value
            ))
            {
                return value;
            }

            Console.WriteLine("Invalid date format. Try again");
        }
    }

    /////////////////////////////////////////////////////
    // INITIALIZE ROOMS
    /////////////////////////////////////////////////////
    public void InitializeRooms()
    {
        for (int i = 101; i <= 105; i++)
            rooms.Add(new StandardRoom(i));

        for (int i = 201; i <= 205; i++)
            rooms.Add(new DeluxeRoom(i));
    }

    public void ListRooms()
    {
        Console.WriteLine("\nROOM LIST");
        Console.WriteLine("Room | Type | Price | Status");

        foreach (var r in rooms)
            r.DisplayRoom();
    }

    private Room FindRoom(int roomNumber)
    {
        return rooms.Find(r => r.RoomNumber == roomNumber);
    }

    public void MakeReservation()
    {
        Customer c = new Customer();

        Console.Write("Name: ");
        c.Name = Console.ReadLine();

        Console.Write("Email: ");
        c.Email = Console.ReadLine();

        Console.Write("Address: ");
        c.Address = Console.ReadLine();

        c.CheckIn = ReadDate("Check In Date");
        c.CheckOut = ReadDate("Check Out Date");

        if (c.CheckOut <= c.CheckIn)
        {
            Console.WriteLine("Check-out must be after check-in.");
            return;
        }

        ListRooms();

        Console.Write("\nSelect Room: ");
        int roomNumber = Convert.ToInt32(Console.ReadLine());

        Room room = FindRoom(roomNumber);

        if (room == null || room.Status == RoomStatus.Reserved)
        {
            Console.WriteLine("Room not available.");
            return;
        }

        Console.WriteLine($"Room Type: {room.Type}");
        Console.WriteLine($"Price: {room.Price}");
        int nights = (c.CheckOut.ToDateTime(TimeOnly.MinValue) - c.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;
        c.TotalPrice = nights * room.Price;
        Console.WriteLine($"Nights: {nights}");
        Console.WriteLine($"Total Price: {c.TotalPrice}");

        Console.WriteLine("\nPayment Method");
        Console.WriteLine("1. Card");
        Console.WriteLine("2. Cash");

        int payment = Convert.ToInt32(Console.ReadLine());

        if (payment == 1)
        {
            Console.Write("Enter 6-digit PIN: ");
            string pin = Console.ReadLine();

            if (pin.Length != 6)
            {
                Console.WriteLine("Invalid PIN.");
                return;
            }

            Console.WriteLine("Payment Successful!");
        }
        else
        {
            Console.WriteLine("\n----- RECEIPT -----");
            Console.WriteLine($"Room: {room.RoomNumber}");
            Console.WriteLine($"Price per Night: {room.Price}");
            Console.WriteLine($"Nights: {nights}");
            Console.WriteLine($"Total: {c.TotalPrice}");
            Console.WriteLine("Payment: Cash");
            Console.WriteLine("-------------------");
        }

        room.Status = RoomStatus.Reserved;

        c.RoomNumber = roomNumber;
        c.ReservationDate = DateTime.Now;

        customers.Add(c);

        Console.WriteLine("Reservation Successful!");
    }

    /////////////////////////////////////////////////////
    // VIEW RESERVATIONS
    /////////////////////////////////////////////////////
    public void ViewReservations()
    {
        Console.WriteLine("\nRESERVED ROOMS");

        foreach (var c in customers)
        {
            Console.WriteLine($"Room {c.RoomNumber} reserved by {c.Name}");
            Console.WriteLine($"Check In at: {c.CheckIn}");
            Console.WriteLine($"Check Out at {c.CheckOut}");
        }
    }

    /////////////////////////////////////////////////////
    // CANCEL RESERVATION
    /////////////////////////////////////////////////////
    public void CancelReservation()
    {
        Console.Write("Enter Room Number: ");
        int number = Convert.ToInt32(Console.ReadLine());

        Room room = FindRoom(number);

        if (room == null || room.Status == RoomStatus.Available)
        {
            Console.WriteLine("No reservation found.");
            return;
        }

        room.Status = RoomStatus.Available;

        customers.RemoveAll(c => c.RoomNumber == number);

        Console.WriteLine("Reservation Cancelled.");
    }

    /////////////////////////////////////////////////////
    // LIST CUSTOMERS
    /////////////////////////////////////////////////////
    public void ListCustomers()
    {
        Console.WriteLine("\nCUSTOMERS");

        foreach (var c in customers)
        {
            Console.WriteLine($"{c.Name} | Room {c.RoomNumber}");
        }
    }

    /////////////////////////////////////////////////////
    // CUSTOMER INFO
    /////////////////////////////////////////////////////
    public void ShowCustomerInfo()
    {
        Console.Write("Enter Customer Name: ");
        string name = Console.ReadLine().ToLower();

        foreach (var c in customers)
        {
            if (c.Name.ToLower() == name)
            {
                Console.WriteLine("\nCustomer Info");
                Console.WriteLine($"Name: {c.Name}");
                Console.WriteLine($"Email: {c.Email}");
                Console.WriteLine($"Address: {c.Address}");
                Console.WriteLine($"Room: {c.RoomNumber}");
                Console.WriteLine($"Check In: {c.CheckIn:yyyy-MM-dd}");
                Console.WriteLine($"Check Out: {c.CheckOut:yyyy-MM-dd}");
                Console.WriteLine($"Date: {c.ReservationDate}");
                return;
            }
        }

        Console.WriteLine("Customer not found.");
    }
}

/////////////////////////////////////////////////////
// MAIN PROGRAM
/////////////////////////////////////////////////////
class Program
{
    static void Menu()
    {
        Console.WriteLine("\n===== HOTEL RESERVATION SYSTEM =====");
        Console.WriteLine("1. List Rooms");
        Console.WriteLine("2. Make Reservation");
        Console.WriteLine("3. View Reservations");
        Console.WriteLine("4. Cancel Reservation");
        Console.WriteLine("5. List Customers");
        Console.WriteLine("6. Customer Info");
        Console.WriteLine("7. Exit");
        Console.Write("Choice: ");
    }

    static void Main()
    {
        HotelSystem hotel = new HotelSystem();
        hotel.InitializeRooms();

        int choice;

        do
        {
            Menu();
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    hotel.ListRooms();
                    break;

                case 2:
                    hotel.MakeReservation();
                    break;

                case 3:
                    hotel.ViewReservations();
                    break;

                case 4:
                    hotel.CancelReservation();
                    break;

                case 5:
                    hotel.ListCustomers();
                    break;

                case 6:
                    hotel.ShowCustomerInfo();
                    break;
            }

        } while (choice != 7);
    }
}
