using UnityEngine;

namespace Sticmac.ObservableModels.Collections
{
    /// <summary>
    /// Observable list model of <see cref="Vector2"/> values.
    /// </summary>
    [CreateAssetMenu(menuName = "Observable Models/Collections/List/Vector2 Observable List Model", order = 100)]
    public class Vector2ObservableListModel : ObservableListModel<Vector2>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Vector2ObservableListModel"/> with the specified values.
        /// </summary>
        /// <param name="values">Initial values of the model.</param>
        /// <returns>The new instance.</returns>
        public static Vector2ObservableListModel Create(params Vector2[] values)
        {
            var model = CreateInstance<Vector2ObservableListModel>();
            model.Value = new System.Collections.Generic.List<Vector2>(values);
            return model;
        }
    }
}