using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMegaProjekt.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuperMegaProjekt.DataContext;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuperMegaProjekt.Pages.StudentList
{
    public class EditModel : PageModel
    {
        private SuperMegaProjekt.DataContext.ApplicationDbContext _db;
        public EditModel(SuperMegaProjekt.DataContext.ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Group> Groups { get; set; }
        public SelectList GroupList { get; set; }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _db.Student.FirstOrDefaultAsync(m => m.StudentId == id);

            if (Student == null)
            {
                return NotFound();
            }

            Groups = await _db.Group.ToListAsync();
            GroupList = new SelectList(Groups.Select(g => new SelectListItem
            {
                Value = g.GroupId.ToString(),
                Text = g.Name
            }), "Value", "Text");

            ViewData["GroupId"] = new SelectList(_db.Group, "GroupId", "GroupId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Student != null)
            {
                _db.Attach(Student).State = EntityState.Modified;

                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(Student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StudentExists(int id)
        {
            return _db.Student.Any(s => s.StudentId == id);
        }
    }
}
