using UnityEngine;
using System.Collections;

public class RecordOrientationScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touches.Length > 0) {
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began){
				
			}
		
		}
	}
}
