﻿
namespace Anycmd.Engine.Host.Rdb.MemorySets
{
    using Engine.Rdb;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 数据库表上下文
    /// </summary>
    public sealed class DbTableSet : IDbTableSet
    {
        public static readonly IDbTableSet Empty = new DbTableSet(EmptyAcDomain.SingleInstance);
        private static readonly object Locker = new object();

        private readonly Dictionary<RdbDescriptor, Dictionary<string, DbTable>> _dicById = new Dictionary<RdbDescriptor, Dictionary<string, DbTable>>();
        private bool _initialized = false;
        private readonly IAcDomain _acDomain;

        private DbTableSet(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
            // TODO:接入总线
        }

        public static IDbTableSet Create(IAcDomain acDomain)
        {
            if (acDomain == EmptyAcDomain.SingleInstance)
            {
                return Empty;
            }
            return new DbTableSet(acDomain);
        }

        /// <summary>
        /// 根据数据库索引该库的全部表
        /// </summary>
        /// <param name="db">数据库模型实例</param>
        /// <returns></returns>
        public IReadOnlyDictionary<string, DbTable> this[RdbDescriptor db]
        {
            get
            {
                if (!_initialized)
                {
                    Init();
                }
                if (!_dicById.ContainsKey(db))
                {
                    return new Dictionary<string, DbTable>(StringComparer.OrdinalIgnoreCase);
                }
                return _dicById[db];
            }
        }

        public bool TryGetDbTable(RdbDescriptor db, string dbTableId, out DbTable dbTable)
        {
            if (!_initialized)
            {
                Init();
            }
            if (!_dicById.ContainsKey(db))
            {
                dbTable = null;
                return false;
            }
            return _dicById[db].TryGetValue(dbTableId, out dbTable);
        }

        private void Init()
        {
            if (_initialized) return;
            lock (Locker)
            {
                if (_initialized) return;
                _dicById.Clear();
                foreach (var db in _acDomain.RdbSet)
                {
                    _dicById.Add(db, new Dictionary<string, DbTable>(StringComparer.OrdinalIgnoreCase));
                    var tables = _acDomain.RetrieveRequiredService<IOriginalHostStateReader>().GetDbTables(db);
                    foreach (var item in tables)
                    {
                        _dicById[db].Add(item.Id, item);
                    }
                }
                _initialized = true;
            }
        }
    }
}