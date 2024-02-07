using UnityEngine;

namespace Sticmac.ObservableModels {
    [CreateAssetMenu(fileName = "String Model", menuName = "Observable Models/String", order = 0)]
    public class StringObservableModel : ObservableModel<string>
    {
        /// <summary>
        /// Creates a new instance of the model.
        /// </summary>
        /// <param name="initialValue">The initial value of the model.</param>
        /// <returns>The new instance.</returns>
        public static StringObservableModel Create(string initialValue = default(string))
        {
            var instance = CreateInstance<StringObservableModel>();
            instance._initialValue = initialValue;
            instance.Value = initialValue;
            return instance;
        }

        public override string StringValue
        {
            get => Value;
            set => Value = value;
        }
    }
}