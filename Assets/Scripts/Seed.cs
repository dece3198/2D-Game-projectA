using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] private float _time;
    public float time
    {
        get { return _time; }
        set 
        {
            _time = value; 
            if(_time > 3600)
            {
                count++;
                if(count >= 3)
                {
                    Instantiate(crops);
                    Destroy(transform);
                }
            }
        }
    }

    [SerializeField] private GameObject crops;
    private int count = 0;

    private void Update()
    {
        time +=  Time.deltaTime;
    }
}
