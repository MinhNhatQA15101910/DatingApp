using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;

namespace API.Controllers;

[Authorize]
public class LikesController(IUnitOfWork unitOfWork) : BaseApiController
{
    [HttpPost("{targetUserId:int}")]
    public async Task<ActionResult> ToggleLike(int targetUserId)
    {
        int sourceUserId = User.GetUserId();

        if (sourceUserId == targetUserId) return BadRequest("You cannot like yourself");

        var existingLike = await unitOfWork.LikesRepository.GetUserLikeAsync(sourceUserId, targetUserId);
        if (existingLike == null)
        {
            var like = new UserLike
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };

            unitOfWork.LikesRepository.AddLike(like);
        }
        else
        {
            unitOfWork.LikesRepository.DeleteLike(existingLike);
        }

        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update like");
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserLikeIds()
    {
        var userIds = await unitOfWork.LikesRepository.GetCurrentUserLikeIdsAsync(User.GetUserId());

        return Ok(userIds);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserLikes([FromQuery] LikeParams likeParams)
    {
        likeParams.UserId = User.GetUserId();
        var users = await unitOfWork.LikesRepository.GetUserLikesAsync(likeParams);

        Response.AddPaginationHeader(users);

        return Ok(users);
    }
}
