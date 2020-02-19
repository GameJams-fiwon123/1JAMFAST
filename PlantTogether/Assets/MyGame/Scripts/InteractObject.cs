using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
	GameObject refPlayer;
	bool isPlayerEnter = false ;

	int orderIndex;

	private void Awake() {
		orderIndex = GetComponent<SpriteRenderer>().sortingOrder;
	}

	public void ReturnSortingOrder() {
		GetComponent<SpriteRenderer>().sortingOrder = orderIndex;
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

	public abstract void DoAction();
	public virtual void EnterPlayer(GameObject refPlayer){
	}
	public virtual void ExitPlayer(GameObject refPlayer) {

	}
}
