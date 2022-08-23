using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Auth.Commands;
using products.Domain.Auth.Entities;
using products.Domain.Shared;

namespace products.Domain.Auth.Handlers;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, NotificationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(IUserRepository userRepository, ILogger<CreateUserHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<NotificationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        UserValidation userValidation = new();
        var validatoin = userValidation.Validate(request);
        if (!validatoin.IsValid) return new("Validation errors occurred.", false, new { validatoin.Errors });

        User user = new(request.UserName, request.Email, request.Password);
        user.Password = _userRepository.HashPassword(request.Password);
        await _userRepository.CreateAsync(user);

        return new("User created");
    }
}
