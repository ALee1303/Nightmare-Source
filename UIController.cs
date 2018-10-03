using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IObserver<Hostage>
{


    [SerializeField]
    public Slider HealthSlider;
    [SerializeField]
    public Text ScoreText;

    public void Awake()
    {
        
    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception e)
    {
        throw new NotImplementedException();
    }

    public void OnNext(Hostage value)
    {
        throw new NotImplementedException();
    }
}
