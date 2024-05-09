using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour, IScreen
{
    public void Activate()
    {
        Debug.Log("Pause");
    }

    public void Deactivate()
    {
        Debug.Log("Continue");
        Free();
    }

    public void Free()
    {
        Destroy(gameObject);
    }
}
