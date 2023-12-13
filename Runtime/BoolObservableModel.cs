using UnityEngine;

namespace Sticmac.ObservableModels {
    [CreateAssetMenu(fileName = "Bool Model", menuName = "Observable Models/Bool", order = 0)]
    public class BoolObservableModel : ObservableModel<bool>
    {
        /// <summary>
        /// Creates a new instance of the model.
        /// </summary>
        /// <param name="initialValue">The initial value of the model.</param>
        /// <returns>The new instance.</returns>
        public static BoolObservableModel Create(bool initialValue = default(bool))
        {
            var instance = CreateInstance<BoolObservableModel>();
            instance._initialValue = initialValue;
            instance.Value = initialValue;
            return instance;
        }
    }
}