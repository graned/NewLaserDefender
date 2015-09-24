using UnityEngine;
using System.Collections;

public class EnemySpaceShip : MonoBehaviour {	
	private Sprite[] spriteToLoad;
	private GameObject enemyPrefab;
	private float width, height;
	private MovementController movController;
	public float movementSpeed;

	/**
	 * *********************************************************************
	 * PROPERTIES
	 */ 
	public MovementController MovController {
		get {
			return movController;
		}
		set {
			movController = value;
		}
	}

	public float MovementSpeed {
		get {
			return movementSpeed;
		}
		set {
			movementSpeed = value;
		}
	}

	public float Height {
		get {
			return height;
		}
		set {
			height = value;
		}
	}

	public float Width {
		get {
			return width;
		}
		set {
			width = value;
		}
	}

	public Sprite[] SpriteToLoad {
		get {
			return spriteToLoad;
		}
		set {
			spriteToLoad = value;
		}
	}

	public GameObject EnemyPrefab {
		get {
			return enemyPrefab;
		}
		set {
			enemyPrefab = value;
		}
	}
	/**
	 * PROPERTIES END
	 * *****************************************************************************
	 */

	// Use this for initialization
	void Start () {
		//THIS LINE LOADS ALL SPRITES LOCATED IN THE RESOURCES/ENEMY FILE
		spriteToLoad = Resources.LoadAll<Sprite> ("Enemy");
		//MOVEMENT CONTROLLER
		movController = new MovementController (enemyPrefab);
		movController.MovementSpeed = this.movementSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.ObjectWidth = this.width;
		movController.ObjectHeight = this.height;
		movController.defineWorldBounds (Camera.main);
	}

	//THIS METHOD CHANGES THE ENEMY SPRITE WHEN SPAWING!
	public void changeSprite(int levelEnemies){
		this.GetComponent<SpriteRenderer> ().sprite = spriteToLoad[Random.Range(0,levelEnemies)];
	}

	//CREATES A NEW ENEMY
	/*public static GameObject createNewEnemy(Transform t){
		return Instantiate (this.enemyPrefab, t.position, Quaternion.identity) as GameObject;
	}*/

	//TODO DETECT COLLISION OF LASER!

	// Update is called once per frame
	void Update () {
		if (this.transform.position.x <= movController.CameraMinBoundX 
		    ||this.transform.position.x >= movController.CameraMaxBoundX){
			movController.MovementSpeed *= -1;
		}
		Debug.Log(movController.MovementSpeed);
		movController.moveObject (movController.MovementSpeed, 0f);
	}
}
