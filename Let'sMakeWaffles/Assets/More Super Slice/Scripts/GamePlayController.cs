using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour {

    public GameObject[] answers;
    public Transform[] answerTransforms;
    public bool[] randomCheck;
    [HideInInspector]//Hiding it for now Because currently we are using GameManager to activate Next panels.
	public GameObject completionScreen;
	public int nextLevelIndex;
	public float endDelay;
	public int soundIndex;
	public InfoManager infoMngr;

	// Use this for initialization
	void Start () {
		infoMngr.SetSoundIndex (soundIndex); 
		GameManager.Instance.ClickOn(2);
		RandomizeAnswers ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void RandomizeAnswers()
	{
		int random;
		for (int i = 0; i < 3 ;) {
			random = Random.Range (0, 3);
			if (CheckRandom (random)) {
				answers [i].gameObject.transform.localPosition = answerTransforms [random].gameObject.transform.localPosition;
				answers [i].transform.SetSiblingIndex (random);
				//Debug.Log ("Random " + random +" on "+i);
				randomCheck [random] = true;
				i++;
			}
		}
	}
	bool CheckRandom(int index)
	{
		if (randomCheck [index]) {
			return false;
		} 
		else
			return true;
	}
    public void ButtonClick(int ind)
    {
		if (GameManager.Instance.CanClick ()) {
			if (ind == 2) {
				GameManager.Instance.ClickOff ();
				Debug.Log ("this is true");	
				GameManager.Instance.ClickOff ();
				SoundManager.instance.PlaySound (15);
				EventController.instance.correctOptionSelectionCounter++;
				for (int i = 0; i < 3; i++) {
					if (answers [i].name == "CorrectAnswer") {
						if (GameManager.Instance.Accessibilty)
							CloseCaption.CCManager.instance.CreateCaption (0, 1);
						answers [i].GetComponent<Image> ().enabled = true;
						break;
					}
				}
				Invoke ("ZapAnimation", 0.5f);
			} else {
				EventController.instance.wrongOptionSelectionCounter++;
				Debug.Log ("this is wrong");
				if (GameManager.Instance.Accessibilty)
					CloseCaption.CCManager.instance.CreateCaption (1, 1);
				SoundManager.instance.PlaySound (16);
			}
		}
	}
	void ZapAnimation()
	{
		if(GameManager.Instance.Accessibilty)
			CloseCaption.CCManager.instance.CreateCaption (2,1);
		SoundManager.instance.PlaySound (17);
		Invoke ("ActiveCompletionScreen", 3.5f);
	}
	void ActiveCompletionScreen()
	{
		if(GameManager.Instance.Accessibilty)
			CloseCaption.CCManager.instance.CreateCaption (3+nextLevelIndex,SoundManager.instance.sounds[nextLevelIndex].length);
		SoundManager.instance.PlaySound (nextLevelIndex);
		//completionScreen.SetActive (true);Commenting Cuz its already being done through Coroutine
		if(EventController.instance != null && !External.Instance.Preview)
			EventController.instance.SetGamePercentage(++EventController.instance.levelCounter);
		StartCoroutine(GameManager.Instance.StartNewLevel (nextLevelIndex, endDelay));
	}

}
