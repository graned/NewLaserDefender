using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float movementSpeed; //THIS VARIABLE WILL BE CONTROLLED FROM THE EDITOR
	public GameObject spaceShip;
	private Vector2 spaceShipVector;

	private float cameraMinBoundX,cameraMaxBoundX,cameraMinBoundY,cameraMaxBoundY;
	// Use this for initialization
	void Start () {
		spaceShipVector = spaceShip.transform.position;
		defineWorldBounds (Camera.main);
	}

	/*
	 * THIS METHOD RETRIEVES DINAMICALLY THE BOUNDERIES OF THE CAMERA,
	 * 
	 * VIEWPORTWORLDPOINT TRANSFORM THE CAMERA LIMITS GIVING THE LOWER LEFT CORNER THE COORDINATES OF (0,0)
	 * AND THE UPPER RIGHT CORNER THE COORDINATES OF (1,1)
	 */
	private void defineWorldBounds(Camera camera){
		//We need to calculate the Z distance between the camera and the game object.
		float distance = transform.position.z - camera.transform.position.z; 
		float halfSpaceShipWidth = spaceShip.renderer.bounds.size.x / 2;
		float halfSpaceShipHeight = spaceShip.renderer.bounds.size.y / 2;
		Vector3 cameraVectorLower = new Vector3 (0, 0, distance);
		Vector3 cameraVectorUpper = new Vector3 (1, 1, distance);
		//In order to calculate the space ship bounderies i need to divide the width and height of the game object
		//by 2, this will give me the amount of pixels from the game object origin.
		cameraMinBoundX = camera.ViewportToWorldPoint (cameraVectorLower).x + halfSpaceShipWidth;
		cameraMaxBoundX = camera.ViewportToWorldPoint(cameraVectorUpper).x - halfSpaceShipWidth;
		cameraMinBoundY = camera.ViewportToWorldPoint(cameraVectorLower).y + halfSpaceShipHeight;
		cameraMaxBoundY = camera.ViewportToWorldPoint(cameraVectorUpper).y - halfSpaceShipHeight;
	}
	
	// Update is called once per frame
	void Update () {
		moveSpaceShip ();
	}
	
	/*
	 * CHECKS IF THE ARROW KEYS ARE PRESSED AND THEN MOVES THE SPACE SHIP
	 */
	public void moveSpaceShip(){
		if(Input.GetKey(KeyCode.UpArrow)){
			recalculateSpaceShipPosition(0f,movementSpeed);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			recalculateSpaceShipPosition(0f,(movementSpeed * -1f));
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			recalculateSpaceShipPosition(movementSpeed,0f);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			recalculateSpaceShipPosition((movementSpeed * -1f),0f);
		}
	}


	/*
	 * THIS METHOD WILL MOVE THE SPACE SHIP
	 * 
	 * TIME.DELTATIME: IS USED TO MAKE SURE THAT THE MOVEMENT IS INDEPENDENT FORM THE FRAME RATE
	 */
	public void recalculateSpaceShipPosition(float movementX, float movementY){
		 
		spaceShipVector.x = Mathf.Clamp((spaceShipVector.x + movementX * Time.deltaTime),
		                                cameraMinBoundX,
		                                cameraMaxBoundX) ;
		spaceShipVector.y = Mathf.Clamp((spaceShipVector.y + movementY * Time.deltaTime),
		                                cameraMinBoundY,
		                                cameraMaxBoundY);;
		spaceShip.transform.position = spaceShipVector;	
	}
}
