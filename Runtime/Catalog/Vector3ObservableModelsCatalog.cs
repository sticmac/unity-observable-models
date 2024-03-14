using UnityEngine;

namespace Sticmac.ObservableModels.Catalogs
{
    [CreateAssetMenu(fileName = "Vector3ObservableModels Catalog", menuName = "Observable Models/Catalogs/Vector3 Observable Models Catalog", order = 100)]
    public class Vector3ObservableModelsCatalog : ObservableModelsCatalog<string, Vector3>
    {
        public static Vector3ObservableModelsCatalog Create(params (string, Vector3)[] models)
        {
            var catalog = CreateInstance<Vector3ObservableModelsCatalog>();
            foreach (var (key, value) in models)
            {
                catalog._models.Add(key, Vector3ObservableModel.Create(value));
            }
            return catalog;
        }
    }
}