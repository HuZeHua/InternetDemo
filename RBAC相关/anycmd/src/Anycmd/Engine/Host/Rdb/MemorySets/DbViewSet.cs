﻿
namespace Anycmd.Engine.Host.Rdb.MemorySets
{
    using Engine.Rdb;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 数据库视图上下文
    /// </summary>
    public sealed class DbViewSet : IDbViewSet
    {
        public static readonly IDbViewSet Empty = new DbViewSet(EmptyAcDomain.SingleInstance);
        private static readonly object Locker = new object();

        private readonly Dictionary<RdbDescriptor, Dictionary<string, DbView>> _dicById = new Dictionary<RdbDescriptor, Dictionary<string, DbView>>();
        private bool _initialized = false;
        private readonly IAcDomain _acDomain;

        private DbViewSet(IAcDomain acDomain)
        {
            this._acDomain = acDomain;
            // TODO:接入总线
        }

        public static IDbViewSet Create(IAcDomain acDomain)
        {
            if (acDomain == EmptyAcDomain.SingleInstance)
            {
                return Empty;
            }
            return new DbViewSet(acDomain);
        }

        /// <summary>
        /// 根据数据库索引该库的所有视图
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IReadOnlyDictionary<string, DbView> this[RdbDescriptor db]
        {
            get
            {
                if (!_initialized)
                {
                    Init();
                }
                if (!_dicById.ContainsKey(db))
                {
                    return new Dictionary<string, DbView>(StringComparer.OrdinalIgnoreCase);
                }
                return _dicById[db];
            }
        }

        public bool TryGetDbView(RdbDescriptor db, string dbViewId, out DbView dbView)
        {
            if (!_initialized)
            {
                Init();
            }
            if (!_dicById.ContainsKey(db))
            {
                dbView = null;
                return false;
            }
            return _dicById[db].TryGetValue(dbViewId, out dbView);
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
                    _dicById.Add(db, new Dictionary<string, DbView>(StringComparer.OrdinalIgnoreCase));
                    var views = _acDomain.RetrieveRequiredService<IOriginalHostStateReader>().GetDbViews(db);
                    foreach (var item in views)
                    {
                        _dicById[db].Add(item.Id, item);
                    }
                }
                _initialized = true;
            }
        }
    }
}