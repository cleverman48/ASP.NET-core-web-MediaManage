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
        const string blobContainerName = "movie";
        const string StorageConnectionString = "UseDevelopmentStorage=true";
        //const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=youtubedev;AccountKey=YVHIVv+u2BzVxYPagFotweQNV+9uotA41Oswq+DHX24ALGgPd7ugq2bVu30tRJkLvN3hCvV7PQM3+AStHVNC/Q==;EndpointSuffix=core.windows.net";
        private BlobContainerClient blobContainer;
        public CreateModel(MediaoManage.Data.MediaoManageContext context)
        {
            _context = context;
            blobContainer = new BlobContainerClient(StorageConnectionString, blobContainerName);
            blobContainer.CreateIfNotExists();
        }

        public IActionResult OnGet()
        {
            //BlobServiceClient blobServiceClient = new BlobServiceClient(StorageConnectionString);
           // blobContainer = blobServiceClient.GetBlobContainerClient(blobContainerName);
           // blobContainer.CreateIfNotExistsAsync(PublicAccessType.Blob);
            return Page();
        }

        [BindProperty]
        public UserMedia UserMedia { get; set; } = default!;

        [BindProperty]

        public IFormFile file { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (file != null)
            {      
                string media_url = GetRandomBlobName(file.FileName);
                BlobClient blobClient = blobContainer.GetBlobClient(media_url);
                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream);
                }
                UserMedia.media_file_name = Path.GetFileNameWithoutExtension(file.FileName);
                UserMedia.media_file_type = Path.GetExtension(file.FileName);
                UserMedia.media_url = media_url;
                _context.UserMedia.Add(UserMedia);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            if (!ModelState.IsValid || _context.UserMedia == null || UserMedia == null)
            {
                return Page();
            }
            _context.UserMedia.Add(UserMedia);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }
    }
}
