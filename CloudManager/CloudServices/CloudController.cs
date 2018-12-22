using CloudManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudManager.CloudServices
{
    interface CloudController
    {

        // Cloud service device operations.
        // Interface for different hosts that inherits from this controller.
        // Children must implement the methods of the interface.

        // Create new device
        Task<bool> CreateDevice(Device d);

        // Delete device
        Task<bool> DeleteDevice(Device d);

        // No edits necessary on the cloud server, edits are handled locally in CloudManager
        // Edit device
        Task<bool> EditDevice(Device d);

        // Ping the device
        // Task<bool> CheckConnection(Device d);

        // Return the device connection string
        Task<string> GetConnectionString(Device d);
    }
}
