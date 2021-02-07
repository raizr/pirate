using UnityEngine;

namespace Assets.Scripts
{
	public enum CellSymbol {
		EMPTY = 0,
		COIN = 1,
		FIRE = 2,
		LIGHT = 3,
		POISEN = 4
	}

	public class SlotType : MonoBehaviour
	{

		public CellSymbol type = CellSymbol.EMPTY;
		public Sprite sprite;
		public float frequency = 50;
		public float damage = 1;
		public float multiplierX2 = 1;
		public float multiplierX3 = 1;
		
		public int money = 100;
		public float moneyMultiplierX2 = 50;
		public float moneyMultiplierX3 = 100;
		private void Awake() { gameObject.SetActive(false); }

		public float TakeDamageX2() {
			return damage * multiplierX2;
		}

		public float TakeDamageX3() {
			return damage * multiplierX3;
		}

		public int TakeMoneyX2() {
			return (int) (money * moneyMultiplierX2);
		}
		public int TakeMoneyX3() {
			return (int) (money * moneyMultiplierX3);
		}
	}
}