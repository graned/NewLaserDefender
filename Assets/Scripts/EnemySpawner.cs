using UnityEngine;
using System.Collections;


public class EnemySpawner : MonoBehaviour {
	private Sprite[] spriteToLoad;
	private int numberOfEnemies;

	public GameObject enemyPrefab;
	public float width = 10f, height = 5f;
	public float movementSpeed = 15f;
	public float enemyLevel = 1;
	public float spawnDelay = 0.5f;

	private EnemySpaceShip enemySpaceShip;
	// Use this for initialization
	void Start () {
		
		//THIS LINE LOADS ALL SPRITES LOCATED IN THE RESOURCES/ENEMY FILE
		spriteToLoad = Resources.LoadAll<Sprite> ("Enemy");

		//BY CALLING THE TRANSFORM VARIABLE OF THE ENEMYSPAWNER OBJECT, IT RETRIEVES ALL THE CHILDS IN THE ENEMYSPAWNER
		//IN THIS CASE ALL THE "POSITION" INSTANCES
		spawnEnemies ();
		//numberOfEnemies = transform.childCount;

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
	private void spawnEnemies(){
		//execute only 6 times or the number of enemies allowed in the game screen
		//using recursion
		Transform nextFreeTransform = nextFreePosition(transform);
		//check if there is not gameobject of type enemySpaceShip at position nextFreeTransform
		if (nextFreeTransform) {
			numberOfEnemies++;
			spawnEnemy (nextFreeTransform);
			//if(numberOfEnemies <= 6)
				Invoke ("spawnEnemies", spawnDelay);
		}
	}

	public void enemyGotDestroyNotifier(){
		this.numberOfEnemies--;
	}

	private int getSpaceshipType(int type){
		return type <= 5 ? type : getSpaceshipType (type - 5);
	}
	//THIS METHOD CHANGES THE ENEMY SPRITE WHEN SPAWING!
	private void changeSprite(GameObject enemySpaceShip, Sprite sprite){
		enemySpaceShip.GetComponent<SpriteRenderer> ().sprite = sprite;
	}
	// Update is called once per frame
	void Update () {
		//check if there are no more enemies
		if(numberOfEnemies <= 0){
			Debug.Log ("NO MORE ENEMIES LEFT!");
			spawnEnemies ();
		}
	}

	private Transform nextFreePosition(Transform transform){
		foreach (Transform nextTransform in transform) {
			if (nextTransform.childCount <= 0) {
				return nextTransform;
			}
		}
		return null;
	}

	private void spawnEnemy(Transform transform){
		//this line creates a new isntance of the game object enemyPrefab, which is initialized from the UI
		GameObject enemy = Instantiate (this.enemyPrefab, transform.position, Quaternion.identity) as GameObject;//enemySpaceShip.createNewEnemy(t);

		//THIS LINE GETS THE INSTANCE OF THE ENEMY SPACESHIP CREATED
		enemySpaceShip = enemy.GetComponent<EnemySpaceShip>();

		//DETERMINES WHICH LEVEL WILL THE SPAWN SPACESHIP WILL BE
		int enemySpaceShipSpawnLevel = (int)Random.Range(1.0f,enemyLevel);

		//SELECTS WHICH SPRITE WILL RENDER BASED ON THE ENEMY SPAWN LEVEL
		float spriteIndex = Random.Range(1.0f,enemySpaceShipSpawnLevel * 5.0f);

		//setting the enemy space ship movement speed
		//adding random movement based on the level to try to avoid overlapping
		enemySpaceShip.movementSpeed = Random.Range(spriteIndex,movementSpeed);

		//firerate of the space ship
		enemySpaceShip.ShotsPerSecond = enemySpaceShipSpawnLevel;

		//this helps determining the type enemy is, also helps on the collider assignation, not max inclusive for int
		enemySpaceShip.EnemyType = getSpaceshipType((int)spriteIndex); 

		//assignes the new instance created a parent
		enemy.transform.parent = transform;

		//gets the position game object were the spaceship si being created
		Position p = transform.GetComponent<Position>();

		//set the radius, so we can draw the gizmos on each enemy space ship
		enemySpaceShip.Radius = p.radius;

		//this line changes the enemy sprite to the one that belongs to the current level
		changeSprite(enemySpaceShip.gameObject,spriteToLoad[(int)spriteIndex - 1]);

		//sets the enemy value based on level and type of spaceShip
		enemySpaceShip.EnemyValue = enemySpaceShipSpawnLevel * (int)spriteIndex * 100;

	}
}
