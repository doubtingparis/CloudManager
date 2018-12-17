using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudManager.Models;
using CloudManager.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CloudManager.Controllers
{
    [Authorize]
    public class DevicesController : Controller
    {
        //DB ref
        private CloudManagerContext db;

        //constructor
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

            Device_ViewModel view = new Device_ViewModel
            {
                CustomerSelection = customer_selection
            };

            return View(view);
        }

        // POST: Devices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,DeviceID,AuthKey,Date")] Device device)
        {
            device.AuthKey = "abc123_src_from_devicecontroller"; //Key from Azure IOT
            device.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Add(device);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(device);
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
            
            IEnumerable<Customer> customer_selection = db.Customer.ToList();

            Device_ViewModel view = new Device_ViewModel
            {
                CustomerSelection = customer_selection,
                Device = device                
            };

            return View(view);
        }

        // POST: Devices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,DeviceID,AuthKey")] Device device)
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

            var device = await db.Device
                .FirstOrDefaultAsync(m => m.DeviceID == id);
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
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return db.Device.Any(e => e.DeviceID == id);
        }
    }
}
