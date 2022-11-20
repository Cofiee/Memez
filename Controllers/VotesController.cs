using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //POST: Votes/Create/{memeId}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Votes/Create/{memeId}")]
        [HttpPost]
        //[ValidateAntiForgeryToken] // TODO: Fix antiforgery token
        [Authorize]
        public async Task<IActionResult> Create(int memeId)
        {
            if (_context.Memes == null || _context.Votes == null)
            {
                return Problem("Entity set 'MemezContext.Meme' or 'Votes'  is null.");
            }
            Meme? meme = await _context.Memes.FindAsync(memeId);
            if (meme == null)
            {
                return NotFound();
            }
            MemezUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Vote? vote = await _context.Votes.Where(v => v.Meme.Id == memeId && v.MemezUser.Equals(user)).FirstAsync();
            if (vote != null)
            {
                return BadRequest("Meme is already liked"); // TODO: find something more suitable
            }
            vote = new Vote() { MemezUser = user, Meme = meme };
            _context.Add(vote);
            meme.VotesSum += 1;
            _context.Update(meme);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        //[ValidateAntiForgeryToken] // TODO: Fix antiforgery token
        [Authorize]
        public async Task<IActionResult> Delete(int voteId)
        {
            if (_context.Memes == null || _context.Votes == null)
            {
                return Problem("Entity set 'MemezContext.Meme' or 'MemezContext.Votes' is null.");
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
