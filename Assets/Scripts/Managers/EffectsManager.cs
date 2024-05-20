using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour, IObserver
{
    [SerializeField] PlayerCamera _playerCamera;
    public void Notify(params object[] parameters)
    {
        if ((float)parameters[0] > 0f)
        {
            StartCoroutine(FreezeScreen());
            StartCoroutine(CameraShake());
        }
    }

    IEnumerator FreezeScreen()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(.2f);
        Time.timeScale = 1f;
    }

    IEnumerator CameraShake()
    {
        float duration = .2f;

        Vector3 camInitPos = _playerCamera.transform.position;

        var wait = new WaitForSecondsRealtime(Time.deltaTime);

        while (duration > 0f)
        {
            _playerCamera.CameraShakingPosition(true, camInitPos + (_playerCamera.transform.up + _playerCamera.transform.right) * (.05f), camInitPos);
            yield return wait;
            _playerCamera.CameraShakingPosition(true, camInitPos + (-_playerCamera.transform.up + _playerCamera.transform.right) * (.05f), camInitPos);
            yield return wait;
            _playerCamera.CameraShakingPosition(true, camInitPos + (-_playerCamera.transform.up - _playerCamera.transform.right) * (.05f), camInitPos);
            yield return wait;
            _playerCamera.CameraShakingPosition(true, camInitPos + (_playerCamera.transform.up - _playerCamera.transform.right) * (.05f), camInitPos);
            yield return wait;
            duration -= Time.deltaTime * 16f;
        }

        _playerCamera.CameraShakingPosition(false, camInitPos, camInitPos);
    }
}
