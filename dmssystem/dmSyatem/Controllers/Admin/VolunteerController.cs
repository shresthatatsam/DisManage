using DataAccess.Service.AdminInterface;
using DataAccess.Service.AdminService;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace dmSyatem.Controllers.Admin
{
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService _volunteerService;

        public VolunteerController(IVolunteerService volunteerService)
        {
            _volunteerService = volunteerService;
        }

        // GET: Index
        public IActionResult Index()
        {
            var volunteers = _volunteerService.GetAllVolunteers();
            return View(volunteers);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public IActionResult Create(Volunteer volunteer)
        {
            if (ModelState.IsValid)
            {
                volunteer.isactive = true;
                _volunteerService.AddVolunteer(volunteer);
                return RedirectToAction(nameof(Index));
            }
            return View(volunteer);
        }

        public IActionResult Edit(Guid id)
        {
            Volunteer Volunteer = _volunteerService.getData(id);
            return View(Volunteer);
        }




        [HttpPost]
        public IActionResult Edit(Volunteer Volunteer)
        {
            if (ModelState.IsValid)
            {
                _volunteerService.EditVolunteer(Volunteer);
                return RedirectToAction("Index");
            }
            return View(Volunteer);
        }


        public async Task<IActionResult> Delete(Volunteer Volunteer)
        {
            _volunteerService.Delete(Volunteer.Id);
            return RedirectToAction("Index");
        }

    }
}
