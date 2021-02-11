using System.Collections.Generic;
using Assets.Scripts.Effects;
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
		private List<BaseEffect> effects = new List<BaseEffect>();

		void Awake() {
			gameObject.SetActive(false);
		}

		public void Init() {
			GetComponents(effects);
		}
		public List<BaseEffect> GetEffects() {
			return effects;
		}
	}
}