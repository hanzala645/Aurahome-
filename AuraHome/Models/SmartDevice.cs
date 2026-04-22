using AuraHome.Interfaces;

namespace AuraHome.Models
{
    public abstract class SmartDevice : ISmartDevice
    {
        public string Id { get; }
        public string Name { get; protected set; }
        public bool IsOn { get; protected set; }

        protected SmartDevice(string name)
        {
            Id = Guid.NewGuid().ToString().Substring(0, 5);
            Name = name;
            IsOn = false;
        }

        public virtual void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"[POWER] {Name} is now ON.");
        }

        public virtual void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"[POWER] {Name} is now OFF.");
        }

        public abstract string GetStatus();
    }
}
