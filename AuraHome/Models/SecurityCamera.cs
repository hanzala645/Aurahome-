using AuraHome.Interfaces;

namespace AuraHome.Models
{
    public class SecurityCamera : SmartDevice, ISecurity
    {
        public bool IsRecording { get; private set; }

        public SecurityCamera(string name) : base(name) { }

        public void ToggleRecording()
        {
            IsRecording = !IsRecording;
            Console.WriteLine($"[SECURITY] {Name} is {(IsRecording ? "now RECORDING" : "STOPPED")}.");
        }

        public override string GetStatus() => $"[Cam] {Name} | Power: {(IsOn ? "ON" : "OFF")} | Recording: {(IsRecording ? "ACTIVE" : "IDLE")}";
    }
}
