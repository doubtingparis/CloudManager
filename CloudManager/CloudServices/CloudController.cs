using CloudManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudManager.CloudServices
{
    interface CloudController
    {

        // Cloud service device operations

        // Create new device
        Task<bool> CreateDevice(Device d);

        // Delete device
        Task<bool> DeleteDevice(Device d);

        // Edit device
        Task<bool> EditDevice(Device d);

        // Ping the device
        // Task<bool> CheckConnection(Device d);

        // Return the device connection string
        Task<string> GetConnectionString(Device d);
    }
}
