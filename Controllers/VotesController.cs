using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Memez.Areas.Identity.Data;
using Memez.Data;
using Memez.Models;

namespace Memez.Controllers
{
    public class VotesController : Controller
    {
        private readonly MemezContext _context;
        private readonly UserManager<MemezUser> _userManager;

        public VotesController(MemezContext context, UserManager<MemezUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //POST: Votes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(int memeId)
        {
            if (_context.Memes == null || _context.Votes == null)
            {
                return Problem("Entity set 'MemezContext.Meme' or 'Votes'  is null.");
            }
            MemezUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Meme? meme = await _context.Memes.FindAsync(memeId);
            if (meme == null)
            {
                return NotFound();
            }
            Vote vote = new Vote();
            meme.VotesSum += 1;
            _context.Add(vote);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(int voteId)
        {
            if (_context.Memes == null || _context.Votes == null)
            {
                return Problem("Entity set 'MemezContext.Meme' or 'Votes' is null.");
            }
            Vote? vote = await _context.Votes.FindAsync(voteId);
            if (vote == null)
            {
                return NotFound();
            }
            Meme? meme = await _context.Memes.FindAsync(vote.Meme);
            if (meme == null)
            {
                return NotFound();
            }
            meme.VotesSum -= 1;
            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
