using System;
using System.Collections.Generic;

[Serializable]
public class ApplicationStatusDetector
{
    private readonly List<Device> ActiveDevice = new();

    public void AddDevice(Device device) => ActiveDevice.Add(device);

    public void RemoveDevice(Device device) => ActiveDevice.Remove(device);

    public bool Check()
    {
        foreach(Device device in ActiveDevice)
        {
            if (!device.IsComplated) return false;
        }
        return true;
    }
}