﻿
namespace Anycmd.Engine.Host.Ac.MessageHandlers
{
    using Commands;
    using Engine.Ac;
    using Engine.Ac.Functions;
    using Exceptions;
    using Infra;
    using Repositories;
    using System;

    public class SaveHelpCommandHandler : CommandHandler<SaveHelpCommand>
    {
        private readonly IAcDomain _acDomain;

        public SaveHelpCommandHandler(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
        }

        public override void Handle(SaveHelpCommand command)
        {
            var operationHelpRepository = _acDomain.RetrieveRequiredService<IRepository<OperationHelp, Guid>>();
            var functionRepository = _acDomain.RetrieveRequiredService<IRepository<Function, Guid>>();
            if (command.FunctionId == Guid.Empty)
            {
                throw new ValidationException("EmptyFunctionId");
            }
            FunctionState operation;
            if (!_acDomain.FunctionSet.TryGetFunction(command.FunctionId, out operation))
            {
                throw new ValidationException("没有Id为" + command.FunctionId + "的操作");
            }
            var entity = operationHelpRepository.GetByKey(command.FunctionId);
            bool isNew = false;
            if (entity == null)
            {
                isNew = true;
                entity = new OperationHelp(command.FunctionId)
                {
                };

            }
            entity.Content = command.Content;
            entity.IsEnabled = command.IsEnabled.HasValue ? command.IsEnabled.Value : 1;
            if (isNew)
            {
                operationHelpRepository.Add(entity);
            }
            else
            {
                operationHelpRepository.Update(entity);
            }
            operationHelpRepository.Context.Commit();
        }
    }
}
