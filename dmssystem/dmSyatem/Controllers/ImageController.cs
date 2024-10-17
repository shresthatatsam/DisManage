using DataAccess.Service;
using DataAccess.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace dmSyatem.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImage _image;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ImageController(IImage image, IHttpContextAccessor httpContextAccessor)
        {
            _image = image;  
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var VictimId = Guid.Parse(_httpContextAccessor?.HttpContext?.Session?.GetString("VictimId"));
            Image data = _image.getData(VictimId);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Image image, List<IFormFile> imageFiles)
        {
            // Retrieve VictimId and DisasterId from session
            var victimIdString = _httpContextAccessor.HttpContext?.Session?.GetString("VictimId");
            var disasterIdString = _httpContextAccessor.HttpContext?.Session?.GetString("DisasterId");

            if (string.IsNullOrEmpty(victimIdString) || string.IsNullOrEmpty(disasterIdString))
            {
                return RedirectToAction("Error", "Home"); // Handle the error if session data is missing
            }

            Guid victimId = Guid.Parse(victimIdString);
            Guid disasterId = Guid.Parse(disasterIdString);

            if (victimId == Guid.Empty || disasterId == Guid.Empty)
            {
                return RedirectToAction("Error", "Home"); // Handle the error if session data is invalid
            }

            var imageEntities = new List<Image>();

            foreach (var file in imageFiles)
            {
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();
                        var base64Image = Convert.ToBase64String(fileBytes);

                        var imageEntity = new Image
                        {
                            ImageUrl = base64Image,  // Store image as Base64
                            Description = image.Description,  // Use the provided description
                            DisasterDate = image.DisasterDate != default ? image.DisasterDate : DateTime.Now,  // Use provided date or set default to now
                            VictimId = victimId,
                            DisasterId = disasterId
                        };

                        imageEntities.Add(imageEntity);
                    }
                }
            }

            // If no images were added, return an error or a message
            if (!imageEntities.Any())
            {
                ModelState.AddModelError(string.Empty, "No images selected.");
                return View(image);
            }

            // Save the images using the image service
            await _image.Create(imageEntities);

            // Redirect to another action, like Index
            return RedirectToAction("Index", "PrintService");
        }
    }
}
