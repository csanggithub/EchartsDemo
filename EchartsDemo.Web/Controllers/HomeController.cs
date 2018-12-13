using EchartsDemo.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EchartsDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMemoryCache _memoryCache;

        public HomeController(IHostingEnvironment hostingEnvironment, IMemoryCache memoryCache)
        {
            _hostingEnvironment = hostingEnvironment;
            _memoryCache = memoryCache;
        }
        public IActionResult EchartsMap()
        {
            return View();
        }

        public IActionResult HKMap()
        {
            return View();
        }

        /// <summary>
        /// 获取地图JSON数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMapJson()
        {
            var result = string.Empty;
            string cacheKey = "guangzhoujson";
            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                //获取文件路径
                var path = _hostingEnvironment.WebRootPath + @"\lib\echarts\map\json\province\guangdong.json";
                if (System.IO.File.Exists(path))
                {
                    result = System.IO.File.ReadAllText(path, Encoding.UTF8);
                }
                _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2)));
            }
            return Json(result);
        }

        /// <summary>
        /// 获取“业务”地图数据
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public JsonResult GetIncomeMap()
        {
            List<AreaVM> result = new List<AreaVM>();
            string cacheKey = "guangzhouarealistjson";
            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                result = new List<AreaVM>();
                result.Add(new AreaVM() { Name = "广州", AreaCode = "200", Value = 1, Amont = "1" });
                result.Add(new AreaVM() { Name = "深圳", AreaCode = "300", Value = 2, Amont = "1" });
                result.Add(new AreaVM() { Name = "东莞", AreaCode = "400", Value = 3, Amont = "1" });
                result.Add(new AreaVM() { Name = "惠州", AreaCode = "500", Value = 4, Amont = "1" });
                result.Add(new AreaVM() { Name = "佛山", AreaCode = "600", Value = 1, Amont = "1" });
                result.Add(new AreaVM() { Name = "清远", AreaCode = "700", Value = 2, Amont = "1" });
                result.Add(new AreaVM() { Name = "韶关", AreaCode = "800", Value = 3, Amont = "1" });
                result.Add(new AreaVM() { Name = "梅州", AreaCode = "900", Value = 4, Amont = "1" });
                result.Add(new AreaVM() { Name = "河源", AreaCode = "110", Value = 1, Amont = "1" });
                result.Add(new AreaVM() { Name = "肇庆", AreaCode = "120", Value = 2, Amont = "1" });
                result.Add(new AreaVM() { Name = "茂名", AreaCode = "130", Value = 3, Amont = "1" });
                result.Add(new AreaVM() { Name = "阳江", AreaCode = "240", Value = 4, Amont = "1" });
                result.Add(new AreaVM() { Name = "湛江", AreaCode = "250", Value = 1, Amont = "1" });
                result.Add(new AreaVM() { Name = "汕头", AreaCode = "260", Value = 2, Amont = "1" });
                result.Add(new AreaVM() { Name = "汕尾", AreaCode = "270", Value = 3, Amont = "1" });
                result.Add(new AreaVM() { Name = "揭阳", AreaCode = "280", Value = 4, Amont = "1" });
                result.Add(new AreaVM() { Name = "潮州", AreaCode = "290", Value = 1, Amont = "1" });
                result.Add(new AreaVM() { Name = "云浮", AreaCode = "300", Value = 2, Amont = "1" });
                result.Add(new AreaVM() { Name = "中山", AreaCode = "310", Value = 3, Amont = "1" });
                result.Add(new AreaVM() { Name = "珠海", AreaCode = "320", Value = 4, Amont = "1" });
                result.Add(new AreaVM() { Name = "江门", AreaCode = "330", Value = 1, Amont = "1" });
                _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2)));
            }
            return Json(result);
        }

        public IActionResult Index()
        {
            var result = string.Empty;

            string cacheKey = "test";
            if (!_memoryCache.TryGetValue(cacheKey, out result))
            {
                var path = _hostingEnvironment.WebRootPath + @"\lib\echarts\map\json\province\guangdong.json";
                if (System.IO.File.Exists(path))
                {
                    result = System.IO.File.ReadAllText(path, Encoding.UTF8);
                }
                //result = $"LineZero{DateTime.Now}";
                //_memoryCache.Set(cacheKey, result);
                //设置相对过期时间2分钟
                _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2)));
                ////设置绝对过期时间2分钟
                //_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                //    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
                ////移除缓存
                //_memoryCache.Remove(cacheKey);
                ////缓存优先级 （程序压力大时，会根据优先级自动回收）
                //_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                //    .SetPriority(CacheItemPriority.NeverRemove));
                ////缓存回调 10秒过期会回调
                //_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                //    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10))
                //    .RegisterPostEvictionCallback((key, value, reason, substate) =>
                //    {
                //        Console.WriteLine($"键{key}值{value}改变，因为{reason}");
                //    }));
                ////缓存回调 根据Token过期
                //var cts = new CancellationTokenSource();
                //_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                //    .AddExpirationToken(new CancellationChangeToken(cts.Token))
                //    .RegisterPostEvictionCallback((key, value, reason, substate) =>
                //    {
                //        Console.WriteLine($"键{key}值{value}改变，因为{reason}");
                //    }));
                //cts.Cancel();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
