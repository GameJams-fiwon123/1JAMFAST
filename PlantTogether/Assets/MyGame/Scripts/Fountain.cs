using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : InteractObject
{


	public override void DoAction(Player player) {
		if (player.slot.childCount == 1) {
			if (player.slot.GetChild(0).tag == "Bottle") {
				player.slot.GetChild(0).gameObject.GetComponent<Bottle>().Fill();
			}
		}
	}

	public override void EnterPlayer(GameObject refPlayer) {
		if (!refPlayer.GetComponent<Player>().photonView.IsMine) {
			return;
		}

		GroundManager.instance.SetGround(ground);
	}

	public override void ExitPlayer(GameObject refPlayer) {

		if (!refPlayer.GetComponent<Player>().photonView.IsMine) {
			return;
		}

		if (GroundManager.instance.refGround == ground) {
			GroundManager.instance.DropGround();
		}
	}
}
