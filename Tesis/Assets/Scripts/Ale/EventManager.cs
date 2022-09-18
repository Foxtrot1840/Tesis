using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void EventReceiver(params object[] parameterContainer);

    static Dictionary<EventsType, EventReceiver> _dictionaryEvents;

    public static void Subscribe(EventsType eventType, EventReceiver listener)
    {
        if (_dictionaryEvents == null)
        {
            _dictionaryEvents = new Dictionary<EventsType, EventReceiver>();
        }

        if (!_dictionaryEvents.ContainsKey(eventType))
        {
            _dictionaryEvents.Add(eventType, null);
        }

        _dictionaryEvents[eventType] += listener;
    }

    public static void Unsubscribe(EventsType eventType, EventReceiver listener)
    {
        if (_dictionaryEvents != null)
        {
            if (_dictionaryEvents.ContainsKey(eventType))
            {
                _dictionaryEvents[eventType] -= listener;
            }
        }
    }

    public static void TriggerEvent(EventsType eventType, params object[] parameters)
    {
        if (_dictionaryEvents == null)
        {
            Debug.Log("No events subscribed");
            return;
        }

        if (_dictionaryEvents.ContainsKey(eventType))
        {
            if (_dictionaryEvents[eventType] != null)
            {
                _dictionaryEvents[eventType](parameters);
            }
        }

    }

    public static void TriggerEvent(EventsType eventType)
    {
        TriggerEvent(eventType, null);
    }

    public enum EventsType
    {
        Event_FinishGame
    }
}