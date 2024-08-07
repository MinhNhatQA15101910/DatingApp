using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")] // /api/users
[ApiController]
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet] // /api/users
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        return context.Users.ToList();
    }

    [HttpGet("{id:int}")] // /api/users/3
    public ActionResult<AppUser> GetUser(int id)
    {
        var user = context.Users.Find(id);

        if (user == null) return NotFound();

        return user;
    }
}
