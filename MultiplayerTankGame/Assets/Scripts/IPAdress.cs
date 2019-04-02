using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Net.NetworkInformation;

[RequireComponent(typeof(Text))]
public class IPAdress : MonoBehaviour {

    private Text textField;

	// Use this for initialization
	void Start () {
        textField = GetComponent<Text>();
        textField.text = IPManager.GetIP(ADDRESSFAM.IPv4);
	}

}
