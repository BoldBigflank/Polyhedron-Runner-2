using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {

	// JSON Data
	public TextAsset spheresJSON;
	List<JSONObject> spheres;
	JSONObject currentShape;
	int currentShapeIndex;
	float songTime;
	
	// Game Variables
	float timer;
	bool gameInProgress;
	AudioSource gameAudio;
	public bool recordAudio;
	
	// Use this for initialization
	void Start () {
		Debug.Log("GameManager Start");
		// Load JSON
		JSONObject j = new JSONObject(spheresJSON.ToString());
		spheres = j.GetField ("spheres").list;
		currentShapeIndex = 0;
		
		// Game Variables Init
		timer = 0.0F;
		gameAudio = gameObject.GetComponent<AudioSource>();
		gameAudio.clip = (AudioClip)Resources.Load (j.GetField ("song").str);
		songTime = j.GetField("time").n;
	}
	
	// Update is called once per frame
	void Update () {
		if(recordAudio) return;
		if(gameInProgress){
			timer += Time.deltaTime;
			// When timer is ~4s before, create a shape
			currentShape = spheres[currentShapeIndex% spheres.Count ];
			if(currentShape.GetField ("time").n <  timer % songTime ){
				// Create shape
				NextShape (currentShape);
				
				// Increment index
				currentShapeIndex++;
			}
		}
		else { // Game is not currently in progress
			if(Input.touches.Length > 0) {
				Debug.Log("GameManager Start");
				Touch t = Input.GetTouch(0);
				if(t.phase == TouchPhase.Began){
					Play ();
				}
			}
			// Mouse controls
			// Clicking
			if(Input.GetMouseButtonDown(0)){
				Play ();
			}
		
		}
		
	}
	
	void Play(){
		if(!gameAudio.isPlaying) gameAudio.Play();
		timer = 0.0F;
		gameInProgress = true;
	}
	
	void NextShape(JSONObject shapeJSON){
		GameObject shape = NewObjectPoolerScript.current.Spawn("Sphere");
		if(!shape) return;
		JSONObject positionJSON = shapeJSON.GetField ("position");
		JSONObject rotationJSON = shapeJSON.GetField ("rotation");
		Debug.Log (rotationJSON);
		shape.transform.LookAt(new Vector3(positionJSON.GetField ("x").n, positionJSON.GetField ("y").n, positionJSON.GetField ("z").n), Vector3.up); // = Quaternion.Euler( rotationJSON.GetField("x").n, rotationJSON.GetField("y").n, 0.0F);
		shape.SetActive(true);
		shape.transform.parent = gameObject.transform;
	}
}
