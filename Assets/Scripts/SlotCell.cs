using UnityEngine;
using System.Collections;
using DG.Tweening;
public class SlotCell : MonoBehaviour
{
	public enum CellSymbol
	{
		LIFE = 0,
		COIN= 1,
		DAMAGE = 2,
		DOUBLE = 3
	}
	public Sprite[] spritePool;
	public static float STOP_IMPEDENCE_DELAY_TIME = 0.3f;
	public int sortingLayer;
	public float bottomBorder = -3.6f;
	public float topPosY = 3.6f;
	public float scrollSpeed = 10f;
	public bool isSpin = false;
	public int symbol;
	public GameObject icon;
	public float randomSpeedCoeff = 1.0f;
	public CellSymbol type = CellSymbol.LIFE;
	void Awake()
	{
		setRandomType();
	}

	void Update()
	{
		if (isSpin) {
			Spin();
		}
		else {
			StopSpin();
		}
	}

	void Spin()
	{
		float offsetY = transform.position.y;
		offsetY -= Time.deltaTime * scrollSpeed * randomSpeedCoeff;
		if (offsetY < bottomBorder) {
			SetUvOffset(topPosY);
			setRandomType();
			return;
		}
		SetUvOffset(offsetY);
	}

	public void StartSpin()
	{
		if(isSpin) {
			return;
		}
		isSpin = true;
	}

	public void StopSpin()
	{
		if (isSpin == true) {
			float endY = (float)Mathf.Ceil(transform.position.y * 10) / 10;
			symbol = GetSymbolFromUvOffset(endY);
			DOTween.To(() => transform.position.y, y => SetUvOffset(y), endY, STOP_IMPEDENCE_DELAY_TIME)
				.OnComplete(() => { isSpin = false; });
		}
	}

	public void SetUvOffset(float offsetY) {
		this.transform.position = new Vector3(transform.position.x, offsetY, transform.position.z);
	}

	public void SetSymbol(int symbol)
	{
		this.symbol = symbol;
		SetUvOffset(GetUvOffsetFromSymbol(symbol));
	}

	float GetUvOffsetFromSymbol(int symbol)
	{
		return (float) symbol / 10;
	}

	int GetSymbolFromUvOffset(float uvOffset)
	{
		return Mathf.FloorToInt(uvOffset * 10);
	}


	void setRandomType() {
		var values = CellSymbol.GetValues(typeof(CellSymbol));
		type = (CellSymbol)values.GetValue(Random.Range(0, values.Length));
		setTextureByType(type);
	}

	void setTextureByType(CellSymbol type) {
		switch (type) {
			case CellSymbol.LIFE:
				icon.GetComponent<SpriteRenderer>().sprite = spritePool[0];
				break;
			case CellSymbol.DAMAGE:
				icon.GetComponent<SpriteRenderer>().sprite = spritePool[1];
				break;
			default:
				icon.GetComponent<SpriteRenderer>().sprite = spritePool[2];
				break;
		}
	}
}
