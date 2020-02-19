using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : InteractObject
{

	public InteractObject obj;

	public SpriteRenderer sprRenderer;
	public GameObject select;

	public Sprite[] sprGrass;
	public Sprite[] sprFlower;

	private int waters = 0;

	private int index = -1;

	// Start is called before the first frame update
	void Start() {
		sprRenderer = GetComponent<SpriteRenderer>();

		RandomGrass();
	}

	private void RandomGrass() {
		sprRenderer.sprite = sprGrass[Random.Range(0, sprGrass.Length - 1)];
	}

	public override void DoAction() {
		if (Player.instance.slot.GetChild(0).tag == "Fork" && index == -1) {
			index++;
			sprRenderer.sprite = sprFlower[index];
			Debug.Log("Arar");
		}else if (Player.instance.slot.GetChild(0).tag == "Seed" && index == 0) {
			index++;
			Destroy(Player.instance.slot.GetChild(0).gameObject);
			sprRenderer.sprite = sprFlower[index];
			Debug.Log("Plantar");
		} else if (Player.instance.slot.GetChild(0).tag == "Bottle" && index == 1) {
			if (Player.instance.slot.GetChild(0).gameObject.GetComponent<bottle>().GetWaters() > 0) {
				waters++;
				Player.instance.slot.GetChild(0).gameObject.GetComponent<bottle>().SpendWater();
				index++;
				sprRenderer.sprite = sprFlower[index];
				Debug.Log("Aguar");
			}
		} else if (Player.instance.slot.GetChild(0).tag == "Bottle" && index == 2) {
			waters++;
			Player.instance.slot.GetChild(0).gameObject.GetComponent<bottle>().SpendWater();
			if (waters == 3) {
				index++;
				sprRenderer.sprite = sprFlower[index];
			}
			Debug.Log("Aguar");
		}
	}

	public override void EnterPlayer(GameObject refPlayer) {
		GroundManager.instance.SetGround(this);
	}

	public override void ExitPlayer(GameObject refPlayer) {
		if (GroundManager.instance.refGround == this.gameObject) {
			GroundManager.instance.DropGround();
		}
	}
}
