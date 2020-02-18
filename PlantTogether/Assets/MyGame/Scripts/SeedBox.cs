using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBox : DetectObject
{


	public override void DoAction(GameObject refPlayer) {
		refPlayer.GetComponent<Player>().slot.sprite = sprObj;
	}

}
