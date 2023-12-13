using UnityEngine;

namespace Sticmac.ObservableModels {
    [CreateAssetMenu(fileName = "Vector3 Model", menuName = "Observable Models/Vector3", order = 0)]
    public class Vector3ObservableModel : ObservableModel<Vector3>
    {
        /// <summary>
        /// Creates a new instance of the model.
        /// </summary>
        /// <param name="initialValue">The initial value of the model.</param>
        /// <returns>The new instance.</returns>
        public static Vector3ObservableModel Create(Vector3 initialValue = default(Vector3))
        {
            var instance = CreateInstance<Vector3ObservableModel>();
            instance._initialValue = initialValue;
            instance.Value = initialValue;
            return instance;
        }
    }
}