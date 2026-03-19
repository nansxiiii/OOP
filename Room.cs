using System;

/////////////////////////////////////////////////////
// ENUM FOR ROOM STATUS
/////////////////////////////////////////////////////
enum RoomStatus { Available, Reserved }

/////////////////////////////////////////////////////
// ABSTRACTION
/////////////////////////////////////////////////////
abstract class Room
{
    private int roomNumber;

    // Encapsulation: Property to control access to room number
    public int RoomNumber
    {
        // Encapsulation: Getter and setter with validation for room number
        get { return roomNumber; }
        set{
            if (value > 0)
                roomNumber = value;
        }
    }

    // Encapsulation: Properties to control access to room details
    public string Type { get; set; }
    public double Price { get; set; }
    public RoomStatus Status { get; set; }

    //Parameterized constructor to initialize the room with specific details
    public Room(int number, string type, double price)
    {
        RoomNumber = number;
        Type = type;
        Price = price;
        // Encapsulation: Set the initial status of the room to Available
        Status = RoomStatus.Available;
    }

    public abstract void DisplayRoom();
}
