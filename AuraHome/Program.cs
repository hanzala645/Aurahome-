using AuraHome.Interfaces;
using AuraHome.Models;

namespace AuraHome
{
    class Program
    {
        static List<Room> House = new List<Room>();

        static void Main(string[] args)
        {
            SetupHouse();
            RunMainMenu();
        }

        static void SetupHouse()
        {
            var livingRoom = new Room("Living Room");
            livingRoom.AddDevice(new SmartLight("Main Chandelier"));
            livingRoom.AddDevice(new Thermostat("HVAC Unit"));
            livingRoom.AddDevice(new SecurityCamera("Entry Cam"));

            var bedroom = new Room("Master Bedroom");
            bedroom.AddDevice(new SmartLight("Bedside Lamp"));
            
            House.Add(livingRoom);
            House.Add(bedroom);
        }

        static void WriteHeader(string text, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n" + new string('=', 45));
            Console.WriteLine($"   {text.ToUpper()}");
            Console.WriteLine(new string('=', 45));
            Console.ResetColor();
        }

        static void RunMainMenu()
        {
            while (true)
            {
                Console.Clear();
                WriteHeader("AuraHome Ecosystem Console", ConsoleColor.Magenta);
                
                Console.WriteLine("\nSELECT A ROOM TO MANAGE:");
                for (int i = 0; i < House.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($" [{i + 1}] ");
                    Console.ResetColor();
                    Console.WriteLine($"{House[i].Name.PadRight(15)} | {House[i].Devices.Count} Connected Devices");
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n [Q] Shutdown System");
                Console.ResetColor();
                Console.Write("\nTarget Index > ");
                
                string input = Console.ReadLine()?.ToUpper() ?? "";
                if (input == "Q") break;

                if (int.TryParse(input, out int index) && index > 0 && index <= House.Count)
                    HandleRoomMenu(House[index - 1]);
            }
        }

        static void HandleRoomMenu(Room room)
        {
            while (true)
            {
                Console.Clear();
                WriteHeader($"ROOM: {room.Name}", ConsoleColor.Blue);
                
                Console.WriteLine("DEVICE DASHBOARD:");
                for (int i = 0; i < room.Devices.Count; i++)
                {
                    var dev = room.Devices[i];
                    Console.Write($" [{i + 1}] ");
                    
                    // Style based on power state
                    Console.ForegroundColor = dev.IsOn ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.Write(dev.IsOn ? "[ACTIVE] " : "[OFF]    ");
                    Console.ResetColor();
                    
                    Console.WriteLine(dev.GetStatus());
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n [B] Return to Hallway");
                Console.ResetColor();
                Console.Write("\nControl Device > ");

                string input = Console.ReadLine()?.ToUpper() ?? "";
                if (input == "B") break;

                if (int.TryParse(input, out int index) && index > 0 && index <= room.Devices.Count)
                    HandleDeviceControl(room.Devices[index - 1]);
            }
        }

        static void HandleDeviceControl(ISmartDevice device)
        {
            while (true)
            {
                Console.Clear();
                WriteHeader($"CONTROL: {device.Name}", ConsoleColor.Yellow);
                
                Console.Write("Status: ");
                Console.ForegroundColor = device.IsOn ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(device.GetStatus());
                Console.ResetColor();

                Console.WriteLine("\nAVAILABLE OPERATIONS:");
                Console.WriteLine(" [1] POWER ON");
                Console.WriteLine(" [2] POWER OFF");

                if (device is IDimmable) Console.WriteLine(" [3] ADJUST BRIGHTNESS");
                if (device is IClimateControl) 
                {
                    Console.WriteLine(" [4] INCREASE TEMP (+0.5)");
                    Console.WriteLine(" [5] DECREASE TEMP (-0.5)");
                }
                if (device is ISecurity) Console.WriteLine(" [6] TOGGLE RECORDING");

                Console.WriteLine("\n [B] BACK");
                Console.Write("\nOperation > ");

                string cmd = Console.ReadLine()?.ToUpper() ?? "";
                if (cmd == "B") break;

                Console.ForegroundColor = ConsoleColor.Cyan;
                switch (cmd)
                {
                    case "1": device.TurnOn(); break;
                    case "2": device.TurnOff(); break;
                    case "3":
                        if (device is IDimmable d) {
                            Console.Write("New Intensity (0-100): ");
                            if (int.TryParse(Console.ReadLine(), out int lvl)) d.SetBrightness(lvl);
                        }
                        break;
                    case "4": (device as IClimateControl)?.IncreaseTemp(); break;
                    case "5": (device as IClimateControl)?.DecreaseTemp(); break;
                    case "6": (device as ISecurity)?.ToggleRecording(); break;
                }
                Console.ResetColor();

                Console.WriteLine("\nPress any key to refresh...");
                Console.ReadKey();
            }
        }
    }
}
