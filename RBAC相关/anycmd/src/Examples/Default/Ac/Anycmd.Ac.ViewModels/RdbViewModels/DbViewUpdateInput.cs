﻿
namespace Anycmd.Ac.ViewModels.RdbViewModels
{
    using Engine.Messages;
    using Engine.Rdb.InOuts;
    using Engine.Rdb.Messages;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 数据库视图输入模型
    /// </summary>
    public sealed class DbViewUpdateInput : IDbViewUpdateInput
    {
        public DbViewUpdateInput()
        {
            HecpOntology = "DbView";
            HecpVerb = "Update";
        }

        public string HecpOntology { get; private set; }

        public string HecpVerb { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Guid DatabaseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        public IAnycmdCommand ToCommand(IAcSession acSession)
        {
            return new UpdateDbViewCommand(acSession, this);
        }
    }
}
