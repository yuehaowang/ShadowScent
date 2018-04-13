using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPad : MonoBehaviour {
	[SerializeField] private GameObject cover;
	[SerializeField] private GameObject pad;
	private float fps = 40.0f;
	private float currentOperationLeftTime = 0.0f;
	private float EnterDuration = 0.8f;
	private float LeaveDuration = 0.6f;
	private float SafeDuration = 1.4f;
	private bool isInRegMode = false;

	// Use this for initialization
	void Start () {
		//EnterRegisterMode ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentOperationLeftTime != 0.0f) {
			currentOperationLeftTime = Mathf.Max (currentOperationLeftTime - Time.deltaTime, 0.0f);
		}
		
	}

	public int EnterRegisterMode () {
		if (currentOperationLeftTime != 0.0f)
			return -1;
		if (isInRegMode)
			return -1;

		isInRegMode = true;
		currentOperationLeftTime = SafeDuration;
		FadeOut (0.0f, 0.8f, EnterDuration);
		MoveUpPad ();

		return 0;
	}

	public int LeaveRegisterMode () {
		if (currentOperationLeftTime != 0.0f)
			return -1;
		if (!isInRegMode)
			return -1;
		
		isInRegMode = false;
		currentOperationLeftTime = SafeDuration;
		FadeOut (0.8f, 0.0f, LeaveDuration);
		MoveDownPad ();

		return 0;
	}

	private void FadeOut(float fromAlpha, float toAlpha, float fadeOutTime) {
		StartCoroutine (_FadeOut(fromAlpha, toAlpha, fadeOutTime));
	}

	private void MoveUpPad() {
		StartCoroutine (_MoveUpPad());
	}

	private void MoveDownPad() {
		StartCoroutine (_MoveDownPad());
	}

	private IEnumerator _FadeOut(float fromAlpha, float toAlpha, float fadeOutTime) {
		var objImage = cover.GetComponent<UnityEngine.UI.Image> ();
		// initialize the alpha value to zero
		Color oldcolor = objImage.color;
		Color newcolor = new Color (oldcolor.r, oldcolor.g, oldcolor.b, fromAlpha);
		objImage.color = newcolor;
		if (fromAlpha < toAlpha) {
			while (objImage.color.a < toAlpha) {
				newcolor = new Color (oldcolor.r, oldcolor.g, oldcolor.b, objImage.color.a + ((toAlpha - fromAlpha) / fps / fadeOutTime));
				objImage.color = newcolor;
				yield return new WaitForSeconds (1.0f / fps);
			}
		} else {
			while (objImage.color.a > toAlpha) {
				newcolor = new Color (oldcolor.r, oldcolor.g, oldcolor.b, objImage.color.a + ((toAlpha - fromAlpha) / fps / fadeOutTime));
				objImage.color = newcolor;
				yield return new WaitForSeconds (1.0f / fps);
			}
		}
	}

	private IEnumerator _MoveUpPad() {
		var objPad = pad.GetComponent <UnityEngine.UI.Image> ();
		float toY = -100;
		float fromY = -600;
		float toTime = LeaveDuration;
		Vector3 fromPlace = objPad.rectTransform.localPosition;
		objPad.transform.localPosition = new Vector3(fromPlace.x, fromY, fromPlace.z);
		Vector3 toPlace;
		while (objPad.rectTransform.localPosition.y < toY) {
			toPlace = new Vector3 (fromPlace.x, objPad.rectTransform.localPosition.y + (toY - fromY) / fps / toTime, fromPlace.z);
			objPad.rectTransform.localPosition = toPlace;
			yield return new WaitForSeconds (1.0f / fps);
		}
	}

	private IEnumerator _MoveDownPad() {
		var objPad = pad.GetComponent <UnityEngine.UI.Image> ();
		float toY = -600;
		float fromY = -100;
		float toTime = EnterDuration;
		Vector3 fromPlace = objPad.rectTransform.localPosition;
		objPad.transform.localPosition = new Vector3(fromPlace.x, fromY, fromPlace.z);
		Vector3 toPlace;
		while (objPad.rectTransform.localPosition.y > toY) {
			toPlace = new Vector3 (fromPlace.x, objPad.rectTransform.localPosition.y + (toY - fromY) / fps / toTime, fromPlace.z);
			objPad.rectTransform.localPosition = toPlace;
			yield return new WaitForSeconds (1.0f / fps);
		}
	}
}
