using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour, IObserver<Hostage>
{
    private int score;

    private Text scoreText;

    private IDisposable unsubscriber;

    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject sm = new GameObject("ScoreManager");
                sm.AddComponent<ScoreManager>();
            }

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        Subscribe(HostageManager.Instance);
    }

    public void Subscribe(IObservable<Hostage> provider)
    {
        if (provider != null)
            unsubscriber = provider.Subscribe(this);
    }

    public static void AddScore(int point)
    {
        instance.score += point;
    }

    public static void ReduceScore(int point)
    {
        instance.score -= point;
        if (instance.score <= 0)
            instance.score = 0;
    }

    public void OnNext(Hostage value)
    {
        score = value.Point;
    }

    public void OnError(Exception e)
    {

    }

    public void OnCompleted()
    {

    }
}
