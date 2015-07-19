using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

[SerializeField]
public class Vector3Time
{
	public Vector3 position;
	public float time;
	
	public override string ToString ()
	{
		return time.ToString("F2") + "s: " + position;
	}
	
	public string ToJson(){
		return "{" + 
			"\"time\":" + time.ToString ("F2") + "," + 
			"\"position\":{" +
				"\"x\":" + position.x.ToString ("F2") + ", " +
				"\"y\":" + position.y.ToString ("F2") + ", " +
				"\"z\":" + position.z.ToString ("F2") + 
			"}" + 
		"}";
	}
}

public class RecordOrientationScript : MonoBehaviour {
	[SerializeField]
	List<Vector3Time> positions;
	
	[SerializeField]
	GameObject gameManager;
	AudioSource audio;
	
	float time;
	
	// Use this for initialization
	void Start () {
		time = 0.0F;
		positions = new List<Vector3Time>();
		audio = gameManager.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touches.Length > 0) {
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began){
				if(!audio.isPlaying) audio.Play();
				AddPosition();
			}
			if(t.deltaTime > 1.0f){
				audio.Stop ();
				SaveFile ("output");
			}
		}
		
		// Mouse controls
		// Clicking
		if(Input.GetMouseButtonDown(0)){
			if(!audio.isPlaying) audio.Play();
			AddPosition ();
		}
		// Right Clicking
		if(Input.GetMouseButtonDown(1)){
			audio.Pause();
			SaveFile ("output");
		}
	}
	
	void AddPosition(){
		Vector3Time v = new Vector3Time();
		
		v.position = transform.position;
		v.time = audio.time;
		
		positions.Add(v);
		Debug.Log (v.ToJson());
	}
	
	void SaveFile(string fileName){
		string output = "";
		output += "{";
		output += "\"positions\":[";
		string outputPositions = "";
		for(int x = 0; x < positions.Count; x++){
			if(outputPositions.Length > 0) outputPositions += ", ";
			outputPositions += positions[x].ToJson();
		}
		output += outputPositions;
		output += "]";
		output += "}";
		
//		Debug.Log (output);
		int i = 0;
		string filePath = fileName + ".json";
		while (File.Exists(filePath))
		{
			i++;
			filePath = fileName + i + ".json";
		}
		StreamWriter sr = File.CreateText(filePath);
		sr.WriteLine (output);
		sr.Close();
		Debug.Log("Wrote " + filePath);
	}
}
