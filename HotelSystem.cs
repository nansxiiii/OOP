using System;
using System.Collections.Generic;

// HOTEL SYSTEM
class HotelSystem
{
    // In-memory data storage
    private List<Room> rooms = new List<Room>();
    private List<Customer> customers = new List<Customer>();

    // INITIALIZE ROOMS
    public void InitializeRooms()
    {
        // Create standard rooms with room numbers 101 to 105 and add them to the rooms list
        for (int i = 101; i <= 105; i++)
            rooms.Add(new StandardRoom(i));

        // Create deluxe rooms with room numbers 201 to 205 and add them to the rooms list
        for (int i = 201; i <= 205; i++)
            rooms.Add(new DeluxeRoom(i));
    }

    // CHECK IN
    public void CheckIn()
    {
        // Prompt the user to enter the room number for check-in and read the input
        Console.Write("Enter Room Number for Check-In: ");
        int number = Convert.ToInt32(Console.ReadLine());

        // Find the customer reservation and the room based on the entered room number
        var c = customers.Find(x => x.RoomNumber == number);
        var room = FindRoom(number);

        // Check if a reservation exists for the specified room number and if the room is valid
        if (c == null || room == null)
        {
            Console.WriteLine("No reservation found.");
            return;
        }

        // Validate the check-in date against the current date to ensure that the check-in is not too early or after the reservation has expired
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        if (today < c.CheckIn)
        {
            Console.WriteLine($"Check-in is on: {c.CheckIn:yyyy-MM-dd}. Too early.");
            return;
        }

        if (today > c.CheckOut)
        {
            Console.WriteLine("Reservation has already expired.");
            return;
        }

        Console.WriteLine($"Welcome {c.Name}! Check-in successful for room {number}.");
    }

    // CHECK OUT
    public void CheckOut()
    {
        Console.Write("Enter Room Number for Check-Out: ");
        int number = Convert.ToInt32(Console.ReadLine());

        var room = FindRoom(number);
        var c = customers.Find(x => x.RoomNumber == number);

        if (c == null || room == null)
        {
            Console.WriteLine("No reservation found.");
            return;
        }

        // Free up the room and remove the customer record
        room.Status = RoomStatus.Available;
        customers.RemoveAll(x => x.RoomNumber == number);

        Console.WriteLine($"Check-out successful for room {number}.");
        Console.WriteLine($"Total charged: {c.TotalPrice}");
    }

    public void ListRooms()
    {
        Console.WriteLine("\nROOM LIST");
        Console.WriteLine("Room | Type | Price | Status");

        // Iterate through the list of rooms and call the DisplayRoom method for each room to show its details
        foreach (var r in rooms)
            r.DisplayRoom();
    }

    // Helper method to find a room by its room number
    private Room FindRoom(int roomNumber)
    {
        // Use the Find method of the rooms list to search for a room with the specified room number
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

        // Read and validate the check-in and check-out dates from the user
        c.CheckIn = DateHelper.ReadDate("Check In Date");
        c.CheckOut = DateHelper.ReadDate("Check Out Date");

        // Validate that the check-out date is after the check-in date
        if (c.CheckOut <= c.CheckIn)
        {
            Console.WriteLine("Check-out must be after check-in.");
            return;
        }

        ListRooms();

        // Prompt the user to select a room by entering its room number
        Console.Write("\nSelect Room: ");
        int roomNumber = Convert.ToInt32(Console.ReadLine());

        // Find the selected room using the FindRoom helper method
        Room room = FindRoom(roomNumber);

        // Check if the room is available for reservation
        if (room == null || room.Status == RoomStatus.Reserved)
        {
            Console.WriteLine("Room not available.");
            return;
        }

        Console.WriteLine($"Room Type: {room.Type}");
        Console.WriteLine($"Price: {room.Price}");

        // Calculate the number of nights based on the check-in and check-out dates and calculate the total price for the reservation
        int nights = (c.CheckOut.ToDateTime(TimeOnly.MinValue) - c.CheckIn.ToDateTime(TimeOnly.MinValue)).Days;
        c.TotalPrice = nights * room.Price;

        Console.WriteLine($"Nights: {nights}");
        Console.WriteLine($"Total Price: {c.TotalPrice}");

        // Prompt the user to select a payment method and process the payment accordingly
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

        // Iterate through the list of customers and display the reservation details for each customer,
        // including the room number, customer name, check-in date, and check-out date
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

    // CUSTOMER INFO
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
