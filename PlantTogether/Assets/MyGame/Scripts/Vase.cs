using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : InteractObject{

	public Sprite vase;
	public Sprite flower;

	SpriteRenderer sprRenderer;

	private void Start() {
		sprRenderer = GetComponent<SpriteRenderer>();
	}

	public override void DoAction(Player player) {
		if (sprRenderer.sprite == vase && player.slot.GetChild(0).tag == "Shovel" && player.slot.GetChild(0).gameObject.GetComponent<Shovel>().HasFlower()) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Soltar Item");
			player.slot.GetChild(0).gameObject.GetComponent<Shovel>().DoAction(player);
			player.slot.GetChild(0).GetComponent<Shovel>().RemoveFlower();
			sprRenderer.sprite = flower;
		} 
	}

	public bool HasFlower() {
		return sprRenderer.sprite == flower;
	}
}
