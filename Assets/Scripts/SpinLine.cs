using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinLine : MonoBehaviour
{
	public GameObject[] cells;
	public float speed = 10.0f;
	public float randomMinCoeffSpeed = 1.0f;
	public float randomMaxCoeffSpeed = 2.0f;
	public bool spinStart = false;
	private List<SlotCell> slots = new List<SlotCell>();
	// Start is called before the first frame update
	void Start()
	{
		var randomSpeed = Random.Range(randomMinCoeffSpeed, randomMaxCoeffSpeed);
		foreach (GameObject cell in cells) {
			SlotCell slotCell = cell.GetComponent<SlotCell>();
			slotCell.randomSpeedCoeff = randomSpeed;
			slotCell.scrollSpeed = speed;
			slots.Add(slotCell);
		}
	}

	// Update is called once per frame
	void Update()
	{
		foreach (SlotCell slot in slots) {
			if(spinStart) {
				slot.StartSpin();
			}
			else {
				slot.StopSpin();
			}
		}
	}
}
