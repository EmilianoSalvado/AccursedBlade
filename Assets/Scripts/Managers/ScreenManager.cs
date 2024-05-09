using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{

    Stack<IScreen> _screenStack;
    public static ScreenManager Instance;
    private void Awake()
    {
        if (Instance != null)
        { Destroy(this); }
        Instance = this;

        _screenStack = new Stack<IScreen>();
    }

    public void Push(IScreen newScreen)
    {
        if (_screenStack.Count > 0)
        {
            _screenStack.Peek().Deactivate();
        }

        _screenStack.Push(newScreen);

        newScreen.Activate();
    }

    public void Push(string screenName)
    {
        var go = Instantiate(Resources.Load<GameObject>($"Screens/{screenName}"));

        Push(go.GetComponent<IScreen>());
    }

    public void Pop()
    {
        if (_screenStack.Count <= 1) return;

        _screenStack.Pop().Free();

        _screenStack.Peek().Activate();
    }
}
