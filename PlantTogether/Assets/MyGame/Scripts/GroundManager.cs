using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{

	public Ground refGround;
	public Ground refLastGround;

	public static GroundManager instance;

	private void Start() {
		if (FindObjectsOfType<GroundManager>().Length > 1) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
	}

	public void SetObject(GameObject objSlot) {
		if (!refGround.obj) {
			objSlot.transform.parent = transform.parent;
			objSlot.transform.position = refGround.transform.position;
			refGround.obj = objSlot.GetComponent<InteractObject>();
			objSlot.GetComponent<InteractObject>().ReturnSortingOrder();
		}
	}

	public void SetGround(Ground newRefGround) {
		if (refGround)
			refGround.select.SetActive(false);

		refLastGround = refGround;
		refGround = newRefGround;
		refGround.select.SetActive(true);
	}

	public void DropGround() {
		refGround.select.SetActive(false);
		refGround = refLastGround;
		refGround.select.SetActive(true);
	}

	public void Update() {
		if (Input.GetKeyDown(KeyCode.Z) && refGround && refGround.obj) {
			ProcessObjectInGround();
		} else if (Input.GetKeyDown(KeyCode.Z) && refGround && Player.instance.slot.childCount == 1) {
			ProcessObjectInSlot();
		}
	}

	private void ProcessObjectInGround() {
		if (refGround.obj.tag == "SeedBox") {
			refGround.obj.DoAction();
		} else if  (refGround.obj.tag == "Fountain") {
			refGround.obj.DoAction();
		} else if (Player.instance.slot.childCount == 0) {
			refGround.obj.transform.parent = Player.instance.slot;
			refGround.obj.transform.position = Player.instance.slot.position;
			refGround.obj.GetComponent<SpriteRenderer>().sortingOrder = 2;
			refGround.obj = null;
		}
	}

	private void ProcessObjectInSlot() {
			refGround.DoAction();
	}
}
