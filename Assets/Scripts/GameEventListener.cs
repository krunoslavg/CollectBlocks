using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : GameEntityComponent
{
    [System.Serializable]
    private struct EventResponseStruct 
    {
        [SerializeField] ScriptableGameEvent _gameEvent;
        [SerializeField] List<UnityEvent> _unityEvents;

        public ScriptableGameEvent GameEvent { get { return _gameEvent; } }
        public List<UnityEvent> UnityEvents { get { return _unityEvents; } }
    }

    [SerializeField] List<EventResponseStruct> _eventResponseStruct;

    void OnEnable ()
    {
        for (int i = 0; i < _eventResponseStruct.Count; i++) 
        {
            if (_eventResponseStruct != null && _eventResponseStruct.Count > 0)
                _eventResponseStruct[i].GameEvent.ReigsterListener (this);
        }
    }

    void OnDisable () 
    {
        if (_eventResponseStruct != null && _eventResponseStruct.Count > 0) 
        {
            for (int i = 0; i < _eventResponseStruct.Count; i++) 
            {
                _eventResponseStruct[i].GameEvent.UnreigsterListener (this);
            }
        }
    }

    public void OnEventRaised (ScriptableGameEvent p_sourceGameEvent) 
    { 
        if (_eventResponseStruct != null && _eventResponseStruct.Count > 0) 
        {
            for (int i = 0; i < _eventResponseStruct.Count; i++) 
            {
                if (_eventResponseStruct[i].GameEvent == p_sourceGameEvent) 
                {
                    for (int j = 0; j < _eventResponseStruct[i].UnityEvents.Count; j++) 
                    {
                        _eventResponseStruct[i].UnityEvents[j].Invoke ();
                    }
                    break;
                }
            }
        }
    }
}