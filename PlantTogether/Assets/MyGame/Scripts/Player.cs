using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	[Header("Variaveis")]
	public float speed = 100;
	public Transform slot;

	Rigidbody2D rb2D;
	SpriteRenderer sprRenderer;
	Animator anim;
	Vector2 dir = Vector2.zero;

	Vector3 scaleChange = Vector3.zero;

	public static Player instance;

	private void Awake() {
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

		TakeOff();
		Movement();

	}

	private void TakeOff() {
		if (Input.GetKey(KeyCode.X) && slot.childCount == 1) {
			slot.GetChild(0).gameObject.GetComponent<InteractObject>().ReturnSortingOrder();
			GroundManager.instance.SetObject(slot.GetChild(0).gameObject);
		}
	}

	void FixedUpdate() {
		rb2D.velocity = dir * speed * Time.fixedDeltaTime;
	}

	private void Movement() {
		dir = Vector2.zero;

		if (Input.GetKey(KeyCode.RightArrow)) {
			dir.x += 1;
			scaleChange = new Vector3(1, 1, 1);
			transform.localScale = scaleChange;
			anim.Play("Walk");
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			dir.x -= 1;
			scaleChange = new Vector3(-1, 1, 1);
			transform.localScale = scaleChange;
			anim.Play("Walk");
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			dir.y += 1;
			anim.Play("Walk");
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			dir.y -= 1;
			anim.Play("Walk");
		}

		if (dir == Vector2.zero) {
			anim.Play("Idle");
		}
	}
}
