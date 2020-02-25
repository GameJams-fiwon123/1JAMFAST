using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameMultiplayer : MonoBehaviour
{

	PhotonView photonView;

	public Text playerNameText;

	// Start is called before the first frame update
	void Start() {

		photonView = GetComponent<PhotonView>();

		playerNameText.text = photonView.Owner.NickName;
	}

}
