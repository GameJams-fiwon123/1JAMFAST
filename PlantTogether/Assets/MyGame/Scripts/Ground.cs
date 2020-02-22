using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : InteractObject
{

	public InteractObject obj;

	public bool isVase;
	public Sprite sprVase;

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
		if (!isVase) {
			sprRenderer.sprite = sprGrass[Random.Range(0, sprGrass.Length - 1)];
			index = -1;
		} else {
			sprRenderer.sprite = sprVase;
		}
	}

	public override void DoAction() {
		if (!isVase) {
			if (Player.instance.slot.GetChild(0).tag == "Fork" && index == -1) {
				FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Arar");
				index++;
				sprRenderer.sprite = sprFlower[index];
				Debug.Log("Arar");
			} else if (Player.instance.slot.GetChild(0).tag == "Seed" && index == 0) {
				FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Soltar Item");
				index++;
				Destroy(Player.instance.slot.GetChild(0).gameObject);
				sprRenderer.sprite = sprFlower[index];
				Debug.Log("Plantar");
			} else if (Player.instance.slot.GetChild(0).tag == "Bottle" && index == 1) {
				if (Player.instance.slot.GetChild(0).gameObject.GetComponent<Bottle>().GetWaters() > 0) {
					FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Regar");
					waters++;
					Player.instance.slot.GetChild(0).gameObject.GetComponent<Bottle>().SpendWater();
					index++;
					sprRenderer.sprite = sprFlower[index];
					Debug.Log("Aguar");
				}
			} else if (Player.instance.slot.GetChild(0).tag == "Bottle" && index == 2) {
				if (Player.instance.slot.GetChild(0).gameObject.GetComponent<Bottle>().GetWaters() > 0) {
					FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Regar");
					waters++;
					Player.instance.slot.GetChild(0).gameObject.GetComponent<Bottle>().SpendWater();
					if (waters == 2) {
						index++;
						sprRenderer.sprite = sprFlower[index];
					}
					Debug.Log("Aguar");
				}
			} else if (Player.instance.slot.GetChild(0).tag == "Shovel" && index == 3) {
				FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Colhendo");
				Player.instance.slot.GetChild(0).GetComponent<Shovel>().DoAction();
				RandomGrass();
			}
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
