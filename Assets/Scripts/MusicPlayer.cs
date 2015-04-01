using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer musicPlayer;

	void Awake(){
		Debug.Log ("Awake: " + GetInstanceID ());
		if (musicPlayer != null) {
			Destroy (gameObject);
		} else {
			musicPlayer = this;
		}
		GameObject.DontDestroyOnLoad (gameObject);
	}
}