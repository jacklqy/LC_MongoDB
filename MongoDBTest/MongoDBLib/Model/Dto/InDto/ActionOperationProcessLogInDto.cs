using System;
using System.Runtime.Serialization;

namespace MongoDBLib.Model.Dto.InDto
{
    public class ActionOperationProcessLogInDto
    {
        public ActionOperationProcessLogInDto(string targetId, string targetCode, DateTime time, string systemCode, bool isDelete, string actionBusiness)
        {
            TargetId = targetId;
            TargetCode = targetCode;
            SystemCode = systemCode;
            IsDelete = isDelete;
            ActionBusiness = actionBusiness;
            Time = time;
        }

        public string ActionBusiness { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string TargetCode { get; set; }

        public string SystemCode { get; set; }

        public bool IsDelete { get; set; }

        public DateTime Time { get; set; }
    }
}