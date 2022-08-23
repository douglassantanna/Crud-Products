using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Auth.Commands;
using products.Domain.Auth.Contracts;
using products.Domain.Shared;

namespace products.Domain.Auth.Handlers;
public class AuthHandler : IRequestHandler<AuthCommand, NotificationResult>
{
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthHandler> _logger;

    public AuthHandler(IAuthRepository authRepository, ILogger<AuthHandler> logger, IUserRepository userRepository)
    {
        _authRepository = authRepository;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<NotificationResult> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetByEmail(request.User.Email);
        var isValidUser = await _authRepository.Authenticate(user);
        if (!isValidUser) return new("Invalid e-mail or password", false);
        return new("Valid user");
    }
}