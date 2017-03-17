﻿
namespace Anycmd.Engine.Host.Edi.Entities
{
    using Engine.Edi.Abstractions;
    using Exceptions;
    using Model;
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class NodeTopic : EntityBase, IAggregateRoot<Guid>, INodeTopic
    {
        private Guid _nodeId;
        private Guid _eventSubjectId;

        private NodeTopic() { }

        /// <summary>
        /// 
        /// </summary>
        public Guid NodeId
        {
            get { return _nodeId; }
            set
            {
                if (value != _nodeId)
                {
                    if (_nodeId != Guid.Empty)
                    {
                        throw new GeneralException("不能更改关联节点");
                    }
                    _nodeId = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid TopicId
        {
            get { return _eventSubjectId; }
            set
            {
                if (value != _eventSubjectId)
                {
                    if (_eventSubjectId != Guid.Empty)
                    {
                        throw new GeneralException("不能更改关联事件主题");
                    }
                    _eventSubjectId = value;
                }
            }
        }
    }
}
