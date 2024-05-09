using UnityEngine;

public class Config : MonoBehaviour
{
    [SerializeField] Transform _mainGameRoot;
    public Transform MainGameRoot {  get { return _mainGameRoot; } }
    bool _paused;

    public static Config Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        { Destroy(this); return; }
        Instance = this;
    }

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
                ScreenManager.Instance.Push(Instantiate(Resources.Load<PauseScreen>("Screens/PauseCanvas")));
            }
            else
            { ScreenManager.Instance.Pop(); }
        }
    }
}