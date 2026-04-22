using AuraHome.Interfaces;

namespace AuraHome.Models
{
    public class Room
    {
        public string Name { get; }
        public List<ISmartDevice> Devices { get; } = new List<ISmartDevice>();

        public Room(string name)
        {
            Name = name;
        }

        public void AddDevice(ISmartDevice device) => Devices.Add(device);
    }
}
