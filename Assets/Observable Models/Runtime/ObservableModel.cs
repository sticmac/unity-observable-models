using System;
using UnityEngine;

namespace Sticmac.ObservableModels {
    public abstract class ObservableModel<T> : ScriptableObject, IReadableModel<T>, IWritableModel<T> {
        private T _value;
        
        public event Action<T> OnValueChanged;

        public T Value {
            get => _value;
            set {
                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }
    }
}
