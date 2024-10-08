using API.Extensions;
using API.Interfaces;

namespace API.Helpers;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.HttpContext.User.Identity?.IsAuthenticated != true) return;

        var username = resultContext.HttpContext.User.GetUsername();

        var userRepository = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

        var user = await userRepository.GetUserByUsernameAsync(username);
        if (user == null) return;

        user.LastActive = DateTime.UtcNow;
        await userRepository.SaveAllAsync();
    }
}
