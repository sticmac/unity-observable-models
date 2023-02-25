using System;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

namespace Sticmac.ObservableModel {
    public abstract class ObservableModel<T> : ScriptableObject {
        private T _value;
        
        private List<IObserver<T>> _observers = new List<IObserver<T>>();

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
