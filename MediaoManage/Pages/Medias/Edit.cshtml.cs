using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaoManage.Data;
using MediaoManage.Models;

namespace MediaoManage.Pages.Medias
{
    public class EditModel : PageModel
    {
        private readonly MediaoManage.Data.MediaoManageContext _context;

        public EditModel(MediaoManage.Data.MediaoManageContext context)
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

            var usermedia =  await _context.UserMedia.FirstOrDefaultAsync(m => m.Id == id);
            if (usermedia == null)
            {
                return NotFound();
            }
            UserMedia = usermedia;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

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

            return RedirectToPage("./Index");
        }

        private bool UserMediaExists(int id)
        {
          return (_context.UserMedia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
