using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sticmac.ObservableModels.Collections
{
    /// <summary>
    /// Observable list model of floats.
    /// </summary>
    [CreateAssetMenu(menuName = "Observable Models/Collections/List/Float Observable List Model", order = 100)]
    public class FloatObservableListModel : ObservableListModel<float>
    {
        /// <summary>
        /// Creates a new instance of <see cref="FloatObservableListModel"/> with the specified values.
        /// </summary>
        /// <param name="values">Initial values of the model.</param>
        /// <returns>The new instance.</returns>
        public static FloatObservableListModel Create(params float[] values)
        {
            var model = CreateInstance<FloatObservableListModel>();
            model.Value = new List<float>(values);
            return model;
        }

        /// <summary>
        /// The sum of all the elements in the list.
        /// </summary>
        public float Sum => Value.Sum();
        /// <summary>
        /// The minimum value in the list.
        /// </summary>
        public float Min => Value.Min();
        /// <summary>
        /// The maximum value in the list.
        /// </summary>
        public float Max => Value.Max();
        /// <summary>
        /// The average value in the list.
        /// </summary>
        public float Average => Value.Average();
    }
}