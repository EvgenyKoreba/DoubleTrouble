using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace CustomEventSystem
{

    public class EventsHandler : MonoBehaviour
    {
        private static Dictionary<Type, SubscribersList<IGlobalSubscriber>> s_Subscribers =
            new Dictionary<Type, SubscribersList<IGlobalSubscriber>>();

        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscribersTypes = EventsHandlerHelper.GetSubscriberTypes(subscriber);
            foreach (Type type in subscribersTypes)
            {
                if (!s_Subscribers.ContainsKey(type))
                {
                    s_Subscribers[type] = new SubscribersList<IGlobalSubscriber>();
                }
                s_Subscribers[type].Add(subscriber);
            }
        }


        public static void Unsubscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscribersTypes = EventsHandlerHelper.GetSubscriberTypes(subscriber);
            foreach (Type type in subscribersTypes)
            {
                if (s_Subscribers.ContainsKey(type))
                {
                    s_Subscribers[type].Remove(subscriber);
                }
            }
        }


        public static void RaiseEvent<TSubscriber>(Action<TSubscriber> action) 
            where TSubscriber : class, IGlobalSubscriber
        {
            SubscribersList<IGlobalSubscriber> subscribers = s_Subscribers[typeof(TSubscriber)];

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
