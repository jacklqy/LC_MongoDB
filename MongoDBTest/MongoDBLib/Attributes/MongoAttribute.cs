using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLib.Attributes
{
    #region Mongo实体标签特性
    /// <summary>
    /// Mongo实体标签特性
    /// </summary>
    public class MongoAttribute : Attribute
    {
        public MongoAttribute(string database, string collection)
        {
            Database = database;
            Collection = collection;
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { get; private set; }

        /// <summary>
        /// 集合名称
        /// </summary>
        public string Collection { get; private set; }

    }
    #endregion
}
