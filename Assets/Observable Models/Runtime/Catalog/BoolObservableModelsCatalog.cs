using UnityEngine;

namespace Sticmac.ObservableModels.Catalogs
{
    [CreateAssetMenu(fileName = "BoolObservableModels Catalog", menuName = "Observable Models/Catalogs/Bool Observable Models Catalog", order = 100)]
    public class BoolObservableModelsCatalog : ObservableModelsCatalog<string, bool>
    {
        public static BoolObservableModelsCatalog Create(params (string, bool)[] models)
        {
            var catalog = CreateInstance<BoolObservableModelsCatalog>();
            foreach (var (key, value) in models)
            {
                catalog._models.Add(key, BoolObservableModel.Create(value));
            }
            return catalog;
        }
    }
}