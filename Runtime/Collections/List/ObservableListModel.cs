using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sticmac.ObservableModels.Collections
{
    /// <summary>
    /// Observable list model.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the list.</typeparam>
    public abstract class ObservableListModel<T> : ObservableModel<IList<T>>,
        IReadableModel<IList<T>>, IWritableModel<IList<T>>,
        IEquatable<ObservableListModel<T>>, IEquatable<IList<T>>,
        IList<T>, ISerializationCallbackReceiver
    {
        #region Observable List Structure
        protected class ObservableList : IList<T>
        {
            private readonly IList<T> internalList;
            private readonly Action onValueChanged;

            public IList<T> InternalList => internalList;

            public ObservableList(List<T> list, Action onValueChanged)
            {
                internalList = list;
                this.onValueChanged = onValueChanged;
            }

            public T this[int index] {
                get => internalList[index];
                set
                {
                    internalList[index] = value;
                    onValueChanged?.Invoke();
                }
            }

            public static explicit operator List<T>(ObservableList observableList) => (List<T>)observableList.internalList;

            public int Count => internalList.Count;

            public bool IsReadOnly => internalList.IsReadOnly;

            public void Add(T item)
            {
                internalList.Add(item);
                onValueChanged?.Invoke();
            }

            public void Clear()
            {
                internalList.Clear();
                onValueChanged?.Invoke();
            }

            public bool Contains(T item)
            {
                return internalList.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                internalList.CopyTo(array, arrayIndex);
            }

            public IEnumerator<T> GetEnumerator()
            {
                return internalList.GetEnumerator();
            }

            public int IndexOf(T item)
            {
                return internalList.IndexOf(item);
            }

            public void Insert(int index, T item)
            {
                internalList.Insert(index, item);
                onValueChanged?.Invoke();
            }

            public bool Remove(T item)
            {
                var res = internalList.Remove(item);
                if (res)
                {
                    onValueChanged?.Invoke();
                }
                return res;
            }

            public void RemoveAt(int index)
            {
                internalList.RemoveAt(index);
                onValueChanged?.Invoke();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        #endregion

        public override IList<T> Value {
            get => base.Value;
            set
            {
                var notNullValue = value ?? new List<T>();

                if (Value == null
                    || (notNullValue != Value && notNullValue != ((ObservableList)Value).InternalList))
                {
                    base.Value = new ObservableList(notNullValue as List<T>, InvokeOnValueChanged);
                }
            }
        }

        #region Initialization
        protected virtual void Awake()
        {
            _initialValue = new List<T>();
            Value ??= _initialValue;
        }
        #endregion

        #region Serialization
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            // Ensure that the initial value is not null after deserialization
            _initialValue ??= new List<T>();

            // Ensure that the value is not null after deserialization
            Value ??= _initialValue;
        }
        #endregion

        #region List Operations
        /// <summary>
        /// Number of elements in the stored list.
        /// </summary>
        public int Count => Value.Count;

        /// <summary>
        /// Whether the stored list is read-only.
        /// </summary>
        public bool IsReadOnly => Value.IsReadOnly;

        /// <summary>
        /// Gets or sets the value at the specified index.
        /// </summary>
        /// <param name="index">Index of the element to get or set.</param>
        /// <returns>The value at the specified index.</returns>
        public T this[int index] {
            get => Value[index];
            set => Value[index] = value;
        }

        /// <summary>
        /// Checks equality with another observable list model.
        /// </summary>
        /// <param name="other">The other observable list model to compare with.</param>
        /// <returns>Whether the two observable list models are equal.</returns>
        public bool Equals(ObservableListModel<T> other)
        {
            foreach (var item in Value)
            {
                if (!other.Value.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the index of the first occurrence of the specified item in the list.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>The index of the first occurrence of the specified item, or -1 if the item is not found.</returns>
        public int IndexOf(T item) => Value.IndexOf(item);

        /// <summary>
        /// Inserts an item at the specified index.
        /// </summary>
        /// <param name="index">The index at which to insert the item.</param>
        /// <param name="item">The item to insert.</param>
        public void Insert(int index, T item) => Value.Insert(index, item);

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <throws>ArgumentOutOfRangeException</throws>
        public void RemoveAt(int index) => Value.RemoveAt(index);

        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item) => Value.Add(item);

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear() => Value.Clear();

        /// <summary>
        /// Checks whether the list contains the specified item.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns>Whether the list contains the specified item.</returns>
        public bool Contains(T item) => Value.Contains(item);

        /// <summary>
        /// Copies the elements of the list to an array, starting at a specified index.
        /// </summary>
        /// <param name="array">The array to copy the elements to.</param>
        /// <param name="arrayIndex">The index at which to start copying.</param>
        public void CopyTo(T[] array, int arrayIndex) => Value.CopyTo(array, arrayIndex);

        /// <summary>
        /// Removes the first occurrence of the specified item from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>Whether the item was removed.</returns>
        public bool Remove(T item) => Value.Remove(item);

        /// <summary>
        /// Gets an enumerator for the list.
        /// </summary>
        /// <returns>An enumerator for the list.</returns>
        public IEnumerator<T> GetEnumerator() => Value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        public override string StringValue
        {
            get => Value.ToString();
            set => Value = new List<T>(value.Split(',').Select(x => (T)Convert.ChangeType(x, typeof(T))));
        }
    }
}