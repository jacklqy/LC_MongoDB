using Microsoft.Extensions.DependencyInjection;
using MongoDBLib.Model.Config;
using MongoDBLib.Service;

namespace MongoDBLib
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册MongoDB服务
        /// </summary>
        /// <param name="services"></param> 
        /// <param name="setupAction"></param>
        public static void AddMongoDBService(this IServiceCollection services, Action<MongoConfig> setupAction)
        {
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction), "调用 MongoDB 配置时出错，未传入配置信息。");

            services.Configure(setupAction);

            #region 服务注册
            services.AddTransient<IMongoService, MongoService>();
            //期待更多扩展todo...
            #endregion
        }
    }
}
