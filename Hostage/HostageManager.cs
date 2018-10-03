using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Contains Saved Hostages
/// </summary>
public class HostageManager : MonoBehaviour, IObservable<Hostage> {

    private List<Hostage> rescues;

    private List<IObserver<Hostage>> observers;
    
    public int SavedHostageCnt { get { return rescues.Count; } }

    // assigned by level manager
    private int hostageCnt;
    
    private static HostageManager instance;
    
    public static HostageManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject hm = new GameObject("HostageManager");
                hm.AddComponent<HostageManager>();
            }

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        rescues = new List<Hostage>();
        observers = new List<IObserver<Hostage>>();
    }

    // called by Hostage when player rescues a hostage
    public static void SaveHostage(Hostage rescuedHostage)
    {
        Debug.Log("Hostage {0} has been rescued", rescuedHostage);
        // Add hostage to the list
        Instance.rescues.Add(rescuedHostage);
        // notify all hostage observers
        foreach (var observer in instance.observers)
            observer.OnNext(rescuedHostage);
        if (IsAllHostageSaved())
        {
            // TODO: Level Clear logic
        }
    }
    
    public static bool IsAllHostageSaved()
    {
        if (instance.hostageCnt == instance.rescues.Count)
            return true;
        return false;
    }

    public IDisposable Subscribe(IObserver<Hostage> observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
        return new Unsubscriber<IObserver<Hostage>>(observers, observer);
    }

    internal class Unsubscriber<T> : IDisposable
    {
        private List<T> _observers;
        private T _observer;

        public Unsubscriber(List<T> observers, T observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}

internal class HostageInvalidException : Exception
{
    internal HostageInvalidException() { }
}