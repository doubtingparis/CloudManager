
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;


namespace CloudManager.CloudServices
{
    public class AzureCloud : ICloud
    {
        // Host URL
        private static string connectionString = 
            "HostName=cld-mgr-iot-hub.azure-devices.net;" +
            "SharedAccessKeyName=iothubowner;" +
            "SharedAccessKey=cBNuOJEEiw01xWyPZAM9SYriPua3UHTqsk19eZozmh4=";

        // Azure device CRUD manager
        RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connectionString);

        // Construct
        public AzureCloud(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        // GET device from cloud that matches the ID in the app DB
        private async Task<Device> GetDevice(string ID)
        {
            return await registryManager.GetDeviceAsync(ID);
        }

        // CREATE
        public async Task<bool> CreateDevice(Models.Device device)
        {
            var deviceID = device.DeviceID.ToString();
            Device localDevice;
            try
            {
                // Device added
                localDevice = await registryManager.AddDeviceAsync(new Device(deviceID));
                return true;
            }
            catch (DeviceAlreadyExistsException)
            {
                // Already exists
                return false;
            }
        }

        // DELETE
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

        // EDIT
        public async Task<bool> EditDevice(Models.Device device)
        {
            Device localDevice;
            try
            {
                // Successfully updated device
                localDevice = await GetDevice(device.DeviceID.ToString());
                await registryManager.UpdateDeviceAsync(localDevice);
                return true;
            }
            catch (DeviceNotFoundException)
            {
                return false;
            }
        }

        // GET device auth.key
        public async Task<string> GetConnectionString(Models.Device device)
        {
            try
            {
                // Fetch device
                var itemID = device.DeviceID.ToString();
                Device localDevice = await registryManager.GetDeviceAsync(itemID);

                // Get key
                var key = localDevice.Authentication.SymmetricKey.PrimaryKey;
                return key;
            }
            catch (DeviceNotFoundException)
            {
                return "Error! Device not found.";
            }
        }

        // Continuous ping method not available for Azure IoT without using a lot of data
        // https://feedback.azure.com/forums/321918-azure-iot/suggestions/31152514-allow-to-use-connectionstate-to-identify-conecte
        //public async Task<bool> CheckConnection(Models.Device device)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
