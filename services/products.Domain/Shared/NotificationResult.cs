namespace products.Domain.Shared;

    public record NotificationResult(string Message,bool Success = true, object? Data = null);
   
