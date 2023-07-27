using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MediaoManage.Data;
using MediaoManage.Models;
using Azure.Storage.Blobs;

namespace MediaoManage.Pages.Medias
{
    public class DetailsModel : PageModel
    {
        private readonly MediaoManage.Data.MediaoManageContext _context;
        public BlobContainerClient blobContainer { get; set; }
        public string blobContainerName = "movie";
        //public string StorageConnectionString = "UseDevelopmentStorage=true";
        const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=youtubedev;AccountKey=YVHIVv+u2BzVxYPagFotweQNV+9uotA41Oswq+DHX24ALGgPd7ugq2bVu30tRJkLvN3hCvV7PQM3+AStHVNC/Q==;EndpointSuffix=core.windows.net";
        public DetailsModel(MediaoManage.Data.MediaoManageContext context)
        {
            _context = context;
            blobContainer = new BlobContainerClient(StorageConnectionString, blobContainerName);
            blobContainer.CreateIfNotExists();
        }

      public UserMedia UserMedia { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserMedia == null)
            {
                return NotFound();
            }

            var usermedia = await _context.UserMedia.FirstOrDefaultAsync(m => m.Id == id);
            if (usermedia == null)
            {
                return NotFound();
            }
            else 
            {
                UserMedia = usermedia;
            }
            return Page();
        }
    }
}
