using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DetectObject : MonoBehaviour
{

	public Sprite sprObj;

	GameObject refPlayer;
	bool isPlayerEnter;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Z) && isPlayerEnter) {
			DoAction(refPlayer);
			Debug.Log("Player Interact with " + gameObject.name);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Player") {
			refPlayer = collision.gameObject;
			isPlayerEnter = true;
			EnterPlayer(refPlayer);
			Debug.Log("Player ENTER in RANGE of object: " + gameObject.name);
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Player") {
			ExitPlayer(refPlayer);
			refPlayer = null;
			isPlayerEnter = false;
			Debug.Log("Player EXIT in RANGE of object: " + gameObject.name);
		}
	}

	public abstract void DoAction(GameObject refPlayer);
	public virtual void EnterPlayer(GameObject refPlayer){
	}
	public virtual void ExitPlayer(GameObject refPlayer) {

	}
}
