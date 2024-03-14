using UnityEngine;

namespace Sticmac.ObservableModels.Catalogs
{
    [CreateAssetMenu(fileName = "IntObservableModels Catalog", menuName = "Observable Models/Catalogs/Int Observable Models Catalog", order = 100)]
    public class IntObservableModelsCatalog : ObservableModelsCatalog<string, int>
    {
        public static IntObservableModelsCatalog Create(params (string, int)[] models)
        {
            var catalog = CreateInstance<IntObservableModelsCatalog>();
            foreach (var (key, value) in models)
            {
                catalog._models.Add(key, IntObservableModel.Create(value));
            }
            return catalog;
        }
    }
}