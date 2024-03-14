using UnityEngine;

namespace Sticmac.ObservableModels.Catalogs
{
    [CreateAssetMenu(fileName = "StringObservableModels Catalog", menuName = "Observable Models/Catalogs/String Observable Models Catalog", order = 100)]
    public class StringObservableModelsCatalog : ObservableModelsCatalog<string, string>
    {
        public static StringObservableModelsCatalog Create(params (string, string)[] models)
        {
            var catalog = CreateInstance<StringObservableModelsCatalog>();
            foreach (var (key, value) in models)
            {
                catalog._models.Add(key, StringObservableModel.Create(value));
            }
            return catalog;
        }
    }
}