using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    public void Notify(params object[] parameters);
}