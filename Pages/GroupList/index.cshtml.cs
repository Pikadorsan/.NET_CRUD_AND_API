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
    public class indexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public indexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Group> Groups { get; set; }

        public async Task OnGet()
        {
            Groups = await _db.Group.ToListAsync();
        }


        public async Task<IActionResult> OnPostDelete(int id)
        {
            var group = await _db.Group.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            var studentsInGroup = await _db.Student.Where(s => s.GroupId == id).ToListAsync();
            if (studentsInGroup.Any())
            {
                TempData["ErrorMessage"] = $"Nie można usunąć grupy {group.Name}, ponieważ posiada przypisanych studentów.";
                return RedirectToPage();
            }

            _db.Group.Remove(group);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}