using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace CustomEventSystem
{

    internal class EventsHandlerHelper
    {
        private static Dictionary<Type, List<Type>> cachedSubscriberList =
            new Dictionary<Type, List<Type>>();


        public static List<Type> GetSubscriberTypes(IGlobalSubscriber globalSubscriber)
        {
            Type type = globalSubscriber.GetType();
            if (cachedSubscriberList.ContainsKey(type))
            {
                return cachedSubscriberList[type];
            }


            List<Type> subscriberTypes = type
                .GetInterfaces()
                .Where(t => t.GetInterfaces()
                    .Contains(typeof(IGlobalSubscriber)))
                .ToList();

            cachedSubscriberList[type] = subscriberTypes;
            return subscriberTypes;
        }
    }

}
