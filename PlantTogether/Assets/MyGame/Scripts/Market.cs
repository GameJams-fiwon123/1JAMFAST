using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : InteractObject
{

	public override void DoAction(Player player) {
		if (player.slot.childCount == 1) {
			if (player.slot.GetChild(0).tag == "Vase" && player.slot.GetChild(0).gameObject.GetComponent<Vase>().HasFlower()) {
				FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Entrega");
				GameManager.instance.musicEvent.SetParameter("Flor", 0);
				ItemsManager.instance.interactObjects.Remove(player.slot.GetChild(0).GetComponent<InteractObject>());
				Destroy(player.slot.GetChild(0).gameObject);
				GameManager.instance.SendFlower();
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
