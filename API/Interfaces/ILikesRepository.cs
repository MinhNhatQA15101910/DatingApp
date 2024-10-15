using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface ILikesRepository
{
    void AddLike(UserLike like);
    void DeleteLike(UserLike like);
    Task<IEnumerable<int>> GetCurrentUserLikeIdsAsync(int currentUserId);
    Task<UserLike?> GetUserLikeAsync(int sourceUserId, int targetUserId);
    Task<PagedList<MemberDto>> GetUserLikesAsync(LikeParams likeParams);
    Task<bool> SaveChangesAsync();
}
