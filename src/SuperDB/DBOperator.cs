using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperDB
{
    /// <summary>
    /// 定义简单的数据库操作方法
    /// </summary>
    public static class DBOperator
    {
        /// <summary>
        /// 异常事件
        /// </summary>
        public static event Action<Exception> ErrorEvent;

        /// <summary>
        /// 执行数据库命令，含事务
        /// 要么全部成功，要么全部失败
        /// </summary>
        /// <param name="factory">数据库工厂</param>
        /// <param name="commands">数据库操作命令</param>
        /// <returns>全部执行成功返回true，否则返回false</returns>
        public static bool TryExecute(this IDBFactory factory, IEnumerable<DBCommand> commands)
        {
            try
            {
                if (commands == null)
                {
                    return false;
                }
                if (commands.Count() == 0)
                {
                    return false;
                }
                using (factory)
                {
                    var flag = true;
                    foreach (var command in commands)
                    {
                        flag &= factory.Connection.Execute(command.CommandText, command.CommandParameter, factory.Transaction) > 0;
                    }
                    return flag ? factory.Commit() : factory.Rollback();
                }
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(ex);
                return false;
            }
        }

        /// <summary>
        /// 执行数据库命令，不含事务
        /// </summary>
        /// <param name="factory">数据库工厂</param>
        /// <param name="command">数据库操作命令</param>
        /// <returns>执行成功返回true，否则返回false</returns>
        public static bool TryExecute(this IDBFactory factory, DBCommand command)
        {
            try
            {
                if (command == null)
                {
                    return false;
                }
                using (factory)
                {
                    return factory.Connection.Execute(command.CommandText, command.CommandParameter) > 0;
                }
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(ex);
                return false;
            }
        }

        /// <summary>
        /// 查询数据库中的数据
        /// </summary>
        /// <typeparam name="T">查询的数据类型</typeparam>
        /// <param name="factory">数据库工厂</param>
        /// <param name="command">数据库操作命令</param>
        /// <returns>返回查到的数据集合</returns>
        public static List<T> Query<T>(this IDBFactory factory, DBCommand command)
        {
            try
            {
                if (command == null)
                {
                    return new List<T>();
                }
                using (factory)
                {
                    return factory.Connection.Query<T>(command.CommandText, command.CommandParameter, factory.Transaction).AsList();
                }
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(ex);
                return new List<T>();
            }
        }

        /// <summary>
        /// 尝试查询数据库中的数据
        /// </summary>
        /// <typeparam name="T">查询的数据类型</typeparam>
        /// <param name="factory">数据库工厂</param>
        /// <param name="command">数据库操作命令</param>
        /// <param name="results">查询到的数据集合</param>
        /// <returns>查询成功返回true， 否则返回false</returns>
        public static bool TryQuery<T>(this IDBFactory factory, DBCommand command, out List<T> results)
        {
            try
            {
                if (command == null)
                {
                    results = new List<T>();
                    return false;
                }
                using (factory)
                {
                    results = factory.Connection.Query<T>(command.CommandText, command.CommandParameter, factory.Transaction).AsList();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(ex);
                results = new List<T>();
                return false;
            }
        }
    }
}
