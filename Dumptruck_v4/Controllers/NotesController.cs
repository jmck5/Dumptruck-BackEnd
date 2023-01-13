using Dumptruck_v4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Dumptruck_v4.Services;

namespace Dumptruck_v4.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase {
        private ScoobyContext myContext;
        private readonly TokenService _tokenService;
        
        public NotesController(
            ScoobyContext context,
            TokenService tokenService
            ) {
            myContext = context;
            _tokenService = tokenService;
        }
        [Authorize]
        [HttpGet]
        public List<Note> GetNotes() {

            Console.WriteLine("This is just for testing");
            if (myContext == null) {
                Console.WriteLine("Context is null");
            } else {
                Console.WriteLine(myContext.ToString());
            }
            var notes = myContext.Notes.Where(x => x.Id > 0).ToList();
            return notes;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Note>> Post(Note data) {
            Note note = new Note() {
                NoteContent = data.NoteContent,
                // User get user id from token,
                TimeStamp = DateTime.Now,
            };
            
            myContext.Notes.Add(note);
            await myContext.SaveChangesAsync();
            return Ok(note);
        }

    }
}
