using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediaoManage.Data;
using MediaoManage.Models;
using System.Security.Policy;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage;
using Azure.Identity;
using System.Reflection.Metadata;

namespace MediaoManage.Pages.Medias
{
    public class CreateModel : PageModel
    {
        private readonly MediaoManage.Data.MediaoManageContext _context;
        public  string blobContainerName = "movie";
        public  string StorageConnectionString = "UseDevelopmentStorage=true";
        //const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=youtubedev;AccountKey=YVHIVv+u2BzVxYPagFotweQNV+9uotA41Oswq+DHX24ALGgPd7ugq2bVu30tRJkLvN3hCvV7PQM3+AStHVNC/Q==;EndpointSuffix=core.windows.net";
        public  BlobContainerClient blobContainer { get; set; }
        public CreateModel(MediaoManage.Data.MediaoManageContext context)
        {
            _context = context;
            blobContainer = new BlobContainerClient(StorageConnectionString, blobContainerName);
            blobContainer.CreateIfNotExists();
        }

        public IActionResult OnGet()
        {    
            return Page();
        }

        [BindProperty]
        public UserMedia UserMedia { get; set; } = default!;

        [BindProperty]

        public IFormFile file { get; set; }
        [BindProperty]

        public IFormFile thumb_nail { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {          
            if (!ModelState.IsValid || _context.UserMedia == null || UserMedia == null||file==null||thumb_nail==null)
            {
                return Page();
            }
            BlobClient blobClient = blobContainer.GetBlobClient(UserMedia.media_url);
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }
            BlobClient blobClient1 = blobContainer.GetBlobClient(UserMedia.media_thumbnail_url);
            using (var stream = thumb_nail.OpenReadStream())
            {
                await blobClient1.UploadAsync(stream);
            }
            _context.UserMedia.Add(UserMedia);
            await _context.SaveChangesAsync();  
            return RedirectToPage("./Index");
        }
    }
}
