using System;

/////////////////////////////////////////////////////
// STANDARD ROOM
/////////////////////////////////////////////////////
class StandardRoom : Room
{
    //Parameterized constructor to initialize the standard room with specific details
    public StandardRoom(int number)
        : base(number, "Standard", 1200)
    {
    }

    // Polymorphism: Override the DisplayRoom method to show room details in a specific format
    public override void DisplayRoom()
    {
        Console.WriteLine($"{RoomNumber} | {Type} | {Price} | {Status}");
    }
}
