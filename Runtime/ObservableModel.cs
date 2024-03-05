using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sticmac.ObservableModels {
    public abstract class ObservableModel : ScriptableObject, IEquatable<ObservableModel>
    {
        public abstract void ResetValue();

        public abstract string StringValue
        {
            get;
            set;
        }

        public abstract object ObjectValue
        {
            get;
            set;
        }

        public abstract bool Equals(ObservableModel other);

        public override bool Equals(object other)
        {
            if (other is ObservableModel model)
            {
                return Equals(model);
            }
            return false;
        }
    }

    public abstract class ObservableModel<T> : ObservableModel, IReadableModel<T>, IWritableModel<T>,
        IEquatable<ObservableModel<T>>, IEquatable<T>
    {
        protected T _value;

        [SerializeField] protected T _initialValue;
        
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<T> OnValueChanged;
        protected void InvokeOnValueChanged() => OnValueChanged?.Invoke(Value);

        private void OnEnable()
        {
            if ( !EqualityComparer<T>.Default.Equals(_initialValue, default) // If the initial value is not the default value
                && EqualityComparer<T>.Default.Equals(_value, default) ) // And the value is the default value
            {
                ResetValue();
            }
        }

        public static implicit operator T(ObservableModel<T> model) => model.Value;

        #region Equality
        /// <summary>
        /// Compares the model with another model.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(ObservableModel other)
        {
            if (other is ObservableModel<T> model)
            {
                return Equals(model);
            }
            return false;
        }

        /// <summary>
        /// Compares the model with another model of the same type.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ObservableModel<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        /// <summary>
        /// Compares the model with a value of the same type.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(T other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }

        public override bool Equals(object other)
        {
            return base.Equals(other) && other is ObservableModel<T> model && Equals(model);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(ObservableModel<T> a, ObservableModel<T> b) => a.Equals(b);
        public static bool operator !=(ObservableModel<T> a, ObservableModel<T> b) => !a.Equals(b);
        public static bool operator ==(ObservableModel<T> a, T b) => a.Equals(b);
        public static bool operator !=(ObservableModel<T> a, T b) => !a.Equals(b);
        public static bool operator ==(T a, ObservableModel<T> b) => b.Equals(a);
        public static bool operator !=(T a, ObservableModel<T> b) => !b.Equals(a);
        #endregion

        /// <summary>
        /// Resets the value to the initial value.
        /// </summary>
        public override void ResetValue()
        {
            Value = _initialValue;
        }

        /// <summary>
        /// The value of the model.
        /// </summary>
        public virtual T Value
        {
            get => _value;
            set {
                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        public override object ObjectValue
        {
            get => Value;
            set => Value = (T)value;
        }
    }
}
