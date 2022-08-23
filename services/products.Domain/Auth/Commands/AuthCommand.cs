using MediatR;
using products.Domain.Shared;

namespace products.Domain.Auth.Commands;
public record AuthCommand(AuthUser User) : IRequest<NotificationResult>;
public record AuthUser(string Email, string Password);