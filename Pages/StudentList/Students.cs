using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMegaProjekt.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuperMegaProjekt.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuperMegaProjekt.Pages.StudentList
{

    public class CreateModel : PageModel
    {
        private readonly SuperMegaProjekt.DataContext.ApplicationDbContext _db;
        public CreateModel(SuperMegaProjekt.DataContext.ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Student Student { get; set; }
        public List<Group> Groups { get; set; }
        public SelectList GroupList { get; set; }

        public void OnGet()
        {
            Groups = _db.Group.ToList();
            GroupList = new SelectList(Groups.Select(g => new SelectListItem
            {
                Value = g.GroupId.ToString(),
                Text = g.Name
            }), "Value", "Text");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (_db.Student == null || Student == null)
            {
                return Page();
            }

            _db.Student.Add(Student);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}

