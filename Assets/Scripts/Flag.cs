using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        // raise flag off ground
        transform.Rotate(0, 5, 0, Space.World);
    }
}