using UnityEngine;
using System.Collections;
using DG.Tweening;
public class SlotCell : MonoBehaviour
{
	public enum CellSymbol
	{
		EMPTY = 0,
		COIN= 1,
		FIRE = 2,
		LIGHT = 3,
		POISEN = 4
	}

	public Sprite[] spritePool;
	public CellSymbol type = CellSymbol.EMPTY;
	void Awake()
	{
		setRandomType();
	}

	public void SetUvOffset(float offsetY) {
		this.transform.position = new Vector3(transform.position.x, offsetY, transform.position.z);
	}

	public void setRandomType() {
		var values = CellSymbol.GetValues(typeof(CellSymbol));
		type = (CellSymbol)values.GetValue(Random.Range(0, values.Length));
		setTextureByType(type);
	}

	void setTextureByType(CellSymbol type) {
		GetComponent<SpriteRenderer>().sprite = spritePool[(int)type];
	}
}
