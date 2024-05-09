using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class Config : MonoBehaviour
{
    [SerializeField] Transform _mainGameRoot;
    bool _paused;

    private void Start()
    {
        ScreenManager.Instance.Push(new GameScreen(_mainGameRoot));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _paused = !_paused;
            if (_paused)
            {
                var screenGO = Instantiate(Resources.Load<GameObject>("Assets/Screens/PauseCanvas")).GetComponent<PauseScreen>();
                ScreenManager.Instance.Push(screenGO);
            }
            else
            { ScreenManager.Instance.Pop(); }
        }
    }
}
