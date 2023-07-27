using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MediaoManage.Data;
using MediaoManage.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure.Storage.Blobs;
using System.Data;

namespace MediaoManage.Pages.Medias
{
    public class IndexModel : PageModel
    {
        private readonly MediaoManage.Data.MediaoManageContext _context;
        public BlobContainerClient blobContainer { get; set; }
        public string blobContainerName = "movie";
        public string StorageConnectionString = "UseDevelopmentStorage=true";
        //const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=youtubedev;AccountKey=YVHIVv+u2BzVxYPagFotweQNV+9uotA41Oswq+DHX24ALGgPd7ugq2bVu30tRJkLvN3hCvV7PQM3+AStHVNC/Q==;EndpointSuffix=core.windows.net";
        public IndexModel(MediaoManage.Data.MediaoManageContext context)
        {
            _context = context;
            blobContainer = new BlobContainerClient(StorageConnectionString, blobContainerName);
            blobContainer.CreateIfNotExists();
        }

        public IList<UserMedia> UserMedia { get;set; } = default!;
        public async Task OnGetAsync()
        {
            var movies = from m in _context.UserMedia
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.media_file_name.Contains(SearchString));
            }           
            UserMedia = await movies.ToListAsync();            
        }
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }
    }
}
