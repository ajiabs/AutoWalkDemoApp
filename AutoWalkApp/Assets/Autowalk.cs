// This script moves your player automatically in the direction he is looking at. You can 
// activate the autowalk function by pull the cardboard trigger, by define a threshold angle 
// or combine both by selecting both of these options.
// The threshold is an value in degree between 0° and 90°. So for example the threshold is 
// 30°, the player will move when he is looking 31° down to the bottom and he will not move 
// when the player is looking 29° down to the bottom. This script can easally be configured
// in the Unity Inspector.Attach this Script to your CardboardMain-GameObject. If you 
// haven't the Cardboard Unity SDK, download it from https://developers.google.com/cardboard/unity/download

using UnityEngine;
using System.Collections;

public class Autowalk : MonoBehaviour {

	private CardboardHead head;
	private Vector3 startingPosition;
	private float delay = 0.0f; 

	void Start() {
		head = Camera.main.GetComponent<StereoController>().Head;
		startingPosition = transform.localPosition;
	}

	void Update() {
		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
		// if looking at object for 2 seconds, enable/disable autowalk
		if (isLookedAt && Time.time>delay) { 
			GameObject FPSController = GameObject.Find("Head");
			FPSInputController autowalk = FPSController.GetComponent<FPSInputController>();
			autowalk.checkAutoWalk = !autowalk.checkAutoWalk;
			delay = Time.time + 2.0f;
		}
		// currently looking at object
		else if (isLookedAt) { 
			GetComponent<Renderer>().material.color = Color.yellow; 
			delay = Time.time + 2.0f;
		} 
		// not looking at object
		else if (!isLookedAt) { 
			GetComponent<Renderer>().material.color = Color.red; 
			delay = Time.time + 2.0f; 
		}
	}

}