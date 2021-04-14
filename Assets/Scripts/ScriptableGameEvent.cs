using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ScriptableGameEvent01", menuName = "Game events/Game event")]
public class ScriptableGameEvent : ScriptableObject {
    [SerializeField] string _eventName;

    private List<GameEventRaiser> _gameEventRaisers = new List<GameEventRaiser>();
    private List<GameEventListener> _gameEventListener = new List<GameEventListener> ();

    public string EventName { get { return _eventName; } }

    public void Raise () 
    {
        for (int i = _gameEventListener.Count - 1; i >= 0; i--)         
            _gameEventListener[i].OnEventRaised (this);        
    }

    public void ReigsterRaiser(GameEventRaiser p_listener)
    {
        if (!_gameEventRaisers.Contains(p_listener))
            _gameEventRaisers.Add(p_listener);
    }

    public void UnreigsterRaiser(GameEventRaiser p_listener)
    {
        _gameEventRaisers.Remove(p_listener);
    }

    public void ReigsterListener (GameEventListener p_listener)
    {
        if (!_gameEventListener.Contains (p_listener))
            _gameEventListener.Add (p_listener);        
    }

    public void UnreigsterListener (GameEventListener p_listener) 
    {
        _gameEventListener.Remove (p_listener);
    }
}