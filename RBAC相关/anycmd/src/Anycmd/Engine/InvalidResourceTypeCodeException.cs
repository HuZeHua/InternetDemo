
namespace Anycmd.Engine
{
    using Exceptions;
    using System;

    public class InvalidResourceTypeCodeException : GeneralException
    {
        public InvalidResourceTypeCodeException(string message)
            : base(message)
        {
            //Intentionally left blank
        }

        public InvalidResourceTypeCodeException(string message, Exception innerException)
            : base(message, innerException)
        {
            //Intentionally left blank
        }

        public InvalidResourceTypeCodeException(Coder code)
            : base("codespace:" + code.Codespace + ",resourceTypeCode:" + code.Code)
        {

        }

        public InvalidResourceTypeCodeException(Coder code, Exception innerException)
            : base("codespace:" + code.Codespace + ",resourceTypeCode:" + code.Code, innerException)
        {

        }
    }
}
