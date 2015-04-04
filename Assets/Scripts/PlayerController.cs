using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float movementSpeed; //THIS VARIABLE WILL BE CONTROLLED FROM THE EDITOR
	public GameObject spaceShip;
	private MovementController movController;

	// Use this for initialization
	void Start () {
		movController = new MovementController (spaceShip);
		movController.ObjectToMoveCurrentPosition = spaceShip.transform.position;
		movController.MovementSpeed = movementSpeed;
		movController.defineWorldBounds (Camera.main);
	}
			
	// Update is called once per frame
	void Update () {
		movController.arrowControl ();
	}	
}
