namespace AuraHome.Interfaces
{
    public interface ISmartDevice
    {
        string Id { get; }
        string Name { get; }
        bool IsOn { get; }
        void TurnOn();
        void TurnOff();
        string GetStatus();
    }

    public interface IDimmable
    {
        int Brightness { get; }
        void SetBrightness(int level);
    }

    public interface IClimateControl
    {
        double Temperature { get; }
        void SetTemperature(double temp);
        void IncreaseTemp();
        void DecreaseTemp();
    }

    public interface ISecurity
    {
        bool IsRecording { get; }
        void ToggleRecording();
    }
}
