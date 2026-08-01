namespace PoupeMoney.Core.Application.Commons.Authentication;

public interface IAuthenticatedUser
{
    string Id { get; }
    string Email { get; }
    bool IsAuthenticated();
}