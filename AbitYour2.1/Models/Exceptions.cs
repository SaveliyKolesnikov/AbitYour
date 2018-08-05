using System;

namespace AbitYour.Models
{
    [Serializable]
    public class InvalidScoreException : Exception
    {
        public InvalidScoreException() { }
        public InvalidScoreException(string message) : base(message) { }
        public InvalidScoreException(string message, Exception inner) : base(message, inner) { }
        protected InvalidScoreException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class InvalidDataException : Exception
    {
        public InvalidDataException() { }
        public InvalidDataException(string message) : base(message) { }
        public InvalidDataException(string message, Exception inner) : base(message, inner) { }
        protected InvalidDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }



    [Serializable]
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException() { }
        public InvalidUrlException(string message) : base(message) { }
        public InvalidUrlException(string message, Exception inner) : base(message, inner) { }
        protected InvalidUrlException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}