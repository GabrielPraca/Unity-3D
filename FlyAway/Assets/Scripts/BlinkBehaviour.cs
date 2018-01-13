using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBehaviour : MonoBehaviour
{
    public float rateBlink;
    private float currentRateBlink;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        currentRateBlink += Time.deltaTime;

        if(currentRateBlink > rateBlink)
        {
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            currentRateBlink = 0;
        }
	}
}
