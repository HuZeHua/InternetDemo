
namespace Anycmd.Engine.Edi.Messages
{
    using Commands;
    using Engine.Messages;
    using InOuts;
    using System;


    public sealed class AddActionCommand : AddEntityCommand<IActionCreateIo>, IAnycmdCommand
    {
        public AddActionCommand(IAcSession acSession, IActionCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddArchiveCommand : AddEntityCommand<IArchiveCreateIo>, IAnycmdCommand
    {
        public AddArchiveCommand(IAcSession acSession, IArchiveCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddBatchCommand : AddEntityCommand<IBatchCreateIo>, IAnycmdCommand
    {
        public AddBatchCommand(IAcSession acSession, IBatchCreateIo input)
            : base(acSession, input)
        {
        }
    }

    public sealed class AddCatalogActionCommand : AddEntityCommand<ICatalogActionCreateIo>, IAnycmdCommand
    {
        public AddCatalogActionCommand(IAcSession acSession, ICatalogActionCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddElementCommand : AddEntityCommand<IElementCreateIo>, IAnycmdCommand
    {
        public AddElementCommand(IAcSession acSession, IElementCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddInfoDicCommand : AddEntityCommand<IInfoDicCreateIo>, IAnycmdCommand
    {
        public AddInfoDicCommand(IAcSession acSession, IInfoDicCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddInfoDicItemCommand : AddEntityCommand<IInfoDicItemCreateIo>, IAnycmdCommand
    {
        public AddInfoDicItemCommand(IAcSession acSession, IInfoDicItemCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddInfoGroupCommand : AddEntityCommand<IInfoGroupCreateIo>, IAnycmdCommand
    {
        public AddInfoGroupCommand(IAcSession acSession, IInfoGroupCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddNodeActionCommand : AddEntityCommand<INodeActionCreateIo>, IAnycmdCommand
    {
        public AddNodeActionCommand(IAcSession acSession, INodeActionCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddNodeCommand : AddEntityCommand<INodeCreateIo>, IAnycmdCommand
    {
        public AddNodeCommand(IAcSession acSession, INodeCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddNodeElementActionCommand : AddEntityCommand<INodeElementActionCreateIo>, IAnycmdCommand
    {
        public AddNodeElementActionCommand(IAcSession acSession, INodeElementActionCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddNodeElementCareCommand : AddEntityCommand<INodeElementCareCreateIo>, IAnycmdCommand
    {
        public AddNodeElementCareCommand(IAcSession acSession, INodeElementCareCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddNodeOntologyCareCommand : AddEntityCommand<INodeOntologyCareCreateIo>, IAnycmdCommand
    {
        public AddNodeOntologyCareCommand(IAcSession acSession, INodeOntologyCareCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddNodeOntologyCatalogCommand : AddEntityCommand<INodeOntologyCatalogCreateIo>, IAnycmdCommand
    {
        public AddNodeOntologyCatalogCommand(IAcSession acSession, INodeOntologyCatalogCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddOntologyCatalogCommand : AddEntityCommand<IOntologyCatalogCreateIo>, IAnycmdCommand
    {
        public AddOntologyCatalogCommand(IAcSession acSession, IOntologyCatalogCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddOntologyCommand : AddEntityCommand<IOntologyCreateIo>, IAnycmdCommand
    {
        public AddOntologyCommand(IAcSession acSession, IOntologyCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class AddProcessCommand : AddEntityCommand<IProcessCreateIo>, IAnycmdCommand
    {
        public AddProcessCommand(IAcSession acSession, IProcessCreateIo input)
            : base(acSession, input)
        {

        }
    }

    // TODO:在界面上添加创建运行时本体元素的按钮
    public sealed class AddSystemElementCommand : Command, IAnycmdCommand
    {
        public AddSystemElementCommand(IAcSession acSession)
        {
            this.AcSession = acSession;
        }

        public IAcSession AcSession { get; private set; }
    }

    public sealed class AddTopicCommand : AddEntityCommand<ITopicCreateIo>, IAnycmdCommand
    {
        public AddTopicCommand(IAcSession acSession, ITopicCreateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class ChangeProcessCatalogCommand : Command, IAnycmdCommand
    {
        public ChangeProcessCatalogCommand(IAcSession acSession, Guid processId, string catalogCode)
        {
            this.AcSession = acSession;
            this.ProcessId = processId;
            this.CatalogCode = catalogCode;
        }

        public IAcSession AcSession { get; private set; }

        public Guid ProcessId { get; private set; }
        public string CatalogCode { get; private set; }
    }

    public sealed class ChangeProcessNetPortCommand : Command, IAnycmdCommand
    {
        public ChangeProcessNetPortCommand(IAcSession acSession, Guid processId, int netPort)
        {
            this.AcSession = acSession;
            this.ProcessId = processId;
            this.NetPort = netPort;
        }

        public IAcSession AcSession { get; private set; }

        public Guid ProcessId { get; private set; }
        public int NetPort { get; private set; }
    }

    public sealed class RemoveActionCommand : RemoveEntityCommand
    {
        public RemoveActionCommand(IAcSession acSession, Guid actionId)
            : base(acSession, actionId)
        {

        }
    }

    public sealed class RemoveArchiveCommand : RemoveEntityCommand
    {
        public RemoveArchiveCommand(IAcSession acSession, Guid archiveId)
            : base(acSession, archiveId)
        {

        }
    }

    public sealed class RemoveBatchCommand : RemoveEntityCommand
    {
        public RemoveBatchCommand(IAcSession acSession, Guid batchId)
            : base(acSession, batchId)
        {

        }
    }

    public sealed class RemoveCatalogActionCommand : RemoveEntityCommand
    {
        public RemoveCatalogActionCommand(IAcSession acSession, Guid ontologyCatalogActionId)
            : base(acSession, ontologyCatalogActionId)
        {

        }
    }

    public sealed class RemoveElementCommand : RemoveEntityCommand
    {
        public RemoveElementCommand(IAcSession acSession, Guid elementId)
            : base(acSession, elementId)
        {

        }
    }

    public sealed class RemoveInfoDicCommand : RemoveEntityCommand
    {
        public RemoveInfoDicCommand(IAcSession acSession, Guid infoDicId)
            : base(acSession, infoDicId)
        {

        }
    }

    public sealed class RemoveInfoDicItemCommand : RemoveEntityCommand
    {
        public RemoveInfoDicItemCommand(IAcSession acSession, Guid infoDicItemId)
            : base(acSession, infoDicItemId)
        {

        }
    }

    public sealed class RemoveInfoGroupCommand : RemoveEntityCommand
    {
        public RemoveInfoGroupCommand(IAcSession acSession, Guid infoGroupId)
            : base(acSession, infoGroupId)
        {

        }
    }

    public sealed class RemoveNodeCommand : RemoveEntityCommand
    {
        public RemoveNodeCommand(IAcSession acSession, Guid nodeId)
            : base(acSession, nodeId)
        {

        }
    }

    public sealed class RemoveNodeElementActionCommand : RemoveEntityCommand
    {
        public RemoveNodeElementActionCommand(IAcSession acSession, Guid nodeElementActionId)
            : base(acSession, nodeElementActionId)
        {

        }
    }

    public sealed class RemoveNodeElementCareCommand : RemoveEntityCommand
    {
        public RemoveNodeElementCareCommand(IAcSession acSession, Guid nodeElementCareId)
            : base(acSession, nodeElementCareId)
        {

        }
    }

    public sealed class RemoveNodeOntologyCareCommand : RemoveEntityCommand
    {
        public RemoveNodeOntologyCareCommand(IAcSession acSession, Guid nodeOntologyCareId)
            : base(acSession, nodeOntologyCareId)
        {

        }
    }

    public sealed class RemoveNodeOntologyCatalogCommand : Command, IAnycmdCommand
    {
        public RemoveNodeOntologyCatalogCommand(IAcSession acSession, Guid nodeId, Guid ontologyId, Guid catalogId)
        {
            this.AcSession = acSession;
            this.NodeId = nodeId;
            this.OntologyId = ontologyId;
            this.CatalogId = catalogId;
        }

        public IAcSession AcSession { get; private set; }
        public Guid NodeId { get; private set; }
        public Guid OntologyId { get; private set; }
        public Guid CatalogId { get; private set; }
    }

    public sealed class RemoveOntologyCatalogCommand : Command, IAnycmdCommand
    {
        public RemoveOntologyCatalogCommand(IAcSession acSession, Guid ontologyId, Guid catalogId)
        {
            this.AcSession = acSession;
            this.OntologyId = ontologyId;
            this.CatalogId = catalogId;
        }

        public IAcSession AcSession { get; private set; }

        public Guid OntologyId { get; private set; }

        public Guid CatalogId { get; private set; }
    }

    public sealed class RemoveOntologyCommand : RemoveEntityCommand
    {
        public RemoveOntologyCommand(IAcSession acSession, Guid ontologyId)
            : base(acSession, ontologyId)
        {

        }
    }

    public sealed class RemoveProcessCommand : RemoveEntityCommand
    {
        public RemoveProcessCommand(IAcSession acSession, Guid processId)
            : base(acSession, processId)
        {

        }
    }

    public sealed class RemoveTopicCommand : RemoveEntityCommand
    {
        public RemoveTopicCommand(IAcSession acSession, Guid eventTopicId)
            : base(acSession, eventTopicId)
        {

        }
    }

    public sealed class UpdateActionCommand : UpdateEntityCommand<IActionUpdateIo>, IAnycmdCommand
    {
        public UpdateActionCommand(IAcSession acSession, IActionUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateArchiveCommand : UpdateEntityCommand<IArchiveUpdateIo>, IAnycmdCommand
    {
        public UpdateArchiveCommand(IAcSession acSession, IArchiveUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateBatchCommand : UpdateEntityCommand<IBatchUpdateIo>, IAnycmdCommand
    {
        public UpdateBatchCommand(IAcSession acSession, IBatchUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateCatalogActionCommand : UpdateEntityCommand<ICatalogActionUpdateIo>, IAnycmdCommand
    {
        public UpdateCatalogActionCommand(IAcSession acSession, ICatalogActionUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateElementCommand : UpdateEntityCommand<IElementUpdateIo>, IAnycmdCommand
    {
        public UpdateElementCommand(IAcSession acSession, IElementUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateInfoDicCommand : UpdateEntityCommand<IInfoDicUpdateIo>, IAnycmdCommand
    {
        public UpdateInfoDicCommand(IAcSession acSession, IInfoDicUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateInfoDicItemCommand : UpdateEntityCommand<IInfoDicItemUpdateIo>, IAnycmdCommand
    {
        public UpdateInfoDicItemCommand(IAcSession acSession, IInfoDicItemUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateInfoGroupCommand : UpdateEntityCommand<IInfoGroupUpdateIo>, IAnycmdCommand
    {
        public UpdateInfoGroupCommand(IAcSession acSession, IInfoGroupUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateInfoRuleCommand : UpdateEntityCommand<IInfoRuleUpdateIo>, IAnycmdCommand
    {
        public UpdateInfoRuleCommand(IAcSession acSession, IInfoRuleUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateNodeActionCommand : UpdateEntityCommand<INodeActionUpdateIo>, IAnycmdCommand
    {
        public UpdateNodeActionCommand(IAcSession acSession, INodeActionUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateNodeCommand : UpdateEntityCommand<INodeUpdateIo>, IAnycmdCommand
    {
        public UpdateNodeCommand(IAcSession acSession, INodeUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateNodeElementCareCommand : Command, IAnycmdCommand
    {
        public UpdateNodeElementCareCommand(IAcSession acSession, Guid nodeElementCareId, bool isInfoIdItem)
        {
            this.AcSession = acSession;
            this.NodeElementCareId = nodeElementCareId;
            this.IsInfoIdItem = isInfoIdItem;
        }

        public IAcSession AcSession { get; private set; }
        public Guid NodeElementCareId { get; private set; }
        public bool IsInfoIdItem { get; private set; }
    }

    public sealed class UpdateOntologyCommand : UpdateEntityCommand<IOntologyUpdateIo>, IAnycmdCommand
    {
        public UpdateOntologyCommand(IAcSession acSession, IOntologyUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateProcessCommand : UpdateEntityCommand<IProcessUpdateIo>, IAnycmdCommand
    {
        public UpdateProcessCommand(IAcSession acSession, IProcessUpdateIo input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateStateCodeCommand : UpdateEntityCommand<IStateCodeUpdateInput>, IAnycmdCommand
    {
        public UpdateStateCodeCommand(IAcSession acSession, IStateCodeUpdateInput input)
            : base(acSession, input)
        {

        }
    }

    public sealed class UpdateTopicCommand : UpdateEntityCommand<ITopicUpdateIo>, IAnycmdCommand
    {
        public UpdateTopicCommand(IAcSession acSession, ITopicUpdateIo input)
            : base(acSession, input)
        {

        }
    }
}
