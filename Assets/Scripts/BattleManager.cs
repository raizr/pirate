using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public bool isSpineStart = false;
    public SpinLine[] spinLines;
    public Button spineButton;
    public TextMeshProUGUI spinCounter;
    public Slider spinCounterSlider;
    public int availableSpins;

    private int spinCount;

    private void Awake() {
        Button btn = spineButton.GetComponent<Button>();
        btn.onClick.AddListener(spinOnClick);
        spinCount = availableSpins;
        spinCounterSlider.minValue = 0;
        spinCounterSlider.maxValue = availableSpins;
        spinCounterSlider.value = spinCount;
    }
	void spinOnClick() {
        if(spinCount > 0) {
            isSpineStart = !isSpineStart;
            if(isSpineStart) {
                spinCount--;
                spinCounterSlider.value--;

            }
        }
    }

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SpinLine line in spinLines) {
            line.spinStart = isSpineStart;
        }

        spinCounter.text = spinCount + "/" + availableSpins;
    }
}
