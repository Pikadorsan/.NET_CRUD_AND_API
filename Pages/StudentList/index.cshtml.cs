using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperMegaProjekt.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuperMegaProjekt.DataContext;

namespace SuperMegaProjekt.Pages.StudentList
{
    public class indexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public indexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get; set; }

        public async Task OnGet()
        {
            Student = _context.Student.Include(s => s.Group).ToList();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }

    }
}