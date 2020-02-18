using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : DetectObject
{


	public override void DoAction(GameObject refPlayer) {
		refPlayer.GetComponent<Player>().slot.sprite = sprObj;
		Destroy(transform.parent.gameObject);
	}

}
