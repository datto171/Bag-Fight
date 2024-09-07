using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdvancedModule.ObserverPattern
{
    [DefaultExecutionOrder(-9999)]
    public class OrderedEventDispatcher
    {
        #region Global Instance
        private static OrderedEventDispatcher _globalInstance;
        public static OrderedEventDispatcher GlobalInstance => _globalInstance ??= new OrderedEventDispatcher();
        #endregion

        #region Scene Instance
        private static OrderedEventDispatcher _sceneInstance;
        public static OrderedEventDispatcher SceneInstance
        {
            get
            {
                if (_sceneInstance != null) return _sceneInstance;
                _sceneInstance = new OrderedEventDispatcher();
                SceneManager.sceneLoaded += OnSceneLoaded;
                return _sceneInstance;
            }
        }

        private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg1 == LoadSceneMode.Single)
            {
                _sceneInstance._delegateMap.Clear();
                _sceneInstance._lookupMap.Clear();
            }
        }
        #endregion

        private readonly Dictionary<Type, Dictionary<int, EventDelegate>> _delegateMap = new();
        private readonly Dictionary<Delegate, EventDelegate> _lookupMap = new();
        private readonly List<EventDelegate> _faultyDelegates = new();
        public void AddListener<T>(EventDelegate<T> handler, int order = 0) where T : IEventData
        {
            EventDelegate internalDelegate = (e) => handler((T)e);
            if (_lookupMap.ContainsKey(handler))
            {
                return;
            }
            _lookupMap[handler] = internalDelegate;
            if (_delegateMap.ContainsKey(typeof(T)))
            {
                if (_delegateMap[typeof(T)].ContainsKey(order))
                {
                    _delegateMap[typeof(T)][order] += internalDelegate;
                }
                else
                {
                    _delegateMap[typeof(T)].Add(order, internalDelegate);
                }
            }
            else
            {
                _delegateMap[typeof(T)] = new Dictionary<int, EventDelegate>() { { order, internalDelegate } };
            }
        }

        public void RemoveListener<T>(EventDelegate<T> handler) where T : IEventData
        {
            if (_lookupMap.TryGetValue(handler, out EventDelegate internalDelegate))
            {
                if (_delegateMap.ContainsKey(typeof(T)))
                {
                    foreach (var order in _delegateMap[typeof(T)].Keys.ToList())
                    {
                        _delegateMap[typeof(T)][order] -= internalDelegate;
                    }
                }
                _lookupMap.Remove(handler);
            }
        }

        public void TriggerEvent<T>(T currentEvent) where T : IEventData
        {
            if (_delegateMap.ContainsKey(typeof(T)))
            {
                var keys = _delegateMap[typeof(T)].Keys.ToList();
                var sortedKeys = _delegateMap[typeof(T)].Keys.OrderBy(k => k).ToList();
                foreach (var key in sortedKeys)
                {
                    var handler = _delegateMap[typeof(T)][key];
                    try
                    {
                        handler?.Invoke(currentEvent);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Error while invoking event: " + e);
                        if (handler != null)
                        {
                            _faultyDelegates.Add(handler);
                        }
                    }
                }
                foreach (var faultyDelegate in _faultyDelegates)
                {
                    foreach (var order in keys)
                    {
                        _delegateMap[typeof(T)][order] -= faultyDelegate;
                    }
                    _lookupMap.Remove(faultyDelegate);
                }
            }
            else
            {
                Debug.Log("The type " + typeof(T) + " hasn't been registered in event system");
            }
        }
    }
}