using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sticmac.ObservableModels.Collections
{
    /// <summary>
    /// Observable list model of integers.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the list.</typeparam>
    [CreateAssetMenu(menuName = "Observable Models/Collections/List/Int Observable List Model", order = 100)]
    public class IntObservableListModel : ObservableListModel<int>
    {
        /// <summary>
        /// Creates a new instance of <see cref="IntObservableListModel"/> with the specified values.
        /// </summary>
        /// <param name="values">Initial values of the model.</param>
        /// <returns>The new instance.</returns>
        public static IntObservableListModel Create(params int[] values)
        {
            var model = CreateInstance<IntObservableListModel>();
            model.Value = new List<int>(values);
            return model;
        }

        /// <summary>
        /// The sum of all the elements in the list.
        /// </summary>
        public int Sum => Value.Sum();
        /// <summary>
        /// The minimum value in the list.
        /// </summary>
        public int Min => Value.Min();
        /// <summary>
        /// The maximum value in the list.
        /// </summary>
        public int Max => Value.Max();
        /// <summary>
        /// The average value in the list.
        /// </summary>
        public float Average => (float)Value.Average();
    }
}