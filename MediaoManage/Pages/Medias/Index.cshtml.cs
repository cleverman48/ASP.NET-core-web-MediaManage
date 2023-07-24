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

namespace MediaoManage.Pages.Medias
{
    public class IndexModel : PageModel
    {
        private readonly MediaoManage.Data.MediaoManageContext _context;

        public IndexModel(MediaoManage.Data.MediaoManageContext context)
        {
            _context = context;
        }

        public IList<UserMedia> UserMedia { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var movies = from m in _context.UserMedia
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.media_searchtext.Contains(SearchString));
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
