namespace products.Domain.Notifications;

    public record NotificationResult(string Message,bool Success, object Data = null);
   
