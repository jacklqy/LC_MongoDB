using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLib.Model.Config
{
    /// <summary>
    /// MongoDB基础配置
    /// </summary>
    public class MongoConfig
    {
        public string ConnectionString {  get; set; }
        public string Database { get; set; }

        /// <summary>
        /// 主机
        /// </summary>
        public string ConnHost { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int ConnPort { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
