using System.Security.Claims;

using PoupeMoney.Core.Application.Commons.Authentication;

namespace PoupeMoney.Core.WebApi.Common.Authentication;

public sealed class AuthenticatedUser : IAuthenticatedUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthenticatedUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
        Load();
    }

    public string Id { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public bool IsAuthenticated() =>
        _contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    private void Load()
    {
        if (!IsAuthenticated()) return;
        var id = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(id)) throw new ArgumentException("Id is required");
        if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email is required");

        Id = id;
        Email = email;
    }
}