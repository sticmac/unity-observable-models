using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sticmac.ObservableModels.Catalogs
{
    public abstract class ObservableModelsCatalog<T, U> : ScriptableObject, IReadOnlyDictionary<T, ObservableModel<U>>, ISerializationCallbackReceiver
    {
        protected Dictionary<T, ObservableModel<U>> _models = new();

        #region Initialization
        protected virtual void OnEnable()
        {
            _models ??= new Dictionary<T, ObservableModel<U>>();
        }
        #endregion

        #region Serialization / Deserialization
        [Serializable]
        private struct SerializedKeyPairValue
        {
            public T key;
            public ObservableModel<U> value;
        }

        [SerializeField, FormerlySerializedAs("keyPairValues")] private List<SerializedKeyPairValue> _catalogElements;
        
        public void OnBeforeSerialize()
        {
            foreach (var pair in _models)
            {
                if (_catalogElements.Where(kvp => EqualityComparer<T>.Default.Equals(kvp.key, pair.Key)).Count() == 0)
                {
                    _catalogElements.Add(new SerializedKeyPairValue()
                    {
                        key = pair.Key,
                        value = pair.Value
                    });
                }
            }
        }

        public virtual void OnAfterDeserialize()
        {
            Deserialize();
        }

        private void Deserialize()
        {
            _models ??= new Dictionary<T, ObservableModel<U>>();
            _models.Clear();
            foreach (var pair in _catalogElements)
            {
                if (!_models.ContainsKey(pair.key))
                {
                    _models.Add(pair.key, pair.value);
                }
            }
        }
        #endregion

        #region IReadOnlyDictionary implementation
        /// <summary>
        /// Gets the model associated with the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ObservableModel<U> this[T key] => _models[key];

        /// <summary>
        /// Gets the keys of the catalog.
        /// </summary>
        public IEnumerable<T> Keys => _models.Keys;

        /// <summary>
        /// Gets the values (models) of the catalog.
        /// </summary>
        public IEnumerable<ObservableModel<U>> Values => _models.Values;

        /// <summary>
        /// Number of elements in the catalog.
        /// </summary>
        public int Count => _models.Count;

        /// <summary>
        /// Checks if the catalog contains the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(T key)
        {
            return _models.ContainsKey(key);
        }

        /// <summary>
        /// Returns an enumerator for the catalog.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<T, ObservableModel<U>>> GetEnumerator()
        {
            return _models.GetEnumerator();
        }

        /// <summary>
        /// Tries to get the model associated with the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(T key, out ObservableModel<U> value)
        {
            return _models.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
