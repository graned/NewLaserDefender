using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	private Sprite[] spriteToLoad;
	public GameObject enemyPrefab;
	// Use this for initialization
	void Start () {
		//THIS LINE LOADS ALL SPRITES LOCATED IN THE RESOURCES/ENEMY FILE
		spriteToLoad = Resources.LoadAll<Sprite> ("Enemy");
		//BY CALLING THE TRANSFORM VARIABLE OF THE ENEMYSPAWNER OBJECT, IT RETRIEVES ALL THE CHILDS IN THE ENEMYSPAWNER
		//IN THIS CASE ALL THE "POSITION" INSTANCES
		spawnEnemies (transform);
	}
	/*
	 * THIS METHOD SPAWNS ALL THE ENEMIES BASED ON A POSITION
	 */
	private void spawnEnemies(Transform trans){
		foreach (Transform t in trans) {
			//this line creates a new isntance of the game object enemyPrefab, which is initialized from the UI
			GameObject enemy = Instantiate (enemyPrefab, t.position, Quaternion.identity) as GameObject;
			//assignes the new instance created a parent
			enemy.transform.parent = t;
			//TEMPORARY FOR LEVEL 1
			changeSprite(enemy,spriteToLoad[Random.Range(0,4)]);
		}
	}
	//THIS METHOD CHANGES THE ENEMY SPRITE WHEN SPAWING!
	private void changeSprite(GameObject enemySpaceShip, Sprite sprite){
		enemySpaceShip.GetComponent<SpriteRenderer> ().sprite = sprite;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
