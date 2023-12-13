using UnityEngine;

namespace Sticmac.ObservableModels {
    [CreateAssetMenu(fileName = "Vector2 Model", menuName = "Observable Models/Vector2", order = 0)]
    public class Vector2ObservableModel : ObservableModel<Vector2>
    {
        /// <summary>
        /// Creates a new instance of the model.
        /// </summary>
        /// <param name="initialValue">The initial value of the model.</param>
        /// <returns>The new instance.</returns>
        public static Vector2ObservableModel Create(Vector2 initialValue = default(Vector2))
        {
            var instance = CreateInstance<Vector2ObservableModel>();
            instance._initialValue = initialValue;
            instance.Value = initialValue;
            return instance;
        }
    }
}