using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TestScreenshot
{
    static class DeviceManager
    {
        public static List<DisplayDevice> GetDisplayDevices()
        {
            SafeNativeMethods.DISPLAY_DEVICE device = new SafeNativeMethods.DISPLAY_DEVICE();
            device.cb = Marshal.SizeOf(device);

            List<DisplayDevice> devices = new List<DisplayDevice>();
            for (uint id = 0; SafeNativeMethods.EnumDisplayDevices(null, id, ref device, 0); id++)
            {
                devices.Add(ConvertToDisplayDevice(device));
                device.cb = Marshal.SizeOf(device);
            }

            return devices;
        }

        private static DisplayDevice ConvertToDisplayDevice(SafeNativeMethods.DISPLAY_DEVICE device)
        {
            return new DisplayDevice()
            {
                DeviceID = device.DeviceID,
                DeviceKey = device.DeviceKey,
                DeviceName = device.DeviceName,
                DeviceString = device.DeviceString,
            };
        }
    }

    class DisplayDevice
    {
        public string DeviceName { get; set; }
        public string DeviceString { get; set; }
        public string DeviceID { get; set; }
        public string DeviceKey { get; set; }
    }
}
