using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace CustomEventSystem
{

    public class EventsHandler : MonoBehaviour
    {
        private static readonly Dictionary<Type, SubscribersList<IGlobalSubscriber>> _subscribers =
            new Dictionary<Type, SubscribersList<IGlobalSubscriber>>();

        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscribersTypes = EventsHandlerHelper.GetSubscriberTypes(subscriber);
            foreach (Type type in subscribersTypes)
            {
                if (!_subscribers.ContainsKey(type))
                {
                    _subscribers[type] = new SubscribersList<IGlobalSubscriber>();
                }
                _subscribers[type].Add(subscriber);
            }
        }


        public static void Unsubscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscribersTypes = EventsHandlerHelper.GetSubscriberTypes(subscriber);
            foreach (Type type in subscribersTypes)
            {
                if (_subscribers.ContainsKey(type))
                {
                    _subscribers[type].Remove(subscriber);
                }
            }
        }


        public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action) 
            where TSubscriber : class, IGlobalSubscriber
        {
            SubscribersList<IGlobalSubscriber> subscribers = _subscribers[typeof(TSubscriber)];

            subscribers.Executing = true;
            foreach (IGlobalSubscriber subscriber in subscribers.List)
            {
                try
                {
                    action.Invoke(subscriber as TSubscriber);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            }

            subscribers.Executing = false;
            subscribers.Cleanup();
        }
    }

}
