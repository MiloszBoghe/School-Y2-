using Microsoft.AspNetCore.Mvc;
using PasswordApp.Web.Models;
using PasswordApp.Web.Services.Contracts;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PasswordApp.Web.Controllers
{
    
    public class EntriesController : Controller
    {
        private readonly IEntryService _entryService;
        private readonly IConverter _converter;
        public EntriesController(IEntryService entryService, IConverter converter)
        {
            _entryService = entryService;
            _converter = converter;
        }
        public async Task<IActionResult> Index()
        {
            //DONE: implement the test for this method and make it green
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var entries = await _entryService.GetEntriesOfUserAsync(Guid.Parse(id));
            var modelEntries = entries.Select(entry => _converter.ConvertTo<EntryListItemViewModel>(entry)).ToList();

            return View(modelEntries);
        }

        [HttpGet("Entries/Edit/{Id}")]
        public IActionResult Edit(Guid id)
        {
            //DONE: implement the test for this method and make it green
            var entry = _entryService.GetById(id);
            return View(new EntryEditViewModel { Password = entry.Password, Url = entry.Url });
        }

        [HttpPost("Entries/Edit/{Id}")]
        public IActionResult Edit(Guid id, EntryEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //DONE: use model binding techniques to validate the input model
            //DONE: update using the repository and send the user to the overview (of entries) page.
            _entryService.Update(id, model.Password, model.Url);
            return RedirectToAction(nameof(Index));
        }
    }
}