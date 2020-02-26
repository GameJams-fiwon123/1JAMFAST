using Photon.Pun;
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

	private PhotonView photonView;

	// Start is called before the first frame update
	void Start() {
		photonView = GetComponent<PhotonView>();
		sprRenderer = GetComponent<SpriteRenderer>();
		RandomGrass();
	}

	private void RandomGrass() {
		if (!isVase) {
			sprRenderer.sprite = sprGrass[Random.Range(0, sprGrass.Length - 1)];
			index = -1;
			waters = 0;
		} else {
			sprRenderer.sprite = sprVase;
		}
	}

	public void SetObject(string type, string name) {
		photonView.RPC("SetObjectNetwork", RpcTarget.All, type, name);

	}

	[PunRPC]
	private void SetObjectNetwork(string type, string name) {
		foreach (InteractObject item in ItemsManager.instance.interactObjects) {
			if (item.tag == type && item.name == name) {

				if (item.tag == "Shovel") {
					GameManager.instance.musicEvent.SetParameter("Pá", 0);
				} else if (item.tag == "Seed") {
					GameManager.instance.musicEvent.SetParameter("Semente", 0);
				} else if (item.tag == "Bottle") {
					GameManager.instance.musicEvent.SetParameter("Balde", 0);
				} else if (item.tag == "Fork") {
					GameManager.instance.musicEvent.SetParameter("Rastelo", 0);
				}

				FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Soltar Item");

				item.ReturnSortingOrder();

				item.transform.parent = transform.parent;
				item.transform.position = transform.position;
				obj = item;
				item.ground = this;
				item.GetComponent<InteractObject>().ReturnSortingOrder();
				break;
			}
		}
	}

	public override void DoAction(Player player) {
		if (!isVase) {
			if (player.slot.GetChild(0).tag == "Fork" && index == -1) {
				photonView.RPC("GrowthGround", RpcTarget.All);
				Debug.Log("Arar");
			} else if (player.slot.GetChild(0).tag == "Seed" && index == 0) {
				player.UseSeed();
				photonView.RPC("GrowthGround", RpcTarget.All);
				Debug.Log("Plantar");
			} else if (player.slot.GetChild(0).tag == "Bottle" && index == 1) {
				if (player.slot.GetChild(0).gameObject.GetComponent<Bottle>().GetWaters() > 0) {
					player.slot.GetChild(0).gameObject.GetComponent<Bottle>().SpendWater();
					photonView.RPC("WaterGround", RpcTarget.All);
					photonView.RPC("GrowthGround", RpcTarget.All);
					Debug.Log("Aguar");
				}
			} else if (player.slot.GetChild(0).tag == "Bottle" && index == 2) {
				if (player.slot.GetChild(0).gameObject.GetComponent<Bottle>().GetWaters() > 0) {
					photonView.RPC("WaterGround", RpcTarget.All);
					player.slot.GetChild(0).gameObject.GetComponent<Bottle>().SpendWater();
					Debug.Log("Aguar");
				}
			} else if (player.slot.GetChild(0).tag == "Shovel" && index == 3 && !player.slot.GetChild(0).gameObject.GetComponent<Shovel>().HasFlower()) {
				player.GetPlant();
				photonView.RPC("DigPlant", RpcTarget.All);
			}
		}
	}

	[PunRPC]
	private void DigPlant() {
		RandomGrass();
	}

	[PunRPC]
	private void WaterGround() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Regar");
		waters++;

		if (waters == 2) {
			index++;
			sprRenderer.sprite = sprFlower[index];
		}
	}

	[PunRPC]
	private void GrowthGround() {

		if (index == -1) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Arar");
		}

		index++;
		sprRenderer.sprite = sprFlower[index];
	}

	public override void EnterPlayer(GameObject refPlayer) {

		if (!refPlayer.GetComponent<Player>().photonView.IsMine) {
			return;
		}

		GroundManager.instance.SetGround(this);
	}

	public override void ExitPlayer(GameObject refPlayer) {
		if (!refPlayer.GetComponent<Player>().photonView.IsMine) {
			return;
		}

		if (GroundManager.instance.refGround == this.gameObject) {
			GroundManager.instance.DropGround();
		}
	}
}
