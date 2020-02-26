using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : InteractObject
{
	public Sprite shovel;
	public Sprite flower;

	SpriteRenderer sprRenderer;

	private void Start() {
		sprRenderer = GetComponent<SpriteRenderer>();
	}

	public override void DoAction(Player player) {
		if (sprRenderer.sprite == shovel) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Colhendo");
			sprRenderer.sprite = flower;
		} 
	}

	public void RemoveFlower() {
		sprRenderer.sprite = shovel;
	}

	public bool HasFlower() {
		return sprRenderer.sprite == flower;
	}

}
