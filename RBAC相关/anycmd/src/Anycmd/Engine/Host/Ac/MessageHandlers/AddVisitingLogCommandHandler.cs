﻿
namespace Anycmd.Engine.Host.Ac.MessageHandlers
{
    using Bus;
    using Dapper;
    using Engine.Ac.Accounts;
    using Engine.Rdb;
    using System;
    using System.Data;

    public class AddVisitingLogCommandHandler : IHandler<AddVisitingLogCommand>
    {
        private readonly IAcDomain _acDomain;

        public AddVisitingLogCommandHandler(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
        }

        public void Handle(AddVisitingLogCommand message)
        {
            var dbId = new Guid("67E6CBF4-B481-4DDD-9FD9-1F0E06E9E1CB");
            RdbDescriptor db;
            if (!_acDomain.RdbSet.TryDb(dbId, out db))
            {
                //throw new CoreException("意外的数据库标识" + dbId);
                return;
            }
            using (var conn = db.GetConnection())
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                conn.Execute(
@"INSERT INTO dbo.VisitingLog
        ( Id ,
          AccountId ,
          LoginName ,
          VisitOn ,
          VisitedOn ,
          IpAddress ,
          StateCode ,
          ReasonPhrase ,
          Description
        )
VALUES  ( @Id ,
          @AccountId ,
          @LoginName ,
          @VisitOn ,
          @VisitedOn ,
          @IpAddress ,
          @StateCode ,
          @ReasonPhrase ,
          @Description
        )", new
                {
                    message.Id,
                    AccountId = message.AccountId,
                    message.LoginName,
                    message.VisitOn,
                    message.VisitedOn,
                    IpAddress = message.IpAddress,
                    message.StateCode,
                    message.ReasonPhrase,
                    message.Description
                });
            }
        }
    }
}
