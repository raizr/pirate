using UnityEngine;

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

	private void Awake() { gameObject.SetActive(false); }
}
