﻿using CloudManager.Models;
using System.Threading.Tasks;

namespace CloudManager.CloudServices
{
    interface ICloud
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

        // Return device connection string
        Task<string> GetConnectionString(Device d);

        // Ping device
        // Task<bool> CheckConnection(Device d);
    }
}
