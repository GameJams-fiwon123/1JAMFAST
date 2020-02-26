using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	[Header("Variaveis")]
	public float speed = 200;
	public Transform slot;

	Rigidbody2D rb2D;
	SpriteRenderer sprRenderer;
	Animator anim;
	Vector2 dir = Vector2.zero;

	Vector3 scaleChange = Vector3.zero;

	public PhotonView photonView;

	public GameObject nameCanvas;

	public static Player instance;

	private void Awake() {
		photonView = GetComponent<PhotonView>();
		if (!photonView.IsMine) {
			return;
		}

		if (FindObjectsOfType<Player>().Length > 1) {
			Destroy(gameObject); ;
		} else {
			instance = this;
		}
	}

	// Start is called before the first frame update
	void Start() {
		rb2D = GetComponent<Rigidbody2D>();
		sprRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {

		if (!photonView.IsMine) {
			return;
		}

		if (!GameManager.instance.isFinish) {
			TakeOff();
			Movement();
		}

	}

	private void TakeOff() {
		if (Input.GetKey(KeyCode.X) && slot.childCount == 1) {
			GroundManager.instance.SetObject(slot.GetChild(0).gameObject);
		}
	}

	public void Take(string id, string name) {
		photonView.RPC("TakeNetwork", RpcTarget.All, id, name);
	}

	[PunRPC]
	private void TakeNetwork(string type, string name) {
		foreach (InteractObject item in ItemsManager.instance.interactObjects) {
			if (item.tag == type && item.name == name) {

				FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Pegar Item");

				if (item.tag == "Shovel") {
					GameManager.instance.musicEvent.SetParameter("Pá", 1);
				} else if (item.tag == "Seed") {
					GameManager.instance.musicEvent.SetParameter("Semente", 1);
				} else if (item.tag == "Bottle") {
					GameManager.instance.musicEvent.SetParameter("Balde", 1);
				} else if (item.tag == "Fork") {
					GameManager.instance.musicEvent.SetParameter("Rastelo", 1);
				}

				item.transform.parent = slot;
				item.transform.position = slot.position;
				item.GetComponent<SpriteRenderer>().sortingOrder = 2;
				item.ground.obj = null;
				item.ground = null;
				break;
			}
		}
	}

	public void DoActionObject(string type, string name) {
		photonView.RPC("DoActionObjectNetwork", RpcTarget.All, type, name);
	}

	[PunRPC]
	private void DoActionObjectNetwork(string type, string name) {
		foreach (InteractObject obj in ItemsManager.instance.interactObjects) {
			if (obj.tag == type && obj.name == name) {

				obj.DoAction(this);

				break;
			}
		}
	}

	void FixedUpdate() {
		rb2D.velocity = dir.normalized * speed * Time.fixedDeltaTime;
	}

	private void Movement() {
		dir = Vector2.zero;

		if (Input.GetKey(KeyCode.RightArrow)) {
			dir.x += 1;
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			dir.x -= 1;
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			dir.y += 1;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			dir.y -= 1;
		}


		photonView.RPC("SyncAnimation", RpcTarget.All, dir);
	}

	[PunRPC]
	public void SyncAnimation(Vector2 dir) {

		if (dir == Vector2.zero) {
			anim.SetBool("Walk", false);
			anim.SetBool("Idle", true);
		} else {
			anim.SetBool("Idle", false);
			anim.SetBool("Walk", true);
		}

		if (dir.x == -1) {
			scaleChange = new Vector3(-1, 1, 1);
			transform.localScale = scaleChange;
			nameCanvas.transform.localScale = scaleChange;
		} else if (dir.x == 1) {
			scaleChange = new Vector3(1, 1, 1);
			transform.localScale = scaleChange;
			nameCanvas.transform.localScale = scaleChange;
		}
	}

	public void UseSeed() {
		photonView.RPC("UseSeedNetwork", RpcTarget.All);
	}

	[PunRPC]
	private void UseSeedNetwork() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Soltar Item");
		ItemsManager.instance.interactObjects.Remove(slot.GetChild(0).GetComponent<InteractObject>());
		Destroy(slot.GetChild(0).gameObject);
	}

	public void GetPlant() {
		photonView.RPC("GetPlantNetwork", RpcTarget.All);
	}

	[PunRPC]
	private void GetPlantNetwork() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Colhendo");
		slot.GetChild(0).GetComponent<Shovel>().DoAction(this);
	}
}
