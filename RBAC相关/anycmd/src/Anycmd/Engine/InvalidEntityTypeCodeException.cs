
namespace Anycmd.Engine
{
    using Exceptions;
    using System;

    public class InvalidEntityTypeCodeException : GeneralException
    {
        public InvalidEntityTypeCodeException(string message)
            : base(message)
        {
            //Intentionally left blank
        }

        public InvalidEntityTypeCodeException(string message, Exception innerException)
            : base(message, innerException)
        {
            //Intentionally left blank
        }

        public InvalidEntityTypeCodeException(Coder code)
            : base("codespace:" + code.Codespace + ",entityTypeCode:" + code.Code)
        {

        }

        public InvalidEntityTypeCodeException(Coder code, Exception innerException)
            : base("codespace:" + code.Codespace + ",entityTypeCode:" + code.Code, innerException)
        {

        }
    }
}
