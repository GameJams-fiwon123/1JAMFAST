using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBox : InteractObject
{
	public GameObject seedPrefab;
	private int seedIndex = 1;

	public override void DoAction(Player player) {
		if (player.slot.childCount == 0) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Pegar Item");
			GameObject obj = Instantiate(seedPrefab, player.slot);
			obj.name = "Seed" + seedIndex;
			seedIndex++;
			ItemsManager.instance.interactObjects.Add(obj.GetComponent<InteractObject>());
			obj.GetComponent<SpriteRenderer>().sortingOrder = 2;
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
