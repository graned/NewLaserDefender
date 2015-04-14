using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
	//public Vector3 vector3;
	public float radius = 1;

	//THIS METHOD DRAWS A SPHERE IN A POSITION
	void OnDrawGizmos(){
		//vector3 = transform.position;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}
