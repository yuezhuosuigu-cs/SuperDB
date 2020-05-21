namespace SuperDB
{
    /// <summary>
    /// 数据库命令
    /// </summary>
    public class DBCommand
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DBCommand()
        { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="commandText">sql命令</param>
        /// <param name="commandParameter">执行参数</param>
        public DBCommand(string commandText, object commandParameter = null)
        {
            CommandText = commandText;
            CommandParameter = commandParameter;
        }

        /// <summary>
        /// 数据库SQL语句
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// 数据库执行参数
        /// </summary>
        public object CommandParameter { get; set; }
    }
}