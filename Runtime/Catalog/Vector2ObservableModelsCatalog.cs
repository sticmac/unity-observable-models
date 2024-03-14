using UnityEngine;

namespace Sticmac.ObservableModels.Catalogs
{
    [CreateAssetMenu(fileName = "Vector2ObservableModels Catalog", menuName = "Observable Models/Catalogs/Vector2 Observable Models Catalog", order = 100)]
    public class Vector2ObservableModelsCatalog : ObservableModelsCatalog<string, Vector2>
    {
        public static Vector2ObservableModelsCatalog Create(params (string, Vector2)[] models)
        {
            var catalog = CreateInstance<Vector2ObservableModelsCatalog>();
            foreach (var (key, value) in models)
            {
                catalog._models.Add(key, Vector2ObservableModel.Create(value));
            }
            return catalog;
        }
    }
}