using System;

namespace Sticmac.ObservableModel
{
    public interface IReadableModel<T>
    {
        public T Value {get;}

        public event Action<T> OnValueChanged;
    }
}
