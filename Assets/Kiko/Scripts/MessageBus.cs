using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageBus
{
    private static MessageBus _instance;
    public static MessageBus Instance
    {
        get
        {
            if (_instance == null)
                _instance = new MessageBus();
            return _instance;
        }
    }

    private Dictionary<string, Action<object>> _eventTable = new Dictionary<string, Action<object>>();

    public void Subscribe(string tag, Action<object> callback)
    {
        if (!_eventTable.ContainsKey(tag))
            _eventTable.Add(tag, null);

        _eventTable[tag] += callback;
    }

    public void Unsubscribe(string tag, Action<object> callback)
    {
        if (_eventTable.ContainsKey(tag))
            _eventTable[tag] -= callback;
    }

    public void Notify(string tag, object arg = null)
    {
        if (_eventTable.ContainsKey(tag) && _eventTable[tag] != null)
            _eventTable[tag].Invoke(arg);
        else Debug.Log($"<color=red>OJO! El evento notificado con tag \"{tag}\" no tiene suscriptores. Revisa la tag o suscríbete.</color>");
    }
}
