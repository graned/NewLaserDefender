﻿using UnityEngine;
using System.Collections;
using GameParentObjectAssembly;

public class PlayerController : MonoBehaviour {
	public float movementSpeed; //THIS VARIABLE WILL BE CONTROLLED FROM THE EDITOR
	public GameObject spaceShip;
	private MovementController movController;
	public GameObject laserPrefab;
	public float repeatRate = 0.2f;
	public float health = 500f;
	private AudioSource audioSource;
	public AudioClip laserAudioClip;

	void Start () {
		movController = new MovementController (spaceShip);
		movController.MovementSpeed = movementSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.defineWorldBounds (Camera.main);
	}
			
	// Update is called once per frame
	void Update () {
		movController.arrowControl ();
		KeyPressed ();
	}	
	private void KeyPressed(){
		if(Input.GetKeyDown(KeyCode.Space)){
			//SPAWN LASER BEAM & FIRE LASER!
			InvokeRepeating("fireLaser",0.0001f,repeatRate);
			//spawnLaser(transform);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("fireLaser");
		}
	}
	public void fireLaser(){
		spawnLaser(transform);
		AudioSource.PlayClipAtPoint (laserAudioClip, transform.position);
	}

	/*
	 * THIS METHOD SPAWNS LASER BEAM
	 */
	private void spawnLaser(Transform transParent){
		Vector3 position = transParent.transform.position;
		position.z -= 5;
		laserPrefab = Instantiate (Resources.Load ("Player/Prefab/laserBeam"), position, Quaternion.identity) as GameObject;
		LaserController laser = laserPrefab.GetComponent<LaserController> ();
		laser.LaserOrign = "PLAYER_LASER";
	}

	//CATCHES THE EVENT WHEN A LASER HITS THE PLAYER SPACEHIP
	void OnTriggerEnter2D(Collider2D collider){
		LaserController laser = collider.gameObject.GetComponent<LaserController> ();
		//CHECKS THAT THE COLLIDER OBJECT IS A LASER
		if (laser){
			if(laser.LaserOrign.Equals("ENEMY_LASER")){
				Debug.Log("OH NO PLATER GOT HIT");
				Debug.Log("PLAYER HEALTH: "+this.health);

				//THIS DESTROYS THE LASER THAT JUST HITTED THE SPACE SHIP
				laser.hit();
				//REDUCES THE HEALTH OF THE SPACESHIP
				health -= laser.getLaserDamage();
				if(health <= 0){
					//IF THE HEALTH IS 0 OR LESS THAN CERO THEN DESTROY THE ENEMYSPACESHIP OBJECT
					Destroy (this.gameObject);
				}
			}
		}	
	}
	
}
