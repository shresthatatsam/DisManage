using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace dmSyatem.Controllers
{
    public class VictimController : Controller
    {
        private readonly IVictimService _victimService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public VictimController(IVictimService victimService, IHttpContextAccessor httpContextAccessor) 
        {
            _victimService = victimService;
            _httpContextAccessor = httpContextAccessor;
        }   

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
       public async Task<IActionResult> Create(Victim victim , IFormFile ppSizePhotoFile)
        {
            if (ModelState.IsValid)
            {
                if (ppSizePhotoFile != null && ppSizePhotoFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await ppSizePhotoFile.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        victim.PPSizePhoto = Convert.ToBase64String(fileBytes);
                    }
                }
                var createdOrUpdatedVictim = _victimService.Create(victim);
                _httpContextAccessor.HttpContext?.Session.SetString("VictimId", createdOrUpdatedVictim.Id.ToString());
                return RedirectToAction("Index", "Disaster");
            }

            return View(victim);  
        }

        public IActionResult Edit(Guid id)
        {
            Victim victim = _victimService.getData(id);
            return View(victim);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Victim victim, IFormFile ppSizePhotoFile)
        {
            if (ModelState.IsValid)
            {
                if (ppSizePhotoFile != null && ppSizePhotoFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await ppSizePhotoFile.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        victim.PPSizePhoto = Convert.ToBase64String(fileBytes);
                    }
                }
                var createdOrUpdatedVictim = _victimService.Create(victim);
                _httpContextAccessor.HttpContext?.Session.SetString("VictimId", createdOrUpdatedVictim.Id.ToString());
                return RedirectToAction("Edit", "Disaster");
            }

            return View(victim);
        }

        public async Task<IActionResult> Delete(Victim victim)
        {
            _victimService.Delete(victim.Id);
            return RedirectToAction("Index");
        }


        }
}
