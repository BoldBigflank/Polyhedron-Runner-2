using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewObjectPoolerScript : MonoBehaviour {

	public static NewObjectPoolerScript current;
	public int pooledAmount = 10;
	public bool willGrow = true;
	public GameObject[] pooledPrefabs;
	
	Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();
//	List<GameObject> pooledObjects;
	
	
	void Awake (){
		current = this;
	}
	// Use this for initialization
	void Start () {
		// Make a pool for each object
		
		foreach(GameObject p in pooledPrefabs){
			List<GameObject> pooledObjects = new List<GameObject>();
			Debug.Log ("Creating pool for " + p.name);
			pools.Add (p.name, pooledObjects);
			for(int i = 0; i < pooledAmount; i++){
				AddObject(p.name);
			}
			
		}
		
	}
	
	List<GameObject> GetPool (string name){
		return (pools.ContainsKey(name)) ? pools[name] : null;
	}
	
	GameObject GetPrefab (string name){
		foreach(GameObject p in pooledPrefabs){
			if(p.name == name) return p;
		}
		return null;
	}
	
	GameObject AddObject(string prefabName){
		List<GameObject> pool = GetPool (prefabName);
		GameObject pooledObject = GetPrefab (prefabName);
		GameObject obj = (GameObject)Instantiate(pooledObject);
		obj.SetActive(false);
		pool.Add(obj);
		obj.transform.parent = transform;
		return obj;
	}
	
	public GameObject Spawn(string prefabName){
		List<GameObject> pool = GetPool (prefabName);
		if(pool == null){
			Debug.Log ("Pool not found: " + prefabName);
			return null;
		}
		for(int i = 0; i < pool.Count; i++){
			if(!pool[i].activeInHierarchy){
				return pool[i];
			}
		}
		
		if(willGrow){
			return AddObject(prefabName);
		}
		
		return null;
	}
	
	public GameObject Spawn(string prefabName, Vector3 pos){
		GameObject g = Spawn (prefabName);
		g.transform.position = pos;
		return g;
	}
}
