using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : InteractObject
{
	public Ground ground;

	public override void DoAction() {
		if (Player.instance.slot.childCount == 1) {
			if (Player.instance.slot.GetChild(0).tag == "Vase" && Player.instance.slot.GetChild(0).gameObject.GetComponent<Vase>().HasFlower()) {
				FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Entrega");
				Destroy(Player.instance.slot.GetChild(0).gameObject);
				GameManager.instance.SendFlower();
			}
		}
	}

	public override void EnterPlayer(GameObject refPlayer) {
		GroundManager.instance.SetGround(ground);
	}

	public override void ExitPlayer(GameObject refPlayer) {
		if (GroundManager.instance.refGround == ground) {
			GroundManager.instance.DropGround();
		}
	}
}
