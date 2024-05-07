using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image _lifeBar, _lifeSubBar, _staminaBar, _staminaSubBar, _energyBar, _energySubBar;
    [SerializeField] float _subBarDelay, _subBarSpeed;

    public Dictionary<UIBars, Image[]> _bars = new Dictionary<UIBars, Image[]>();

    private void Start()
    {
        _bars.Clear();
        _bars.Add(UIBars.LifeBar, new Image[] { _lifeBar, _lifeSubBar });
        _bars.Add(UIBars.StaminaBar, new Image[] { _staminaBar, _staminaSubBar });
        _bars.Add(UIBars.EnergyBar, new Image[] { _energyBar, _energySubBar });
    }

    public void UpdateBar(UIBars bar, float fill)
    {
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
}