using System;

// DELUXE ROOM
class DeluxeRoom : Room
{
    //Parameterized constructor to initialize the deluxe room with specific details
    public DeluxeRoom(int number)
        : base(number, "Deluxe", 2000)
    {
    }

    //Polymorphism: Override the DisplayRoom method to show room details in a specific format
    public override void DisplayRoom()
    {
        Console.WriteLine($"{RoomNumber} | {Type} | {Price} | {Status}");
    }
}
