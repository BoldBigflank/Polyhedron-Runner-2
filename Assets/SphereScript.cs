using UnityEngine;
using System.Collections;

public class SphereScript : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
	}
	
	void Awake(){
		gameObject.transform.localScale = Vector3.one * 0.01F;
		gameObject.transform.position = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("End")) End ();
	}
	
	void End() {
		gameObject.SetActive(false);
	}
}
