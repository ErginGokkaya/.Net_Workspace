using Microsoft.AspNetCore.Mvc;
using Note_Taking_MVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Note_Taking_MVC.Controllers
{
    public class NotesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NotesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("NotesApi");
            // API'den notları çekme işlemi burada yapılabilir

            var response = await client.GetAsync("/notes");
            if (response.IsSuccessStatusCode)
            {
                var notes = await response.Content.ReadFromJsonAsync<List<Note>>();
                return View(notes);
            }
            else
            {
                // Hata durumunda boş bir liste döndürülebilir veya hata işleme yapılabilir
                return View(new List<Note>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("NotesApi");

                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(note),
                                                System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/notes", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the note.");
                }
            }
            return View(note);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("NotesApi");
            var response = await client.GetAsync($"/notes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var note = await response.Content.ReadFromJsonAsync<Note>();
                return View(note);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
                var client = _httpClientFactory.CreateClient("NotesApi");
                var response = await client.GetAsync($"/notes/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
 
                }
                var existingNote = await response.Content.ReadFromJsonAsync<Note>();

                return View(existingNote);  
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Note note)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("NotesApi");

                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(note),
                                                System.Text.Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"/notes/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the note.");
                }
            }
            return View(note);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("NotesApi");
            var response = await client.DeleteAsync($"/notes/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return StatusCode((int)response.StatusCode, "An error occurred while deleting the note.");
            }
        }
    }
}
