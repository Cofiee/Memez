using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Memez.Data;
using Memez.Models;
using Memez.Images;
using Memez.Areas.Identity.Data;

namespace Memez.Controllers
{
    public class MemesController : Controller
    {
        private readonly MemezContext _context;
        private readonly UserManager<MemezUser> _userManager;

        public MemesController(MemezContext context, UserManager<MemezUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Memes
        public async Task<IActionResult> Index()
        {
            var memezContext = _context.Memes;
            return View(await memezContext.ToListAsync());
        }

        // GET: Memes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Memes == null)
            {
                return NotFound();
            }

            var meme = await _context.Memes.FirstOrDefaultAsync(m => m.Id == id);
            if (meme == null)
            {
                return NotFound();
            }

            return View(meme);
        }

        // GET: Memes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title")] Meme meme, IFormFile formFile)
        {
            meme.ImagePath = "";
            meme.Timestamp = DateTime.Now;
            MemezUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            meme.MemezUser = user;
            ModelState.ClearValidationState(nameof(Meme));
/*            if (!TryValidateModel(meme, nameof(Meme)) || formFile.Length <= 0)
            {
                return View(meme);
            }*/
            // TODO: Walidacja memow.
            _context.Add(meme);
            await _context.SaveChangesAsync();
            BaseImageManager imageManager = new BaseImageManager("wwwroot/images/memes");
            string imagePath = imageManager.Save(formFile, meme.Id);
            meme.ImagePath = imagePath;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Memes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Memes == null)
            {
                return NotFound();
            }

            var meme = await _context.Memes.FindAsync(id);
            if (meme == null)
            {
                return NotFound();
            }
            return View(meme);
        }

        // POST: Memes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ImagePath,Timestamp,AccountId")] Meme meme)
        {
            if (id != meme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemeExists(meme.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meme);
        }

        // GET: Memes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Memes == null)
            {
                return NotFound();
            }

            var meme = await _context.Memes.FirstOrDefaultAsync(m => m.Id == id);
            if (meme == null)
            {
                return NotFound();
            }

            return View(meme);
        }

        // POST: Memes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Memes == null)
            {
                return Problem("Entity set 'MemezContext.Meme'  is null.");
            }
            var meme = await _context.Memes.FindAsync(id);
            if (meme != null)
            {
                _context.Memes.Remove(meme);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemeExists(int id)
        {
          return (_context.Memes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
