using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBox : InteractObject
{
	public Ground ground;
	public GameObject seedPrefab;

	public override void DoAction() {
		if (Player.instance.slot.childCount == 0) {
			GameObject obj = Instantiate(seedPrefab, Player.instance.slot);
			obj.GetComponent<SpriteRenderer>().sortingOrder = 2;
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
