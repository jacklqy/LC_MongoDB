using Microsoft.AspNetCore.Mvc;
using MongoDBLib.Model.Page;
using MongoDBLib.Service;
using MongoDBLib.Extension;
using Microsoft.AspNetCore.Authorization;
using MongoDBLib.Model.Dto.InDto;
using MongoDBLib.Model.Dto.QueryDto;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMongoService _mongoService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMongoService mongoService)
        {
            _mongoService = mongoService;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task GetMongoDBTest()
        //{
        //    //0.Json新增测试
        //    var doc = BsonDocument.Parse("{a:1,b:2,c:3,d:[{c:1}],e:'ff'}");
        //    _mongoService.Add(MongoConstants.BaseDB, MongoConstants.TestEntity, doc);

        //    //1.新增测试
        //    List<BsonDocument> list = new List<BsonDocument>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        JObject obj = new JObject();
        //        obj["Txt"] = "test" + (i + 10);
        //        obj["Age"] = 20 + i;
        //        obj["Sex"] = true;
        //        obj["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //        BsonDocument document = BsonDocument.Parse(JsonConvert.SerializeObject(obj));
        //        if (document["CreateTime"].AsString != null)
        //        {
        //            string time = document["CreateTime"].AsString;
        //            document.Set(document.IndexOfName("CreateTime"), BsonDateTime.Create(DateTime.Parse(time)));

        //        }
        //        list.Add(document);
        //    }
        //    _mongoService.BatchAdd(MongoConstants.BaseDB, MongoConstants.TestEntity, list);

        //    //2.修改测试
        //    var result = _mongoService.Update(new TestEntity() { _id = "62a14379fc05924a1fcc870d", Age = 10000, Sex = true, Txt = "测试修改", CreateTime = DateTime.Now.AddDays(-5) });
        //    var result1 = _mongoService.Update(new TestEntity() { _id = "62a14391fc05924a1fcc8713", Age = 10000, Sex = true, Txt = "测试修改", CreateTime = DateTime.Now.AddDays(-4) });

        //    //3.删除测试
        //    var result2 = _mongoService.Delete(new TestEntity() { _id = "62a14391fc05924a1fcc870e" });

        //    //4.单个查询
        //    var result3 = _mongoService.Get<TestEntity>(m => m._id == "62a14391fc05924a1fcc870f", null);

        //    //5.列表查询
        //    if (!string.IsNullOrEmpty(dto.Txt))
        //    {
        //        var result4 = _mongoService.List<TestEntity>(m => m.Txt == dto.Txt, null);//精准查询
        //        var result5 = _mongoService.List<TestEntity>(m => m.Txt.Contains(dto.Txt), null);//模糊查询
        //    }

        //    //6.分页测试
        //    var data = _mongoService.PageList<TestEntity>(MongoConstants.BaseDB, MongoConstants.TestEntity, m => m.Age < 10000, null);
        //    var data1 = _mongoService.PageList<TestEntity>(m => m.Age < 10000);
        //    if (dto.CreateTimeStart != null && dto.CreateTimeEnd != null)
        //    {
        //        var data2 = _mongoService.PageList<TestEntity>(m => m.CreateTime > dto.CreateTimeStart && m.CreateTime < dto.CreateTimeEnd);//日期范围查询
        //    }
        //    var data3 = _mongoService.PageList<TestEntity>(m => m.Age < 10000, f => new TestEntity() { Age = f.Age });
        //}

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task MongoDBTest()
        {
            await _mongoService.AddAsync(new ActionOperationProcessLogInDto("111", "222", DateTime.Now, "333", true, "444"));
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionOperationProcessLogInDto> GetById(string targetId)
        {
            System.Linq.Expressions.Expression<Func<ActionOperationProcessLogInDto, bool>> expression = o => o.TargetId == targetId && !o.IsDelete;

            var resultData = await _mongoService.GetAsync(expression);

            return resultData;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<ActionOperationProcessLogInDto>> GetListById(string targetId)
        {
            var resultData = new List<ActionOperationProcessLogInDto>();

            System.Linq.Expressions.Expression<Func<ActionOperationProcessLogInDto, bool>> expression = o => o.TargetId == targetId && !o.IsDelete;

            resultData = await _mongoService.GetListAsync(expression);

            return resultData;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="queryDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<MongoPageList<ActionOperationProcessLogInDto>> OperationProcessQuery(ActionLogQueryDto queryDto)
        {
            var resultData = new MongoPageList<ActionOperationProcessLogInDto>();

            System.Linq.Expressions.Expression<Func<ActionOperationProcessLogInDto, bool>> expression = o => o.IsDelete == false;

            if (!string.IsNullOrEmpty(queryDto.BusinessId))
                expression = expression.And(o => o.TargetId.Contains(queryDto.BusinessId));

            if (!string.IsNullOrEmpty(queryDto.BusinessCode))
                expression = expression.And(o => o.TargetCode.Contains(queryDto.BusinessCode));

            if (queryDto.StartTime != null)
                expression = expression.And(o => o.Time >= queryDto.StartTime.Value);

            if (queryDto.EndTime != null)
                expression = expression.And(o => o.Time <= queryDto.EndTime.Value.AddDays(1));

            if (queryDto.ActionBusiness != null && queryDto.ActionBusiness.Count > 0)
                expression = expression.And(o => queryDto.ActionBusiness.Contains(o.ActionBusiness));

            System.Linq.Expressions.Expression<Func<ActionOperationProcessLogInDto, ActionOperationProcessLogInDto>> projector = o =>
                new ActionOperationProcessLogInDto(o.TargetId, o.TargetCode, o.Time, o.SystemCode, o.IsDelete, o.ActionBusiness);

            resultData = await _mongoService.GetPageListAsync(expression, projector, queryDto.PageIndex, queryDto.PageSize, o => o.Time, true);

            return resultData;
        }

    }
}
