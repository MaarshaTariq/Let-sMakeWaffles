using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour {

	public GameObject final;
	// Use this for initialization
	void Start () {
		Invoke ("DisableScreen",4.5f);
		SoundManager.instance.PlaySound (19);
		if(GameManager.Instance.Accessibilty)
			CloseCaption.CCManager.instance.CreateCaption (15,SoundManager.instance.sounds[19].length);
	}
	void DisableScreen()
	{
		final.SetActive (true);
		this.gameObject.SetActive (false);
	}
}
