using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpinLine : MonoBehaviour
{
	public GameObject[] cells;
	public float speed = 0.1f;
	public bool spinStart = false, spinHolders = true;
	public float nextY = 0.0f, y = 0.0f;
	public float spacing = 1.7f;
	public float shiftY = 2.7f;

private List<SlotCell> slots = new List<SlotCell>();

    void Start()
	{
		foreach (GameObject cell in cells) {
			SlotCell slotCell = cell.GetComponent<SlotCell>();
			slots.Add(slotCell);
		}
	}

	public void StartSpine() {
		spinStart = spinHolders = true;

	}

	void Update()
	{
		if (!spinStart) {
			return;
		}
		if(spinHolders) {
			y += speed * Time.deltaTime;
        }
		
		while (y >= nextY + spacing) {
			AdvanceReel();
		}
		
		RefreshHolderPositions();
	}
	internal void AdvanceReel() {
		nextY += spacing;
		var endHolder = slots[slots.Count - 1];
		slots.RemoveAt(slots.Count - 1);
		slots.Insert(0, endHolder);
		endHolder.transform.SetAsLastSibling();
		endHolder.setRandomType();
	}

	public void RefreshHolderPositions() {
		for (int i = 0; i < slots.Count; i++) {
			slots[i].SetUvOffset(i * -spacing - y % spacing + shiftY);
		}
	}

	internal void Stop() {
		y = nextY = 0;
		spinHolders = false;
		int distance = 3;
		float diff = y % spacing;
		if (diff > spacing / 2) {
			distance++;
		}
		float targetY = y - diff + spacing * distance;
		DOTween.To(() => y, x => y = x, targetY, 1.0f)
			.SetEase(Ease.OutBack)
			.OnComplete(OnAllHoldersStopped);
	}
	internal void OnAllHoldersStopped() {
		Update();
		spinStart = false;
	}

	public int GetStopDistance() {
		return Mathf.Clamp(2, 0, 3);
	}
}
