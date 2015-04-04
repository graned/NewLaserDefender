using UnityEngine;
using System.Collections;
/*
 * THIS CLASS HAS AS PURPOSE, TO MANAGE ALL MOVING OBJECTS IN THE GAME 
 */
public class MovementController{
	//this is the game object that we want to move or "animate"
	private GameObject objectToMove;
	//this will be the speed of the movements
	private float movementSpeed;
	//this variables determine the camera bounds
	private float cameraMinBoundX,cameraMaxBoundX,cameraMinBoundY,cameraMaxBoundY;
	//current position of the game object
	private Vector2 objectToMoveCurrentPosition;
	//object's width
	private float objectWidth;
	//object's height
	private float objectHeight;

	//constructor that recieves the game object that will be moved in the game space
	public MovementController(GameObject objectToMove){
		this.objectToMove = objectToMove;
	}

	/*
	 * GETTERS AND SETTERS
	 * *********************************************************************************************
	 */
	public float ObjectHeight {
		get {
			return objectHeight;
		}
		set {
			objectHeight = value;
		}
	}

	public float ObjectWidth {
		get {
			return objectWidth;
		}
		set {
			objectWidth = value;
		}
	}

	public float CameraMinBoundX {
		get {
			return cameraMinBoundX;
		}
	}

	public float CameraMaxBoundX {
		get {
			return cameraMaxBoundX;
		}
	}

	public float CameraMinBoundY {
		get {
			return cameraMinBoundY;
		}
	}

	public float CameraMaxBoundY {
		get {
			return cameraMaxBoundY;
		}
	}

	public Vector2 ObjectToMoveCurrentPosition {
		get {
			return objectToMoveCurrentPosition;
		}
		set {
			objectToMoveCurrentPosition = value;
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
	
	/*
	 * END OF GETTERS AND SETTERS
	 * *********************************************************************************************
	 */

	/*
	 * THIS METHOD RETRIEVES DINAMICALLY THE BOUNDERIES OF THE CAMERA,
	 * 
	 * VIEWPORTWORLDPOINT TRANSFORM THE CAMERA LIMITS GIVING THE LOWER LEFT CORNER THE COORDINATES OF (0,0)
	 * AND THE UPPER RIGHT CORNER THE COORDINATES OF (1,1)
	 * 
	 * THIS VALUES ARE CALCULATED ASSUMING THAT THE PIVOT POINT IS THE CENTER OF THE GAME OBJECT
	 */
	public void defineWorldBounds(Camera camera){
		float halfObjectWidth;
		float halfObjectHeight;
		//We need to calculate the Z distance between the camera and the game object.
		float distance = objectToMove.transform.position.z - camera.transform.position.z; 
		//IF WE DEFINE THE OBJECT's WIDTH AND HEIGHT THEN TAKE THOSE VALUES, OTHERWISE TAKE THE VALES FROM THE 
		//RENDERED OBJECT
		if (objectHeight == 0f && objectWidth == 0f) {
			halfObjectWidth = objectToMove.renderer.bounds.size.x / 2;
			halfObjectHeight = objectToMove.renderer.bounds.size.y / 2;
		} else {
			halfObjectWidth = objectWidth / 2;
			halfObjectHeight = objectHeight / 2;
		}
		Vector3 cameraVectorLower = new Vector3 (0, 0, distance);
		Vector3 cameraVectorUpper = new Vector3 (1, 1, distance);
		//In order to calculate the space ship bounderies i need to divide the width and height of the game object
		//by 2, this will give me the amount of pixels from the game object origin.
		cameraMinBoundX = camera.ViewportToWorldPoint(cameraVectorLower).x + halfObjectWidth;
		cameraMaxBoundX = camera.ViewportToWorldPoint(cameraVectorUpper).x - halfObjectWidth;
		cameraMinBoundY = camera.ViewportToWorldPoint(cameraVectorLower).y + halfObjectHeight;
		cameraMaxBoundY = camera.ViewportToWorldPoint(cameraVectorUpper).y - halfObjectHeight;
	}
	/*
	 * CHECKS IF THE ARROW KEYS ARE PRESSED AND THEN MOVES THE GAME OBJECT
	 */
	public void arrowControl(){
		if(Input.GetKey(KeyCode.UpArrow)){
			moveObject(0f,movementSpeed);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			moveObject(0f,(movementSpeed * -1f));
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			moveObject(movementSpeed,0f);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			moveObject((movementSpeed * -1f),0f);
		}
	}

	/*
	 * THIS METHOD WILL MOVE THE GAME OBJECT. THE OBJECT WILL NOT GO BEYOND THE CAMERA BOUNDARIES
	 * 
	 * TIME.DELTATIME: IS USED TO MAKE SURE THAT THE MOVEMENT IS INDEPENDENT FORM THE FRAME RATE
	 */
	public void moveObject(float movementX, float movementY){
		objectToMoveCurrentPosition.x = Mathf.Clamp((objectToMoveCurrentPosition.x + movementX * Time.deltaTime),
		                                cameraMinBoundX,
		                                cameraMaxBoundX) ;
		objectToMoveCurrentPosition.y = Mathf.Clamp((objectToMoveCurrentPosition.y + movementY * Time.deltaTime),
		                                cameraMinBoundY,
		                                cameraMaxBoundY);;
		objectToMove.transform.position = objectToMoveCurrentPosition;	
	}
}
