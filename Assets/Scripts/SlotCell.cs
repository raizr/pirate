using UnityEngine;

namespace Assets.Scripts
{
	public class SlotCell : MonoBehaviour
	{
		public CellSymbol type = CellSymbol.EMPTY;
		public void SetOffset(float offsetY) {
			this.transform.position = new Vector3(transform.position.x, offsetY, transform.position.z);
		}

		public void SetType(SlotType slotType)
		{
			GetComponent<SpriteRenderer>().sprite = slotType.sprite;
			type = slotType.type;
		}
	}
}
