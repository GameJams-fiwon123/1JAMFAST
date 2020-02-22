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

	public override void DoAction() {
		if (sprRenderer.sprite == shovel) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Colhendo");
			sprRenderer.sprite = flower;
		} else {
			sprRenderer.sprite = shovel;
		}
	}

	public bool HasFlower() {
		return sprRenderer.sprite == flower;
	}

}
