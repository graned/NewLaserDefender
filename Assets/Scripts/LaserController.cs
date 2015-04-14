using UnityEngine;
using System.Collections;

public class LaserController{
	private Vector3 laserVector;
	private GameObject laserObject;
	private float laserSpeed;
	private MovementController movController;
	private bool laserFired = false;

	public LaserController(GameObject laserObject, float laserSpeed){
		this.laserObject = laserObject;
		this.laserSpeed = laserSpeed;
	}
	// Use this for initialization
	void Start () {
		movController = new MovementController (laserObject);
		movController.MovementSpeed = this.laserSpeed;
		movController.ObjectToMoveCurrentPosition = laserObject.transform.position;
		movController.defineWorldBounds (Camera.main);
	}
	public void Update(){
		if (laserFired) {
			movController.moveObject (0f, laserSpeed);
		}
	}

	// FIRES THE LASER!
	public void fireLaser(GameObject laserToMove){
		Debug.Log("FIRE THE \"LASER\"");
		movController.ObjectToMove = laserToMove;
		laserFired = true;
	}
}
