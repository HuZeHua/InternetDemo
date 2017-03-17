
namespace Anycmd.Logging
{
    using System;
    using System.Data;
    using Util;

    public class ExceptionLog : ExceptionLogBase
    {
        private ExceptionLog(Guid id)
            : base(id)
        {

        }

        public static ExceptionLog Create(IDataRecord reader)
        {
            return new ExceptionLog(reader.GetGuid(reader.GetOrdinal("Id")))
            {
                BaseDirectory = reader.GetNullableString("BaseDirectory"),
                Process = reader.GetNullableString("Process"),
                Machine = reader.GetNullableString("Machine"),
                Level = reader.GetNullableString("Level"),
                Logger = reader.GetNullableString("Logger"),
                LogOn = reader.GetDateTime(reader.GetOrdinal("LogOn")),
                Message = reader.GetNullableString("Message"),
                Thread = reader.GetNullableString("Thread"),
                Exception = reader.GetNullableString("Exception")
            };
        }
    }
}
