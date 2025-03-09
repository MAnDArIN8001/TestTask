using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utiles.Storage
{
    public class ItemStorage<T> : IDisposable
    {
        public event Action<T> OnAddItem;
        public event Action<T> OnRemoveItem;
        
        private int _storageCapacity;

        private List<T> _storage;

        public bool IsFull => _storage.Count == _storageCapacity;
        public bool IsContainsAnyItem => _storage.Count > 0;

        public IReadOnlyList<T> Storage => _storage;

        public ItemStorage(int storageCapacity)
        {
            _storageCapacity = storageCapacity;
            _storage = new List<T>();
        }

        ~ItemStorage()
        {
            Dispose();
        }

        public bool TryAddItem(T item)
        {
            if (_storage.Count >= _storageCapacity)
            {
                Debug.LogWarning($"Storage {this} is full");
                
                return false;
            }

            if (_storage.Contains(item))
            {
                Debug.LogWarning($"Storage {this} is already contains item {item}");

                return false;
            }
            
            _storage.Add(item);
            
            OnAddItem?.Invoke(item);

            return true;
        }

        public bool TryRemoveItem(T item)
        {
            if (!_storage.Contains(item))
            {
                Debug.LogWarning($"Storage {this} doesnt contains item {item}");

                return false;
            }

            _storage.Remove(item);
            
            OnRemoveItem?.Invoke(item);

            return true;
        }

        public bool TryGetLastItem(out T item)
        {
            var lastItem = _storage.Last();

            if (lastItem is null)
            {
                item = default;
                
                return false;
            }

            item = lastItem;

            return true;
        }
        
        public void Dispose()
        {
            _storage.Clear();   
        }
    }
}