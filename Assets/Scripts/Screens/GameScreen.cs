using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : IScreen
{
    Dictionary<Behaviour, bool> _before = new Dictionary<Behaviour, bool>();

    Transform _root;

    public GameScreen(Transform root)
    {
        _root = root;

        foreach (var beh in _root.GetComponentsInChildren<Behaviour>())
        { _before.Add(beh, beh.enabled); }
    }
    public void Activate()
    {
        foreach (var beh in _before)
        { beh.Key.enabled = beh.Value; }
    }

    public void Deactivate()
    {
        foreach (var beh in _root.GetComponentsInChildren<Behaviour>())
        {
            _before[beh] = beh.enabled;
            beh.enabled = false;
        }
    }

    public void Free()
    {
        GameObject.Destroy(_root.gameObject);
    }
}
