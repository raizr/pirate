using UnityEngine;
using System.Collections;
using DG.Tweening;
public class SlotCell : MonoBehaviour
{

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
