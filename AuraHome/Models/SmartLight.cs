using AuraHome.Interfaces;

namespace AuraHome.Models
{
    public class SmartLight : SmartDevice, IDimmable
    {
        public int Brightness { get; private set; } = 100;

        public SmartLight(string name) : base(name) { }

        public void SetBrightness(int level)
        {
            Brightness = Math.Clamp(level, 0, 100);
            Console.WriteLine($"[LIGHT] {Name} brightness set to {Brightness}%.");
        }

        public override string GetStatus() => $"[Light] {Name} | Power: {(IsOn ? "ON" : "OFF")} | Brightness: {Brightness}%";
    }
}
