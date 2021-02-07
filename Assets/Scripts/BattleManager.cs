using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class BattleManager : MonoBehaviour
	{
		public enum State {
			NotStarted,
			Idle,
			SpinStarting,
			Spinning,
			SpinStopping,
			Result,
		}

		public SlotTypeManager typeManager;
		[HideInInspector] public bool isSpineStart;
		public SpinLine[] spinLines;
		public Button spineButton;
		public TextMeshProUGUI spinCounter;
		public Slider spinCounterSlider;
		public FadeLayer fadeLayer;
		public int availableSpins;
		public float delaySpins = 0.3f;
		public float stopDelaySpins = 0.3f;
		public float stopSpinTime = 3.0f;
		private int spinCount;
		private int currentLineIndex;
		private State spinState = State.NotStarted;

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
				StopCoroutine("StopSpinCoroutine");
				StartCoroutine("SetSpinLines");
			}
		}

		IEnumerator SetSpinLines() {
			if (isSpineStart || spinCount == 0) {
				yield return null;
			}
			isSpineStart = true;
			spinState = State.SpinStarting;
			spinCount--;
			spinCounterSlider.value--;
			// Move the first cube up or down.
			currentLineIndex = 0;
			yield return new WaitForSeconds(delaySpins);
			spinLines[0].StartSpine();

			yield return new WaitForSeconds(delaySpins);
			spinLines[1].StartSpine();

			yield return new WaitForSeconds(delaySpins);
			spinLines[2].StartSpine();

			yield return new WaitForSeconds(stopSpinTime);
			StopCoroutine("SetSpinLines");
			StartCoroutine("StopSpinCoroutine");
			spinState = State.Spinning;
		}

		public void StopLines() {
			spinLines[currentLineIndex].Stop();
			currentLineIndex++;
			if (currentLineIndex >= spinLines.Length) {
				spinState = State.SpinStopping;
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
			switch (spinState)
			{
				case State.SpinStopping:
					for (int i = 0; i < spinLines.Length; i++) {
						if (spinLines[i].spinStart) {
							return;
						}
					}

					spinState = State.Result;
					break;
				case State.Result:
					ApplyResult();
					break;
			}
		}

		void ApplyResult () {
			var result = typeManager.CheckResult(spinLines[0].centerSlot.type,
				spinLines[1].centerSlot.type,
				spinLines[2].centerSlot.type);

			spinState = State.Idle;
			isSpineStart = false;
			if (result.type == ResultType.NONE) {
				spinOnClick();
			}

			else {
				fadeLayer.FadeOnEffect(result.effect, spinOnClick);
			}
			Debug.Log("type " +result.type + " damage " + result.damage + " money " + result.money);
		}


		void OnGUI() {
			spinCounter.text = spinCount + "/" + availableSpins;
		}
	}
}
