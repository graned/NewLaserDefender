using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	// Use this for initialization
	void Start () {
		//this line creates a new isntance of the game object enemyPrefab, which is initialized from the UI
		GameObject enemy = Instantiate (enemyPrefab, new Vector3 (8, 6, 0), Quaternion.identity) as GameObject;
		//this line makes the new instance created, be part of the EnemySpawner game object, 
		/*
		 * IN UI
		 * EnemySpawner
		 * 		|	
		 * 		|-->Enemy(clone) : this is the game object created from the prefab
		 */
		enemy.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
