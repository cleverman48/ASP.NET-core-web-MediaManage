using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MediaoManage.Data;
using MediaoManage.Models;

namespace MediaoManage.Pages.Medias
{
    public class DeleteModel : PageModel
    {
        private readonly MediaoManage.Data.MediaoManageContext _context;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserMedia == null)
            {
                return NotFound();
            }
            var usermedia = await _context.UserMedia.FindAsync(id);

            if (usermedia != null)
            {
                UserMedia = usermedia;
                _context.UserMedia.Remove(UserMedia);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
