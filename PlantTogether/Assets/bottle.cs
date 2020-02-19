using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottle : InteractObject
{
	int maxWaters = 3;
	int waters = 0;

	public override void DoAction() {
		throw new System.NotImplementedException();
	}

	public int GetWaters() {
		return waters;
	}

	public void SpendWater() {
		if (waters > 0) {
			waters--;
			Debug.Log("Diminuiu para " + waters);
		}
	}

	public void Fill() {
		if (waters < maxWaters) {
			waters++;
			Debug.Log("Aumentou para " + waters);
		}
	}
}
