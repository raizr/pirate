using UnityEngine;

public class SlotTypeManager : MonoBehaviour
{

	[HideInInspector] public SlotType[] types;
	[HideInInspector] public float[] weights;
	private bool isInit = false;

	private void Awake() {
		gameObject.SetActive(false);
		Init();
	}

	public void Init() {
		if (isInit) {
			return;
		}
		isInit = true;
		types = GetComponentsInChildren<SlotType>(true);
		SetWeights();
	}

	private void SetWeights() {
		weights = new float[types.Length];
		for (int i = 0; i < weights.Length; i++) {
			weights[i] = (i == 0 ? 0 : weights[i - 1]) + types[i].frequency;
		}
	}

	public SlotType GetRandomSymbol() {
		Init();
		if (weights.Length > 0) {
			float r = Random.Range(0, weights[weights.Length - 1]);
			for (int i = 0; i < weights.Length; i++) {
				if (r < weights[i])
					return types[i];
			}
		}
		return new SlotType();
	}
}
