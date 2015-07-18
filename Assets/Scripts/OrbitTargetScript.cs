using UnityEngine;
using System.Collections;

public class OrbitTargetScript : MonoBehaviour {
	public float distance = 10.0F;
	public Transform target;
	
	// Use this for initialization
	void Start () {
	
		if(!target) target = new GameObject().transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Quaternion rotation = transform.rotation;
		Vector3 position = rotation * (distance * Vector3.back) + target.position;
		
		transform.position = position;
	}
}
