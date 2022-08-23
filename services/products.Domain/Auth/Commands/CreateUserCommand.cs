using MediatR;
using products.Domain.Shared;

namespace products.Domain.Auth.Commands;
public record CreateUserCommand(string UserName, string Email, string Password) : IRequest<NotificationResult>;