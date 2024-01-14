using System.Collections.Generic;
using UnityEngine;
namespace PrisionGuard
{
    public class ObjectPool<T>
    {
        private GameObject _prefab;
        private List<GameObject> _activeList;
        private Queue<GameObject> _idleList;

        public ObjectPool(GameObject prefab)
        {
            _activeList = new List<GameObject>();
            _idleList = new Queue<GameObject>();
            _prefab = prefab;
        }

        public T GetObject(T _default)
        {
            if (_idleList.Count > 0)
            {
                GameObject oldObject = _idleList.Dequeue();
                _activeList.Add(oldObject);
                oldObject.SetActive(true);
                return oldObject.GetComponent<T>();
            }

            GameObject newObject = GameObject.Instantiate(_prefab);
            _activeList.Add(newObject);
            return newObject.GetComponent<T>();
        }

        public void DisableObject(GameObject old)
        {
            if (_activeList.Contains(old))
            {
                _activeList.Remove(old);
            }
        }

        public void ReturnToPool(GameObject old)
        {
            _idleList.Enqueue(old);
        }

        public int GetCount()
        {
            return _activeList.Count;
        }
    }
}