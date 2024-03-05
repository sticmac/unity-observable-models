using UnityEngine;

namespace Sticmac.ObservableModels.Collections
{
    /// <summary>
    /// Observable list model of booleans.
    /// </summary>
    [CreateAssetMenu(menuName = "Observable Models/List/Bool Observable List Model", order = 100)]
    public class BoolObservableListModel : ObservableListModel<bool>
    {
        /// <summary>
        /// Creates a new instance of <see cref="BoolObservableListModel"/> with the specified values.
        /// </summary>
        /// <param name="values">
        /// <returns></returns>
        public static BoolObservableListModel Create(params bool[] values)
        {
            var model = CreateInstance<BoolObservableListModel>();
            model.Value = new System.Collections.Generic.List<bool>(values);
            return model;
        }

        /// <summary>
        /// Returns true if all the elements in the list are true.
        /// Returns false if the list is empty.
        /// </summary>
        /// <returns>Whether all the elements in the list are true.</returns>
        public bool All()
        {
            // If the list is empty, return false.
            if (Value.Count == 0)
            {
                return false;
            }

            // If any of the elements in the list are false, return false.
            foreach (var item in Value)
            {
                if (!item)
                {
                    return false;
                }
            }

            // If all the elements in the list are true, return true.
            return true;
        }

        /// <summary>
        /// Returns true if any of the elements in the list are true.
        /// </summary>
        /// <returns>Whether any of the elements in the list are true.</returns>
        public bool Any()
        {
            foreach (var item in Value)
            {
                if (item)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if all the elements in the list are true (alias for <see cref="All"/>).
        /// </summary>
        /// <returns></returns>
        public bool And() => All();
        /// <summary>
        /// Returns true if any of the elements in the list are true (alias for <see cref="Any"/>).
        /// </summary>
        /// <returns></returns>
        public bool Or() => Any();

        /// <summary>
        /// Returns true if not all the elements in the list are true.
        /// </summary>
        /// <returns></returns>
        public bool NotAll() => !All();
        /// <summary>
        /// Returns true if none of the elements in the list are true.
        /// </summary>
        /// <returns></returns>
        public bool None() => !Any();
        /// <summary>
        /// Returns true if not all the elements in the list are true (alias for <see cref="NotAll"/>).
        /// </summary>
        /// <returns></returns>
        public bool Nand() => !And();
        /// <summary>
        /// Returns true if none of the elements in the list are true (alias for <see cref="None"/>).
        /// </summary>
        /// <returns></returns>
        public bool Nor() => !Or();

        /// <summary>
        /// Returns true if all the elements in the list are the same.
        /// </summary>
        /// <returns></returns>
        public bool AllEqual() => All() || None();

        /// <summary>
        /// Returns the number of elements in the list that are true.
        /// </summary>
        /// <returns></returns>
        public int CountTrue()
        {
            int count = 0;
            foreach (var item in Value)
            {
                if (item)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Returns the number of elements in the list that are false.
        /// </summary>
        /// <returns></returns>
        public int CountFalse()
        {
            int count = 0;
            foreach (var item in Value)
            {
                if (!item)
                {
                    count++;
                }
            }
            return count;
        }
    }
}