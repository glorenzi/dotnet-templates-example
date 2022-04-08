using System.Runtime.Serialization;

namespace WebApiTemplate;

[Serializable]
public class InvalidParametersException : Exception
{
    public InvalidParametersException() { }
    public InvalidParametersException(string message) : base(message) { }
    public InvalidParametersException(string message, Exception inner) : base(message, inner) { }
    protected InvalidParametersException(
      SerializationInfo info,
      StreamingContext context) : base(info, context) { }
}
