using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLib.Model.Page
{
    public static class MongoPageConvert
    {
        public static async Task<MongoPageList<T>> GetPageData<T>(this IFindFluent<T, T> queryCursor, long count, int pageIndex, int pageSize) where T : class
        {
            MongoPageList<T> result = new MongoPageList<T>
            {
                Count = (int)count,
                PageIndex = ((pageIndex <= 0) ? 1 : pageIndex)
            };
            if (pageSize == 0)
            {
                result.PageSize = result.Count;
                MongoPageList<T> mongoPageList = result;
                mongoPageList.Datas = await queryCursor.ToListAsync().ConfigureAwait(continueOnCapturedContext: false);
                result.PageCount = 1;
            }
            else
            {
                result.PageSize = pageSize;
                if (pageIndex != 0)
                {
                    result.Datas = queryCursor.Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                }

                result.PageCount = (result.Count + pageSize - 1) / pageSize;
            }

            result.HasNext = result.PageCount > result.PageIndex;
            result.HasPrev = result.PageIndex > 1;
            return result;
        }
    }
}
