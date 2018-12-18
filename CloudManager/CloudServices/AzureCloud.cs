using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudManager.Models;

using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;


namespace CloudManager.CloudServices
{
    public class AzureCloud : CloudController
    {

        // IoT Hub connection URL
        private static readonly string connectionString = "HostName=cld-mgr-iot-hub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=cBNuOJEEiw01xWyPZAM9SYriPua3UHTqsk19eZozmh4=";

        // Ref. to Azure SDK object
        RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);

        // Construct
        public AzureCloud()
        {
        }

        private async Task<Microsoft.Azure.Devices.Device> GetDevice(string ID)
        {
            return await registryManager.GetDeviceAsync(ID);
        }

        public async Task<bool> CreateDevice(Models.Device device)
        {
            var deviceID = device.DeviceID.ToString();
            Microsoft.Azure.Devices.Device localDevice;
            try
            {
                // Device added
                localDevice = await registryManager.AddDeviceAsync(new Microsoft.Azure.Devices.Device(deviceID));
                return true;
            }
            catch (DeviceAlreadyExistsException)
            {
                // Already exists
                return false;
            }
        }

        public async Task<bool> DeleteDevice(Models.Device device)
        {
            try
            {
                // Device found, delete device from cloud and return true
                await registryManager.RemoveDeviceAsync(device.DeviceID.ToString());
                return true;
            }
            catch (DeviceNotFoundException)
            {
                return false;
            }
        }

        public async void EditDevice(Models.Device device)
        {
            var localDevice = await GetDevice(device.DeviceID.ToString());
            await registryManager.UpdateDeviceAsync(localDevice);
        }

        // Continuous ping method not available from Azure IoT without using a lot of data
        // https://feedback.azure.com/forums/321918-azure-iot/suggestions/31152514-allow-to-use-connectionstate-to-identify-conecte
        //public async Task<bool> CheckConnection(Models.Device device)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<string> GetConnectionString(Models.Device device)
        {
            try
            {
                // Device found, return key
                Microsoft.Azure.Devices.Device localDevice = await registryManager.GetDeviceAsync(device);
                var key = localDevice.Authentication.SymmetricKey.PrimaryKey;
                return key;
            }
            catch (DeviceNotFoundException)
            {
                return "Error! Device not found.";
            }
        }



        






        //testing method
        //private static async Task TestAddDeviceAsync(string DeviceID)
        //{
        //    var deviceID = DeviceID;
        //    Microsoft.Azure.Devices.Device device;
        //    try
        //    {
        //        device = await registryManager.AddDeviceAsync(new Microsoft.Azure.Devices.Device(deviceID));
        //        Console.WriteLine("Device created.");
        //    }
        //    catch (DeviceAlreadyExistsException)
        //    {
        //        Console.WriteLine("Device exists.. retrieving information.");
        //        device = await registryManager.GetDeviceAsync(deviceID);
        //    }
        //    Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        //}
    }
}
