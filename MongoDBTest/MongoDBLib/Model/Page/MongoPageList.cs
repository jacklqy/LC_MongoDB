using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBLib.Model.Page
{
    /// <summary>
    /// 分页模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoPageList<T>: Pager where T : class
    {
        public int Count { get; set; }

        public List<T> Datas { get; set; }

        public int PageCount { get; set; }

        public bool HasPrev { get; set; }

        public bool HasNext { get; set; }

        ///// <summary>
        ///// 分页集合
        ///// </summary>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">页项</param>
        ///// <param name="totalCount">总数</param>
        ///// <param name="datas">数据</param>
        //public MongoPageList(int pageIndex, int pageSize, int totalCount, List<T> datas)
        //{
        //    _count = totalCount;
        //    _pageSize = pageSize;
        //    _pageIndex = pageIndex;
        //    _datas = datas;
        //    _pageCount = _count % _pageSize == 0 ? _count / _pageSize : _count / _pageSize + 1;
        //}

        //private readonly int _count;
        ///// <summary>
        ///// 总数
        ///// </summary>
        //public int Count
        //{
        //    get { return _count; }
        //}

        //private readonly List<T> _datas;
        ///// <summary>
        ///// 元素
        ///// </summary>
        //public List<T> Datas
        //{
        //    get { return _datas; }
        //}


        //private readonly int _pageSize;
        ///// <summary>
        ///// 页项
        ///// </summary>
        //public int PageSize
        //{
        //    get { return _pageSize; }
        //}

        //private readonly int _pageIndex;
        ///// <summary>
        ///// 页码
        ///// </summary>
        //public int PageIndex
        //{
        //    get { return _pageIndex; }
        //}

        //private readonly int _pageCount;
        ///// <summary>
        ///// 总页数
        ///// </summary>
        //public int PageCount
        //{
        //    get { return _pageCount; }
        //}

        ///// <summary>
        ///// 是否有上一页
        ///// </summary>
        //public bool HasPrev
        //{
        //    get { return _pageIndex > 1; }
        //}

        ///// <summary>
        ///// 是否有下一页
        ///// </summary>
        //public bool HasNext
        //{
        //    get { return _pageIndex < _pageCount; }
        //}
    }
}
