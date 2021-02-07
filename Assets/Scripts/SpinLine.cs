using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
	public class SpinLine : MonoBehaviour
	{
		public GameObject[] cells;
		public SlotTypeManager slotTypesManager;
		public float speed = 0.1f;
		public bool spinStart, spinHolders = true;
		public float nextY, y;
		public float spacing = 1.7f;
		public float shiftY = 2.7f;
		private readonly List<SlotCell> slots = new List<SlotCell>();
		[HideInInspector] public SlotCell centerSlot;

		void Awake()
		{
			foreach (GameObject cell in cells) {
				SlotCell slotCell = cell.GetComponent<SlotCell>();
				slotCell.SetType(slotTypesManager.GetRandomSymbol());
				slots.Add(slotCell);
			}

			centerSlot = slots[1];
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
			endHolder.SetType(slotTypesManager.GetRandomSymbol());
		}

		public void RefreshHolderPositions() {
			for (int i = 0; i < slots.Count; i++) {
				slots[i].SetOffset(i * -spacing - y % spacing + shiftY);
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
	}
}
