using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudManager.Models;
using CloudManager.ViewModels;
using CloudManager.CloudServices;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace CloudManager.Controllers
{
    [Authorize]
    public class DevicesController : Controller
    {
        // Connection strings
        private static readonly string AzureConnectionString = 
            "HostName=cld-mgr-iot-hub.azure-devices.net;" +
            "SharedAccessKeyName=iothubowner;" +
            "SharedAccessKey=cBNuOJEEiw01xWyPZAM9SYriPua3UHTqsk19eZozmh4=";
        
        
        // DB ref
        private CloudManagerContext db;
        private ICloud cloud = new AzureCloud(AzureConnectionString);

        // Constructor
        public DevicesController(CloudManagerContext context)
        {
            db = context;
        }

        // GET: Devices
        public IActionResult Index()
        {
            var devices = db.Device.Include(x => x.Customer);
            return View(devices.ToList());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Device == id result with the customer name included
            Device device = await db.Device.Include(x => x.Customer).FirstOrDefaultAsync(x => x.DeviceID == id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            IEnumerable<Customer> customer_selection = db.Customer.ToList();

            Device view = new Device
            {
                CustomerSelection = customer_selection
            };

            return View(view);
        }

        // POST: Devices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,AuthKey,Date")] Device device)
        {
            Device deviceBuilder = device;

            deviceBuilder.Date = DateTime.Now;
            deviceBuilder.AuthKey = "temp";

            if (ModelState.IsValid)
            {
                // Create device in DB
                db.Add(deviceBuilder);
                await db.SaveChangesAsync();

                Device localDevice = await db.Device.LastAsync();

                // Create device in cloud
                Task<bool> t1 = cloud.CreateDevice(localDevice);
                Task.WaitAll(t1);

                // When creation is complete on cloud-side
                if (t1.Result)
                {
                    // Key from Azure IOT
                    Task<string> t2 = cloud.GetConnectionString(localDevice);
                    Task.WaitAll(t2);

                    localDevice.AuthKey = t2.Result;

                    db.Update(localDevice);
                    await db.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            //Not successful
            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await db.Device.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            
            //dropdown functionality
            IEnumerable<Customer> customer_selection = db.Customer.ToList();

            //initialize view
            //Device_ViewModel view = new Device_ViewModel
            //{
            //    CustomerSelection = customer_selection,
            //    Device = device
            //};

            device.CustomerSelection = customer_selection;

            return View(device);
        }

        // POST: Devices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeviceID,CustomerID,AuthKey,Date")] Device device)
        {
            if (id != device.DeviceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(device);
                    await db.SaveChangesAsync();
                    //testing access
                    System.Threading.Thread.Sleep(1000);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.DeviceID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Edit device in the cloud
                //Task<bool> t1 = cloud.EditDevice(device);
                //Task.WaitAll(t1);

                //if (t1.Result)

                return RedirectToAction(nameof(Index));
            }         
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await db.Device.FirstOrDefaultAsync(m => m.DeviceID == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await db.Device.FindAsync(id);

            db.Device.Remove(device);
            await db.SaveChangesAsync();


            // Edit device in cloud
            Task<bool> t1 = cloud.DeleteDevice(device);
            Task.WaitAll(t1);

            if (t1.Result)
            {
                //device deleted
            }            
            return RedirectToAction(nameof(Index));
        }

        // Does device exist in db?
        private bool DeviceExists(int id)
        {
            return db.Device.Any(e => e.DeviceID == id);
        }

        // Get device reference from DB by ID
        private async Task<Device> GetDeviceAsync(int id)
        {
            var device = await db.Device.FirstOrDefaultAsync(m => m.DeviceID == id);
            return device;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
