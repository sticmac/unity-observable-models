using System.Globalization;
using UnityEngine;

namespace Sticmac.ObservableModels {
    [CreateAssetMenu(fileName = "Float Model", menuName = "Observable Models/Float", order = 0)]
    public class FloatObservableModel : ObservableModel<float>
    {
        /// <summary>
        /// Creates a new instance of the model.
        /// </summary>
        /// <param name="initialValue">The initial value of the model.</param>
        /// <returns>The new instance.</returns>
        public static FloatObservableModel Create(float initialValue = default(float))
        {
            var instance = CreateInstance<FloatObservableModel>();
            instance._initialValue = initialValue;
            instance.Value = initialValue;
            return instance;
        }

        public override string StringValue
        {
            get => Value.ToString(CultureInfo.InvariantCulture);
            set => Value = float.Parse(value.Replace(",", "."), CultureInfo.InvariantCulture);
        }
    }
}