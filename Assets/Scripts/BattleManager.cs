using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
	public bool isSpineStart = false;
	public SpinLine[] spinLines;
	public Button spineButton;
	public TextMeshProUGUI spinCounter;
	public Slider spinCounterSlider;
	public int availableSpins;
	public float delaySpins = 0.3f;
	public float stopDelaySpins = 0.3f;
	public float stopSpinTime = 3.0f;
	private int spinCount;
	private int currentLineIndex = 0;

	private void Awake() {
		Button btn = spineButton.GetComponent<Button>();
		btn.onClick.AddListener(spinOnClick);
		spinCount = availableSpins;
		spinCounterSlider.minValue = 0;
		spinCounterSlider.maxValue = availableSpins;
		spinCounterSlider.value = spinCount;
	}
	void spinOnClick()
	{
		if(spinCount > 0 && !isSpineStart) {
				isSpineStart = true;
				spinCount--;
				spinCounterSlider.value--;
				StopCoroutine("StopSpinCoroutine");
				StartCoroutine("setSpinLines");
		}
	}

	IEnumerator setSpinLines() {
		// Move the first cube up or down.
		currentLineIndex = 0;
		yield return new WaitForSeconds(delaySpins);
		spinLines[0].StartSpine();

		yield return new WaitForSeconds(delaySpins);
		spinLines[1].StartSpine();

		yield return new WaitForSeconds(delaySpins);
		spinLines[2].StartSpine();

		yield return new WaitForSeconds(stopSpinTime);
		StopCoroutine("setSpinLines");
		StartCoroutine("StopSpinCoroutine");
	}

	public void StopLines() {
		spinLines[currentLineIndex].Stop();
		currentLineIndex++;
		if (currentLineIndex >= spinLines.Length) {
			isSpineStart = false;
		}
	}

	private IEnumerator StopSpinCoroutine() {
		while (currentLineIndex < spinLines.Length) {
			StopLines();
			yield return new WaitForSeconds(stopDelaySpins);
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnGUI() {
		spinCounter.text = spinCount + "/" + availableSpins;
	}
}
