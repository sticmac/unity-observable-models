
using UnityEngine;
using System.Collections.Generic;

namespace Sticmac.ObservableModels.Collections
{
    [CreateAssetMenu(menuName = "Observable Models/Collections/List/String Observable List Model", order = 100)]
    public class StringObservableListModel : ObservableListModel<string>
    {
        /// <summary>
        /// Creates a new instance of <see cref="StringObservableListModel"/> with the specified values.
        /// </summary>
        /// <param name="values">Initial values of the model.</param>
        /// <returns>The new instance.</returns>
        public static StringObservableListModel Create(params string[] values)
        {
            var model = CreateInstance<StringObservableListModel>();
            model.Value = new List<string>(values);
            return model;
        }

        public string Concatenated => string.Join(", ", Value);
    }
}