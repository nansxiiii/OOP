Hotel Reservation Console App
=============================

Description
-----------
Simple console-based hotel reservation system implemented in C#. The app presents a menu to list rooms, make/view/cancel reservations, manage customers, and perform check-in/check-out.

Requirements
------------
- .NET SDK (6.0 or later recommended) or Visual Studio 2022/2026 with .NET workload

Run (Visual Studio)
-------------------
1. Open the `OOP` folder or the solution in Visual Studio.
2. Set the project containing `Program.cs` as Startup Project.
3. Build (Ctrl+Shift+B) and Run (F5 or Ctrl+F5).

Run (dotnet CLI)
----------------
1. Open a terminal in the project folder that contains the `.csproj` file (for example the folder with `Program.cs`).
2. Run:

```
dotnet build
dotnet run
```

Usage
-----
- The program shows a numbered menu. Enter the number for the action and press Enter.
- Common actions:
  - `1` List Rooms
  - `2` Make Reservation
  - `3` View Reservations
  - `4` Cancel Reservation
  - `5` List Customers
  - `6` Customer Info
  - `7` Check In
  - `8` Check Out
  - `9` Exit

Testing (manual)
----------------
- Start the app and choose `1` to verify rooms load correctly.
- Create a reservation (`2`) using valid customer and room inputs, then `3` to confirm it appears.
- Try canceling (`4`) a reservation and verify it's removed.
- Use `7` and `8` to test check-in/check-out flows.

Troubleshooting
---------------
- If compilation fails, ensure all source files (`Program.cs`, `HotelSystem.cs`, `Room.cs`, `Customer.cs`, `StandardRoom.cs`, `DeluxeRoom.cs`) are present in the project and included in the `.csproj`.
- If you prefer an immediate exit instead of returning to the menu, replace the exit logic with `Environment.Exit(0);` in `Program.cs`.

Contact
-------
This repository was edited locally. For further changes, open the project in Visual Studio or edit the source files directly.
