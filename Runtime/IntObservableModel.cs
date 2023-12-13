using UnityEngine;

namespace Sticmac.ObservableModels {
    [CreateAssetMenu(fileName = "Int Model", menuName = "Observable Models/Int", order = 0)]
    public class IntObservableModel : ObservableModel<int>
    {
        /// <summary>
        /// Creates a new instance of the model.
        /// </summary>
        /// <param name="initialValue">The initial value of the model.</param>
        /// <returns>The new instance.</returns>
        public static IntObservableModel Create(int initialValue = default(int))
        {
            var instance = CreateInstance<IntObservableModel>();
            instance._initialValue = initialValue;
            instance.Value = initialValue;
            return instance;
        }
    }
}