using System;
using UnityEngine;
using System.Collections.Generic;

namespace Sticmac.ObservableModel {
    public abstract class ObservableModel<T> : ScriptableObject, IObservable<T> {
        private T _value;
        
        private List<IObserver<T>> _observers = new List<IObserver<T>>();

        public T Value {
            get => _value;
            set {
                _value = value;
                foreach (IObserver<T> observer in _observers)
                {
                    observer.OnNext(value);
                }
            }
        }

        private class Unsubscriber : IDisposable {
            private List<IObserver<T>> _observers;
            private IObserver<T> _observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose() {
                if (_observer == null && _observers.Contains(_observer)) {
                    _observers.Remove(_observer);
                }
            }
        }

        public IDisposable Subscribe(IObserver<T> observer) {
            _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }
    }
}