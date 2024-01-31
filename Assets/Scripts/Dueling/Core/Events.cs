using BBG.Dueling.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace BBG.Dueling
{
    public class Events : MonoBehaviour
    {
        public delegate void OnPerform(DuelAction action);
        
        private Dictionary<string, OnPerform> events;
        private static Events instance;

        private void Awake()
        {
            events = new();
            instance = this;
        }

        private void OnDestroy()
        {
            events.Clear();
            instance = null;
        }

        internal static void TriggerAction(DuelAction action, string name)
        {
            if (instance.events.ContainsKey(name))
                instance.events[name]?.Invoke(action);
        }

        private void Observe(OnPerform method, string eventName)
        {
            if (!events.ContainsKey(eventName))
                events.Add(eventName, null);

            events[eventName] += (method);
        }

        private void RemoveObserve(OnPerform method, string eventName)
        {
            if (events.ContainsKey(eventName))
                events[eventName] -= method;
        }

        internal static void TriggerAction(DuelAction action)
        {
            TriggerAction(action, action.GetType().Name);
        }

        internal static void Observe<T>(OnPerform method) where T : DuelAction
        {
            instance.Observe(method, typeof(T).Name);
        }

        internal static void Observe(EventName name, OnPerform method)
        {
            instance.Observe(method, name.ToString());
        }

        internal static void RemoveObserve(EventName name, OnPerform method)
        {
            instance.RemoveObserve(method, name.ToString()); 
        }

        internal static void ObserveValidate(EventName name, OnPerform method)
        {
            instance.Observe(method, name + ".Validate");
        }

        internal static void RemoveValidate(EventName name, OnPerform method)
        {
            instance.RemoveObserve(method, name + ".Validate");
        }

        internal static void TriggerValidate(DuelAction action)
        {
            TriggerAction(action, action.GetType().Name + ".Validate");
        }

        internal static void TriggerValidate(DuelAction action, EventName eventName)
        {
            TriggerAction(action, eventName + ".Validate");
        }
    }
}