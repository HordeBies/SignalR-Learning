using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.SampleMVC.Data;
using SignalR.SampleMVC.Models;
using System.Security.Claims;

namespace SignalR.SampleMVC.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ChatRoomsController(ApplicationDbContext context) : ControllerBase
    {

        // GET: api/ChatRooms
        [HttpGet]
        [Route("/[controller]/GetChatRoom")]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetChatRoom()
        {
            if (context.ChatRooms == null)
            {
                return NotFound();
            }
            return await context.ChatRooms.ToListAsync();
        }

        [HttpGet]
        [Route("/[controller]/GetChatUser")]
        public async Task<ActionResult<Object>> GetChatUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = await context.Users.ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            return users.Where(u => u.Id != userId).Select(u => new { u.Id, u.UserName }).ToList();
        }

        // POST: api/ChatRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("/[controller]/PostChatRoom")]
        public async Task<ActionResult<ChatRoom>> PostChatRoom(ChatRoom chatRoom)
        {
            if (context.ChatRooms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ChatRoom'  is null.");
            }
            context.ChatRooms.Add(chatRoom);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetChatRoom", new { id = chatRoom.Id }, chatRoom);
        }

        // DELETE: api/ChatRooms/5
        [HttpDelete("{id}")]
        [Route("/[controller]/DeleteChatRoom/{id}")]
        public async Task<IActionResult> DeleteChatRoom(int id)
        {
            if (context.ChatRooms == null)
            {
                return NotFound();
            }
            var chatRoom = await context.ChatRooms.FindAsync(id);
            if (chatRoom == null)
            {
                return NotFound();
            }

            context.ChatRooms.Remove(chatRoom);
            await context.SaveChangesAsync();

            var room = await context.ChatRooms.FirstOrDefaultAsync();

            return Ok(new { deleted = id, selected = (room == null ? 0 : room.Id) });
        }


    }
}
