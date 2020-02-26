using Photon.Pun;
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

		
			refGround.SetObject(objSlot.tag, objSlot.name);
			
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
		if (!GameManager.instance.isFinish) {
			if (Input.GetKeyDown(KeyCode.Z) && refGround && refGround.obj) {
				ProcessObjectInGround();
			} else if (Input.GetKeyDown(KeyCode.Z) && refGround && Player.instance.slot.childCount == 1) {
				ProcessObjectInSlot();
			}
		}
	}

	private void ProcessObjectInGround() {
		ProcessObjectInGroundNetwork();
	}

	private void ProcessObjectInGroundNetwork() {
		if (refGround.obj.tag == "SeedBox") {
			Player.instance.DoActionObject(refGround.obj.tag, refGround.obj.name);
		} else if (refGround.obj.tag == "Fountain") {
			Player.instance.DoActionObject(refGround.obj.tag, refGround.obj.name);
		} else if (refGround.obj.tag == "Vase" && Player.instance.slot.childCount == 1 && !refGround.obj.GetComponent<Vase>().HasFlower()) {
			Player.instance.DoActionObject(refGround.obj.tag, refGround.obj.name);
		} else if (refGround.obj.tag == "Market") {
			Player.instance.DoActionObject(refGround.obj.tag, refGround.obj.name);
		} else if (Player.instance.slot.childCount == 0) {
			Player.instance.Take(refGround.obj.tag, refGround.obj.name);
		}
	}

	private void ProcessObjectInSlot() {
			refGround.DoAction(Player.instance);
	}
}
