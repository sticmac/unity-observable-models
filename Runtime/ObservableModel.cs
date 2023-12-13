using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sticmac.ObservableModels {
    public abstract class ObservableModel<T> : ScriptableObject, IReadableModel<T>, IWritableModel<T> {
        private T _value;

        [SerializeField] protected T _initialValue;
        
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<T> OnValueChanged;

        private void OnEnable() {
            if ( !EqualityComparer<T>.Default.Equals(_initialValue, default) // If the initial value is not the default value
                && EqualityComparer<T>.Default.Equals(_value, default) ) // And the value is the default value
            {
                ResetValue();
            }
        }

        public static implicit operator T(ObservableModel<T> model) => model.Value;

        /// <summary>
        /// Resets the value to the initial value.
        /// </summary>
        public void ResetValue()
        {
            Value = _initialValue;
        }

        /// <summary>
        /// The value of the model.
        /// </summary>
        public T Value {
            get => _value;
            set {
                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }
    }
}
