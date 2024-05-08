using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IObserver
{
    [SerializeField] Image _lifeBar, _lifeSubBar, _staminaBar, _staminaSubBar, _energyBar, _energySubBar;
    [SerializeField] TextMeshProUGUI _batteriesAmountTMP;
    [SerializeField] float _subBarDelay, _subBarSpeed;

    Dictionary<UIBars, Image[]> _bars = new Dictionary<UIBars, Image[]>();
    Action<int> _setBatteriesAmount;

    private void Awake()
    {
        _bars.Clear();
        _bars.Add(UIBars.LifeBar, new Image[] { _lifeBar, _lifeSubBar });
        _bars.Add(UIBars.StaminaBar, new Image[] { _staminaBar, _staminaSubBar });
        _bars.Add(UIBars.EnergyBar, new Image[] { _energyBar, _energySubBar });

        _setBatteriesAmount = x => _batteriesAmountTMP.text = x.ToString();
    }

    public void UpdateBar(UIBars bar, float fill)
    {
        fill = Mathf.Clamp(fill, 0f, fill);

        _bars[bar][0].fillAmount = fill;

        if (_bars[bar][1].fillAmount < fill)
        {
            _bars[bar][1].fillAmount = fill;
            return;
        }

        StopAllCoroutines();
        StartCoroutine(BarRoutine(_bars[bar][0], _bars[bar][1]));

        foreach (var imagePair in _bars
            .Where(x => x.Value[0].fillAmount < x.Value[1].fillAmount).ToArray()
            .Select(x => { return new Image[] { x.Value[0], x.Value[1] }; }).ToArray())
        { StartCoroutine(BarRoutine(imagePair[0], imagePair[1])); }
    }

    IEnumerator BarRoutine(Image barA, Image barB)
    {
        yield return new WaitForSeconds(_subBarDelay);

        while (barA.fillAmount > barB.fillAmount)
        {
            barB.fillAmount -= (barB.fillAmount - barA.fillAmount) * (_subBarSpeed * Time.deltaTime);

            yield return new WaitForSeconds(Time.deltaTime);
        }

    }

    public void Notify(params object[] parameters)
    {
        UpdateBar((UIBars)parameters[0], (float)parameters[1]);

        if (parameters.Length > 2)
        {
            _setBatteriesAmount((int)parameters[2]);
        }
    }
}