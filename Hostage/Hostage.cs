using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour {

    [SerializeField]
    private int point = 0;
    public int Point { get { return point; } }
    
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            HostageManager.SaveHostage(this);
            ScoreManager.AddScore(point);
            PlayRescueEffect();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void PlayRescueEffect()
    {
        //TODO: Play Animation then destroy object
    }

    public override string ToString()
    {
        return gameObject.name;
    }
}
