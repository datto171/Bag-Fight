using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdvancedModule.ObserverPattern
{
    public interface IEventData
    {
    }

    public delegate void EventDelegate<T>(T e) where T : IEventData;

    public delegate void EventDelegate(IEventData e);

    [DefaultExecutionOrder(-9999)]
    public class EventDispatcher
    {
        #region Global Instance

        private static EventDispatcher _globalInstance;

        public static EventDispatcher GlobalInstance
        {
            get
            {
                if (_globalInstance != null) return _globalInstance;
                _globalInstance = new EventDispatcher();
                return _globalInstance;
            }
        }

        #endregion

        #region Scene Instance

        private static EventDispatcher _sceneInstance;

        public static EventDispatcher SceneInstance
        {
            get
            {
                if (_sceneInstance != null) return _sceneInstance;
                _sceneInstance = new EventDispatcher();
                SceneManager.sceneLoaded += OnSceneLoaded;
                return _sceneInstance;
            }
        }

        private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            _sceneInstance._delegateMap.Clear();
            _sceneInstance._lookupMap.Clear();
        }

        #endregion

        private readonly Dictionary<Type, EventDelegate> _delegateMap = new();

        private readonly Dictionary<Delegate, EventDelegate> _lookupMap = new();

        private readonly List<EventDelegate> _faultyDelegates = new List<EventDelegate>();

        public void AddListener<T>(EventDelegate<T> handler) where T : IEventData
        {
            EventDelegate internalDelegate = (e) => handler((T)e);
            if (_lookupMap.ContainsKey(handler))
            {
                return;
            }

            _lookupMap[handler] = internalDelegate;
            if (_delegateMap.ContainsKey(typeof(T)))
            {
                _delegateMap[typeof(T)] += internalDelegate;
            }
            else
            {
                _delegateMap[typeof(T)] = internalDelegate;
            }
        }

        public void RemoveListener<T>(EventDelegate<T> handler) where T : IEventData
        {
            if (_lookupMap.TryGetValue(handler, out EventDelegate internalDelegate))
            {
                if (_delegateMap.ContainsKey(typeof(T)))
                {
                    _delegateMap[typeof(T)] -= internalDelegate;
                }

                _lookupMap.Remove(handler);
            }
        }

        public void TriggerEvent<T>(T currentEvent) where T : IEventData
        {
            if (_delegateMap.ContainsKey(typeof(T)))
            {
                var handler = _delegateMap[typeof(T)];
                try
                {
                    handler?.Invoke(currentEvent);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error in event system: " + e);
                    if (handler != null)
                    {
                        _faultyDelegates.Add(handler);
                    }
                }

                foreach (var faultyDelegate in _faultyDelegates)
                {
                    _delegateMap[typeof(T)] -= faultyDelegate;
                    _lookupMap.Remove(faultyDelegate);
                }
            }
        }

        public void Clear()
        {
            _delegateMap.Clear();
            _lookupMap.Clear();
        }
    }
}