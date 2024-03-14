using UnityEngine;

namespace Sticmac.ObservableModels.Catalogs
{
    [CreateAssetMenu(fileName = "FloatObservableModels Catalog", menuName = "Observable Models/Catalogs/Float Observable Models Catalog", order = 100)]
    public class FloatObservableModelsCatalog : ObservableModelsCatalog<string, float>
    {
        public static FloatObservableModelsCatalog Create(params (string, float)[] models)
        {
            var catalog = CreateInstance<FloatObservableModelsCatalog>();
            foreach (var (key, value) in models)
            {
                catalog._models.Add(key, FloatObservableModel.Create(value));
            }
            return catalog;
        }
    }
}