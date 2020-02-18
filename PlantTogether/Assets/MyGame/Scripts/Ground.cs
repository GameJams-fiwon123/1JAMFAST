using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : DetectObject
{

	public SpriteRenderer sprRenderer;

	public Sprite[] sprGrass;
	public Sprite[] sprFlower;

	// Start is called before the first frame update
	void Start() {
		sprRenderer = GetComponent<SpriteRenderer>();

		RandomGrass();
	}

	private void RandomGrass() {
		sprRenderer.sprite = sprGrass[Random.Range(0, sprGrass.Length - 1)];
	}

	public override void DoAction(GameObject refPlayer) {

		if (GroundManager.instance.refGround == this.gameObject) {

			sprRenderer.sprite = sprFlower[0];

			Debug.Log("Plantou");
		}
	}

	public override void EnterPlayer(GameObject refPlayer) {
		GroundManager.instance.refGround = this.gameObject;
	}

	public override void ExitPlayer(GameObject refPlayer) {
		if (GroundManager.instance.refGround == this.gameObject)
			GroundManager.instance.refGround = null;
	}
}
