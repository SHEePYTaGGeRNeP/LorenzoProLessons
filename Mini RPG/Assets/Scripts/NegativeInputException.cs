using System;
using System.Runtime.Serialization;

[Serializable]
public class NegativeInputException : Exception
{
    public NegativeInputException()
    {
    }

    public NegativeInputException(string message) : base(message)
    {
    }

    public NegativeInputException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected NegativeInputException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}