using System;
using System.Threading.Tasks;
using CloudManager.Models;

namespace CloudManager.CloudServices
{
    public class AWSCloud : ICloud
    {

        // Class implemented for theoretical showcase.
        // In a full solution, this would be a decent way to open up for multiple 
        // cloud providers, so a customer would be able to choose different hosts.


        // Construct
        public AWSCloud(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        // Host URL
        private static string connectionString;



        // Not implemented methods

        public Task<bool> CreateDevice(Device d)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDevice(Device d)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditDevice(Device d)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetConnectionString(Device d)
        {
            throw new NotImplementedException();
        }
    }
}
