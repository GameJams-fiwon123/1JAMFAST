using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
	public List<InteractObject> interactObjects;

	public static ItemsManager instance;

	private void Start() {
		if (FindObjectsOfType<ItemsManager>().Length > 1) {
			Destroy(gameObject);
		}else {
			instance = this;
		}
	}
}
