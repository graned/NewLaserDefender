using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	//private Sprite[] spriteToLoad;
	public GameObject enemyPrefab;
	public GameObject enemyFormation;
	public float width = 10f, height = 5f;
	//private MovementController movController;
	public float movementSpeed = 15f;

	private EnemySpaceShip enemySpaceShip;
	// Use this for initialization
	void Start () {
		//THIS LINE LOADS ALL SPRITES LOCATED IN THE RESOURCES/ENEMY FILE
		//spriteToLoad = Resources.LoadAll<Sprite> ("Enemy");
		//BY CALLING THE TRANSFORM VARIABLE OF THE ENEMYSPAWNER OBJECT, IT RETRIEVES ALL THE CHILDS IN THE ENEMYSPAWNER
		//IN THIS CASE ALL THE "POSITION" INSTANCES
		spawnEnemies (transform);
		//MOVEMENT CONTROLLER
	/*	movController = new MovementController (enemyFormation);
		movController.MovementSpeed = this.movementSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.ObjectWidth = this.width;
		movController.ObjectHeight = this.height;
		movController.defineWorldBounds (Camera.main);*/
	}

	void OnDrawGizmos(){
		float xmin, xmax, ymin, ymax;
		xmin = transform.position.x - 0.5f * width;
		xmax = transform.position.x + 0.5f * width;
		ymin = transform.position.y - 0.5f * height;
		ymax = transform.position.y + 0.5f * height;
		Gizmos.DrawLine (new Vector3 (xmin, ymin, 0), new Vector3 (xmin, ymax));
		Gizmos.DrawLine (new Vector3 (xmin, ymax, 0), new Vector3 (xmax, ymax));
		Gizmos.DrawLine (new Vector3 (xmax, ymax, 0), new Vector3 (xmax, ymin));
		Gizmos.DrawLine (new Vector3 (xmax, ymin, 0), new Vector3 (xmin, ymin));
	}
	/*
	 * THIS METHOD SPAWNS ALL THE ENEMIES BASED ON A POSITION
	 */
	private void spawnEnemies(Transform trans){
		foreach (Transform t in trans) {
			//this line creates a new isntance of the game object enemyPrefab, which is initialized from the UI
			GameObject enemy = Instantiate (this.enemyPrefab, t.position, Quaternion.identity) as GameObject;//enemySpaceShip.createNewEnemy(t);

			// = (EnemySpaceShip)enemy;
			enemySpaceShip = enemy.GetComponent<EnemySpaceShip>();

			enemySpaceShip.EnemyPrefab = enemySpaceShip.gameObject;
			//enemySpaceShip.Width = width;
			//enemySpaceShip.Height = height;
			enemySpaceShip.movementSpeed = movementSpeed*-1;
			//assignes the new instance created a parent
			enemy.transform.parent = t;
			Position p = t.GetComponent<Position>();
			enemySpaceShip.Radius = p.radius;
			enemySpaceShip.MovBoundriesX = t.transform.position.x;
			enemySpaceShip.MovBoundriesY = t.transform.position.y;
			//break;
			//TEMPORARY FOR LEVEL 1
			//enemySpaceShip.changeSprite(1);
			//changeSprite(enemy,spriteToLoad[Random.Range(0,4)]);
		}
	}
	/*
	//THIS METHOD CHANGES THE ENEMY SPRITE WHEN SPAWING!
	private void changeSprite(GameObject enemySpaceShip, Sprite sprite){
		enemySpaceShip.GetComponent<SpriteRenderer> ().sprite = sprite;
	}
	// Update is called once per frame
	void Update () {
		if (this.transform.position.x == movController.CameraMinBoundX 
		    ||this.transform.position.x == movController.CameraMaxBoundX){
			movController.MovementSpeed *= -1;
		}
		movController.moveObject (movController.MovementSpeed, 0f);
	}*/
}
