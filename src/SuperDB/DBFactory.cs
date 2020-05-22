using System;
using System.Data;

namespace SuperDB
{
    /// <summary>
    /// 数据库连接工厂
    /// </summary>
    public abstract class DBFactory : IDisposable
    {
        private IDbTransaction _Transaction;

        /// <summary>
        /// 获取一个打开的数据库连接
        /// </summary>
        public abstract IDbConnection Connection { get; }

        /// <summary>
        /// 获取一个打开的数据库连接的事务
        /// </summary>
        public virtual IDbTransaction Transaction
        {
            get
            {
                if (_Transaction == default)
                {
                    _Transaction = Connection.BeginTransaction();
                }
                return _Transaction;
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public virtual bool Commit()
        {
            Transaction.Commit();
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            Connection.Dispose();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <returns></returns>
        public virtual bool Rollback()
        {
            Transaction.Rollback();
            return false;
        }
    }
}
