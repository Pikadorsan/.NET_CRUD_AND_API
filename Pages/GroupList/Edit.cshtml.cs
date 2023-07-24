using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMegaProjekt.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuperMegaProjekt.DataContext;

namespace SuperMegaProjekt.Pages.GroupList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Group Group { get; set; }
        public async Task OnGet(int id)
        {
            Group = await _db.Group.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var groupFromDb = await _db.Group.FindAsync(Group.GroupId);
                groupFromDb.Name = Group.Name;
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

    }
}