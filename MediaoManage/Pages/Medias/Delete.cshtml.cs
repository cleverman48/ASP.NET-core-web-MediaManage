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
    public class DeleteModel : PageModel
    {
        private readonly MediaoManage.Data.MediaoManageContext _context;
        public string blobContainerName = "movie";
        //public string StorageConnectionString = "UseDevelopmentStorage=true";
        const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=youtubedev;AccountKey=YVHIVv+u2BzVxYPagFotweQNV+9uotA41Oswq+DHX24ALGgPd7ugq2bVu30tRJkLvN3hCvV7PQM3+AStHVNC/Q==;EndpointSuffix=core.windows.net";
        public DeleteModel(MediaoManage.Data.MediaoManageContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UserMedia UserMedia { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserMedia == null)
            {
                return NotFound();
            }
            if(id!=null)
            {
                var usermedia = await _context.UserMedia.FirstOrDefaultAsync(m => m.Id == id);

                if (usermedia == null)
                {
                    return NotFound();
                }
                else
                {
                    UserMedia = usermedia;
                }   
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id,string? media_url)
        {
            if ((id == null && media_url==null) || _context.UserMedia == null)
            {
                return NotFound();
            }
            var usermedia = await _context.UserMedia.FindAsync(id);
            UserMedia = usermedia;
            var blobContainer = new BlobContainerClient(StorageConnectionString, blobContainerName);
            var blob = blobContainer.GetBlobClient(UserMedia.media_url);
            await blob.DeleteIfExistsAsync();
            if (media_url == "azure")
            {
                UserMedia.media_status = 1;
                UserMedia.media_deleted = DateTime.Now;
                _context.Attach(UserMedia).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMediaExists(UserMedia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                _context.UserMedia.Remove(UserMedia);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return Page();
        }
        private bool UserMediaExists(int id)
        {
            return (_context.UserMedia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
