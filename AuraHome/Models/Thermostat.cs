using AuraHome.Interfaces;

namespace AuraHome.Models
{
    public class Thermostat : SmartDevice, IClimateControl
    {
        public double Temperature { get; private set; } = 22.0;

        public Thermostat(string name) : base(name) { }

        public void SetTemperature(double temp)
        {
            Temperature = Math.Round(temp, 1);
            Console.WriteLine($"[CLIMATE] {Name} target temperature: {Temperature}°C");
        }

        public void IncreaseTemp() => SetTemperature(Temperature + 0.5);
        public void DecreaseTemp() => SetTemperature(Temperature - 0.5);

        public override string GetStatus() => $"[AC] {Name} | Power: {(IsOn ? "ON" : "OFF")} | Temp: {Temperature}°C";
    }
}
