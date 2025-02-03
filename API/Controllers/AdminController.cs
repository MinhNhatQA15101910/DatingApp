using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Controllers;

public class AdminController(
    UserManager<AppUser> userManager,
    IUnitOfWork unitOfWork,
    IPhotoService photoService
) : BaseApiController
{
    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
        var users = await userManager.Users
            .OrderBy(user => user.UserName)
            .Select(user => new
            {
                user.Id,
                Username = user.UserName,
                Roles = user.UserRoles.Select(userRole => userRole.Role.Name).ToList()
            })
            .ToListAsync();

        return Ok(users);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("edit-roles/{username}")]
    public async Task<ActionResult> EditRoles(string username, string roles)
    {
        if (string.IsNullOrEmpty(roles))
        {
            return BadRequest("You must select at least one role");
        }

        var selectedRoles = roles.Split(",").ToArray();

        var user = await userManager.FindByNameAsync(username);
        if (user == null) return BadRequest("User not found");

        var userRoles = await userManager.GetRolesAsync(user);

        var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
        if (!result.Succeeded) return BadRequest("Failed to add to roles");

        result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
        if (!result.Succeeded) return BadRequest("Failed to remove from roles");

        return Ok(await userManager.GetRolesAsync(user));
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpGet("photos-to-moderate")]
    public async Task<ActionResult<IEnumerable<PhotoForApprovalDto>>> GetPhotosForModeration()
    {
        var photos = await unitOfWork.PhotoRepository.GetUnapprovedPhotosAsync();

        return Ok(photos);
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpPost("approve-photo/{photoId}")]
    public async Task<ActionResult> ApprovePhoto(int photoId)
    {
        var photo = await unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);
        if (photo == null) return NotFound("Photo not found");

        photo.IsApproved = true;

        var user = await unitOfWork.UserRepository.GetUserByPhotoIdAsync(photoId);
        if (user == null) return NotFound("User not found");

        if (!user.Photos.Any(p => p.IsMain))
        {
            photo.IsMain = true;
        }

        if (await unitOfWork.Complete()) return NoContent();

        return BadRequest("Failed to approve photo");
    }


    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpPost("reject-photo/{photoId}")]
    public async Task<ActionResult> RejectPhoto(int photoId)
    {
        var photo = await unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);

        if (photo == null) return NotFound("Photo not found");

        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);
        }

        unitOfWork.PhotoRepository.RemovePhoto(photo);

        if (await unitOfWork.Complete()) return NoContent();

        return BadRequest("Failed to reject photo");
    }
}
