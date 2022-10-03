using Domo;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Ptarmigan.Utils;

namespace Ptarmigan.Services
{
    public static class DomoExtensions
    {
        public static IEnumerable<T> GetValues<T>(this IRepository<T> repo)
            => repo.GetModels().Select(m => m.Value);

        public static IEnumerable<object> GetValues(this IRepository repo)
            => repo.GetModels().Select(m => m.Value);

        public static JsonText ToJson(this IRepository repo)
            => JsonConvert.SerializeObject(repo.GetValues().ToArray(), Formatting.Indented);

        public static T LoadFromJson<T>(this T repo, JsonText content) where T: IRepository
        {
            var type = repo.ValueType.MakeArrayType();
            var tmp = (Array)JsonConvert.DeserializeObject(content, type);

            if (repo.IsSingleton)
            {
                if (tmp.Length != 1) throw new Exception("Singleton repository can only have one item");
                var model = repo.GetSingleModel();
                model.Value = tmp.GetValue(0);
            }
            else
            {
                repo.Clear();
                for (var i = 0; i < tmp.Length; ++i)
                {
                    repo.Add(Guid.NewGuid(), tmp.GetValue(i));
                }
            }

            return repo;
        }
    }
}
