using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventRaiser : MonoBehaviour 
{
    [SerializeField] List<ScriptableGameEvent> _gameEvents;

    public List<ScriptableGameEvent> GameEvents { get { return _gameEvents; } }

    public void RaiseAllEvents () 
    {
        for (int i = 0; i < _gameEvents.Count; i++) 
        {
            _gameEvents[i].Raise ();
        }
    }

    public void RaiseEventAtIndex (int p_index) 
    {
        _gameEvents[p_index].Raise ();
    }

    public void RaiseEventNamed (string p_value) 
    {
        for (int i = 0; i < _gameEvents.Count; i++) 
        {
            if (_gameEvents[i].EventName == p_value)
                _gameEvents[i].Raise ();
        }
    }
}