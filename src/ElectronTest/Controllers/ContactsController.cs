using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElectronTest.Models;
using Newtonsoft.Json;
using System.Text;

namespace ElectronTest.Controllers
{
    public class ContactsController : Controller
    {
        private string _apiBaseUrl = "http://localhost:5000/api/contacts/";

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) })
            {
                return View(JsonConvert.DeserializeObject<List<Contact>>(await (await client.GetAsync("")).Content.ReadAsStringAsync()));
            }
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) })
            {
                var contact = JsonConvert.DeserializeObject<Contact>(await (await client.GetAsync(id.ToString())).Content.ReadAsStringAsync());

                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
            }
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address,City,Email,Name,Phone,PostalCode,State")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) })
                {
                    await client.PostAsync("", new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json"));
                }

                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) })
            {
                var contact = JsonConvert.DeserializeObject<Contact>(await (await client.GetAsync(id.ToString())).Content.ReadAsStringAsync());

                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
            }
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,City,Email,Name,Phone,PostalCode,State")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) })
                {
                    await client.PutAsync(id.ToString(), new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json"));
                }
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) })
            {
                var contact = JsonConvert.DeserializeObject<Contact>(await (await client.GetAsync(id.ToString())).Content.ReadAsStringAsync());

                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
            }

        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) })
            {
                await client.DeleteAsync(id.ToString());
                return RedirectToAction("Index");
            }
        }

        private async Task<bool> ContactExists(int id)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) })
            {
                return JsonConvert.DeserializeObject<Contact>(await (await client.GetAsync("id")).Content.ReadAsStringAsync()) != null;
            }
        }
    }
}