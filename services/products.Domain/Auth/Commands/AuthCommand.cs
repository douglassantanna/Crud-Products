using MediatR;
using products.Domain.Shared;

namespace products.Domain.Auth.Commands;
public record AuthCommand(string Email, string Password) : IRequest<NotificationResult>;