using UnityEngine;

namespace TicTacToe.Core
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (InstanceNull())
                {
                    _instance = FindAnyObjectByType<T>();

                    if (InstanceNull())
                    {
                        var gameObject = new GameObject(typeof(T).Name);
                        _instance = gameObject.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (InstanceNull())
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
                Destroy(gameObject);
        }

        private static bool InstanceNull() => _instance == null;
    }
}