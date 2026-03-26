using System;

// DATE HELPER (Check In / Check Out)
static class DateHelper
{
    // Reads a date from console using yyyy-MM-dd format and validates it
    public static DateOnly ReadDate(string label)
    {
        while (true)
        {
            // Prompt the user to enter a date with the specified label and format
            Console.Write($"{label} (yyyy-MM-dd): ");
            string input = Console.ReadLine();

            // Try to parse the input string into a DateOnly object using the specified format and culture settings
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
}
