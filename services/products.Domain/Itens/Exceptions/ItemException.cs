using System.Runtime.Serialization;

namespace products.Domain.Itens.Exceptions;

public class ItemException : Exception
{
    public ItemException()
    {
    }

    public ItemException(string? message) : base(message)
    {
    }

    public ItemException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ItemException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
