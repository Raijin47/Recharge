using System;
using System.Collections.Generic;

[Serializable]
public class ApplicationStatusDetector
{
    private readonly List<Phone> ActiveDevice = new();

    public void AddDevice(Phone device) => ActiveDevice.Add(device);

    public void RemoveDevice(Phone device) => ActiveDevice.Remove(device);

    public bool Check()
    {
        foreach(Phone device in ActiveDevice)
        {
            if (!device.IsComplated) return false;
        }
        return true;
    }
}