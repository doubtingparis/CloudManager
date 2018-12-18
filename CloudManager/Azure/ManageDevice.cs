using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudManager.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;


namespace CloudManager.Azure
{
    public class ManageDevice
    {
        // Ref. to Azure SDK object
        static RegistryManager registryManager;

        // IoT Hub connection string (different from device key)
        static readonly string connectionString = "HostName=cld-mgr-iot-hub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=cBNuOJEEiw01xWyPZAM9SYriPua3UHTqsk19eZozmh4=";


        public void ConnectCloud()
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
        }
        
        

        // Add device to cloud
        private static async Task<bool> AddDeviceAsync(string DeviceID)
        {
            var deviceID = DeviceID;
            Microsoft.Azure.Devices.Device device;
            try
            {
                //success
                device = await registryManager.AddDeviceAsync(new Microsoft.Azure.Devices.Device(deviceID));
                return true;
            }
            catch (DeviceAlreadyExistsException)
            {
                //already exists
                return false;
            }
        }

        // Return the device connection string
        public async Task<string> GetDeviceAuthKey(string DeviceID)
        {
            try
            {
                //Device found, return value
                Microsoft.Azure.Devices.Device localDevice = await registryManager.GetDeviceAsync(DeviceID);
                return localDevice.Authentication.SymmetricKey.PrimaryKey;
            }
            catch(DeviceNotFoundException)
            {
                return "Device not found";
            }
        }
        
        //testing method
        private static async Task TestAddDeviceAsync(string DeviceID)
        {
            var deviceID = DeviceID;
            Microsoft.Azure.Devices.Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Microsoft.Azure.Devices.Device(deviceID));
                Console.WriteLine("Device created.");
            }
            catch (DeviceAlreadyExistsException)
            {
                Console.WriteLine("Device exists.. retrieving information.");
                device = await registryManager.GetDeviceAsync(deviceID);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
