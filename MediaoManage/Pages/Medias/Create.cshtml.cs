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

namespace MediaoManage.Pages.Medias
{
    public class CreateModel : PageModel
    {
        private readonly MediaoManage.Data.MediaoManageContext _context;
        const string blobContainerName = "movie";
        const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=youtubedev;AccountKey=YVHIVv+u2BzVxYPagFotweQNV+9uotA41Oswq+DHX24ALGgPd7ugq2bVu30tRJkLvN3hCvV7PQM3+AStHVNC/Q==;EndpointSuffix=core.windows.net";
        private BlobContainerClient blobContainer;
        public CreateModel(MediaoManage.Data.MediaoManageContext context)
        {
            _context = context;
            BlobServiceClient blobServiceClient = new BlobServiceClient(StorageConnectionString);
            blobContainer = blobServiceClient.GetBlobContainerClient(blobContainerName);
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserMedia UserMedia { get; set; } = default!;

        [BindProperty]

        public IFormFile file { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.UserMedia == null || UserMedia == null)
            {
                return Page();
            }
          if(file!=null)
            {
                BlobClient blobClient = blobContainer.GetBlobClient(UserMedia.media_url);
                await blobClient.UploadAsync(file.OpenReadStream(), true);
            }
            _context.UserMedia.Add(UserMedia);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
