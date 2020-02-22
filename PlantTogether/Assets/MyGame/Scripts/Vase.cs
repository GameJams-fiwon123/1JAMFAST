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

	public override void DoAction() {
		if (sprRenderer.sprite == vase && Player.instance.slot.GetChild(0).tag == "Shovel" && Player.instance.slot.GetChild(0).gameObject.GetComponent<Shovel>().HasFlower()) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Soltar Item");
			Player.instance.slot.GetChild(0).gameObject.GetComponent<Shovel>().DoAction();
			sprRenderer.sprite = flower;
		} else {
			sprRenderer.sprite = vase;
		}
	}

	public bool HasFlower() {
		return sprRenderer.sprite == flower;
	}
}
