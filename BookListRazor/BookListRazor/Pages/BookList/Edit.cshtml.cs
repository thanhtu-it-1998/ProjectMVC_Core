using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly BookDbContext _context;

        public EditModel(BookDbContext context)
        {
            _context = context;
        }
        public Book Book { get; set; }
        public async Task OnGet(int id)
        {
            Book = await _context.Books.FindAsync(id);
        }
        public async Task<IActionResult> OnPost(Book book)
        {
            if (ModelState.IsValid)
            {
                var res = await _context.Books.FindAsync(book.Id);
                res.Name = book.Name;
                res.Author = book.Author;
                res.ISBN= book.ISBN;
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else return Page();
        }
    }
}
