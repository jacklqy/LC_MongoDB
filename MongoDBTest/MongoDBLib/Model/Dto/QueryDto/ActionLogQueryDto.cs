using MongoDBLib.Model.Page;

namespace MongoDBLib.Model.Dto.QueryDto
{
    public class ActionLogQueryDto : Pager
    {
        /// <summary>
        /// 业务Id
        /// </summary>
        public string BusinessId { get; set; }

        /// <summary>
        /// 业务编码
        /// </summary>
        public string BusinessCode { get; set; }


        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModulName { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public List<string> ActionBusiness { get; set; }

        /// <summary>
        /// 记录
        /// </summary>
        public string OperationContent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>

        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>

        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string CreateName { get; set; }

        /// <summary>
        /// 操作人域账号
        /// </summary>
        public string CreateDomainId { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string TargetVersion { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string TargetBusinessCode { get; set; }
    }
}
