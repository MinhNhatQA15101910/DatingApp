using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Data;

public class PhotoRepository(DataContext context, IMapper mapper) : IPhotoRepository
{
    public async Task<Photo?> GetPhotoByIdAsync(int id)
    {
        return await context.Photos
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotosAsync()
    {
        return await context.Photos
            .IgnoreQueryFilters()
            .Where(p => !p.IsApproved)
            .ProjectTo<PhotoForApprovalDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public void RemovePhoto(Photo photo)
    {
        context.Photos.Remove(photo);
    }
}
