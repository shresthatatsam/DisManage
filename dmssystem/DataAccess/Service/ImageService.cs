using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Service.Interface;
using Models;

namespace DataAccess.Service
{
    public class ImageService:IImage
    {
        private readonly ApplicationDbContext _context;
        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Image getData(Guid id)
        {
            return _context.DisasterImages.Where(x => x.VictimId == id).FirstOrDefault();
        }
        public async Task<List<Image>> Create(List<Image> images)
        {
            if (images == null || !images.Any())
            {
                return null;
            }

            var victimId = images.First().VictimId;
            if (victimId == Guid.Empty)
            {
                return null; 
            }

            var existingImages = _context.DisasterImages
                                        .Where(d => d.VictimId == victimId)
                                        .ToList();


            if (existingImages.Any())
            {
                _context.DisasterImages.RemoveRange(existingImages);  
                await _context.SaveChangesAsync(); 
            }


            if (existingImages.Any())
            {
                foreach (var image in images)
                {
                    var existingImage = existingImages.FirstOrDefault(i => i.Id == image.Id);
                    if (existingImage != null)
                    {
                        existingImage.ImageUrl = image.ImageUrl ?? existingImage.ImageUrl;
                        existingImage.Description = image.Description ?? existingImage.Description;
                        existingImage.DisasterDate = image.DisasterDate != default ? image.DisasterDate : existingImage.DisasterDate;
                    }
                    else
                    {
                        _context.DisasterImages.Add(image);
                    }
                }

                await _context.SaveChangesAsync();
                return existingImages.Concat(images.Where(img => !existingImages.Any(e => e.Id == img.Id))).ToList();
            }
            else
            {
                await _context.DisasterImages.AddRangeAsync(images);
                await _context.SaveChangesAsync();
                return images;
            }
        }

    }
}
