using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float movementSpeed; //THIS VARIABLE WILL BE CONTROLLED FROM THE EDITOR
	public GameObject laserPrefab;
	public GameObject spaceShip;
	public float laserSpeed = 15f;
	private MovementController movController;
	private LaserController laserController;


	// Use this for initialization
	void Start () {
		movController = new MovementController (spaceShip);
		movController.ObjectToMoveCurrentPosition = spaceShip.transform.position;
		movController.MovementSpeed = movementSpeed;
		movController.defineWorldBounds (Camera.main);
		laserController = new LaserController (laserPrefab, laserSpeed);
	}
			
	// Update is called once per frame
	void Update () {
		movController.arrowControl ();
		KeyPressed ();
	}	

	private void KeyPressed(){
		if(Input.GetKeyDown(KeyCode.Space)){
			//SPAWN LASER BEAM & FIRE LASER!
			spawnFireLaser(transform);
		}
	}

	/*
	 * THIS METHOD SPAWNS LASER BEAM
	 */
	private void spawnFireLaser(Transform trans){
		foreach (Transform t in trans) {
			//this line creates a new isntance of the game object enemyPrefab, which is initialized from the UI
			GameObject laser = Instantiate (laserPrefab, t.position, Quaternion.identity) as GameObject;
			//assignes the new instance created a parent
			laser.transform.parent = t;
			laserController.fireLaser (laser);
		}
	}
}
