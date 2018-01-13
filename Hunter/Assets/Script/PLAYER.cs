using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour {

    public static Vector3 position;

	// Use this for initialization
	void Start () {

        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        position = transform.position;
	}
}
