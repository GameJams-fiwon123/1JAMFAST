using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : InteractObject
{

	public Ground ground;

	public override void DoAction() {
		if (Player.instance.slot.childCount == 1) {
			if (Player.instance.slot.GetChild(0).tag == "Bottle") {
				Player.instance.slot.GetChild(0).gameObject.GetComponent<Bottle>().Fill();
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
