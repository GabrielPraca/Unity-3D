using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Physics.Raycast(transform.position, dir, out hit, 15f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                IA_BIXU.roar.gameObject.SetActive(true);
            }
            else
            {
                //IA_BIXU.roar.gameObject.SetActive(false);
            }
        }

    }
}
