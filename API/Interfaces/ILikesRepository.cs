using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface ILikesRepository
{
    void AddLike(UserLike like);
    void DeleteLike(UserLike like);
    Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId);
    Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId);
    Task<PagedList<MemberDto>> GetUserLikes(LikeParams likeParams);
    Task<bool> SaveChanges();
}
