using System;

namespace Sticmac.ObservableModels
{
    public interface IReadableModel<T>
    {
        public T Value {get;}

        public event Action<T> OnValueChanged;
    }
}
