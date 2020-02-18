using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{

	public GameObject refGround;

	public static GroundManager instance;

	private void Start() {
		if (FindObjectsOfType<GroundManager>().Length > 1) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
	}
}
