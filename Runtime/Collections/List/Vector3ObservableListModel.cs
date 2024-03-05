using UnityEngine;

namespace Sticmac.ObservableModels.Collections
{
    /// <summary>
    /// Observable list model of <see cref="Vector3"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Observable Models/Collections/List/Vector3 Observable List Model", order = 100)]
    public class Vector3ObservableListModel : ObservableListModel<Vector3>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Vector3ObservableListModel"/> with the specified values.
        /// </summary>
        /// <param name="values">Initial values of the model.</param>
        /// <returns>The new instance.</returns>
        public static Vector3ObservableListModel Create(params Vector3[] values)
        {
            var model = CreateInstance<Vector3ObservableListModel>();
            model.Value = new System.Collections.Generic.List<Vector3>(values);
            return model;
        }
    }
}