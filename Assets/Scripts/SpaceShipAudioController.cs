using UnityEngine;
using System.Collections;

public class SpaceShipAudioController : MonoBehaviour
{

	private Hashtable soundMap; 

	// Use this for initialization
	void Start (){
		//initialize the soundMap
		//since hash table is not generic the parameters will be as following:
		// key - Action bounded to the Audio clip
		// value - the Audio Clip that needs to be played
		soundMap = new Hashtable();
	}
	
	public void playAudioClip(string actionKey){
	}

	public void addAudioClip(string actionKey, AudioClip audioClip){
	}

	public void removeAudioClip(string actionKey){
	}
}

