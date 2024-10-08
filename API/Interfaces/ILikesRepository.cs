using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface ILikesRepository
{
    void AddLike(UserLike like);
    void DeleteLike(UserLike like);
    Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId);
    Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId);
    Task<IEnumerable<MemberDto>> GetUserLikes(string predicate, int userId);
    Task<bool> SaveChanges();
}
