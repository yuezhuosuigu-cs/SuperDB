using System;
using System.Data;

namespace SuperDB
{
    /// <summary>
    /// 数据库连接工厂
    /// </summary>
    public interface IDBFactory : IDisposable
    {
        /// <summary>
        /// 获取一个打开的数据库连接
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 获取一个打开的数据库连接的事务
        /// </summary>
        IDbTransaction Transaction { get; }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        bool Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <returns></returns>
        bool Rollback();
    }
}
