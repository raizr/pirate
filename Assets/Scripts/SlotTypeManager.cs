using System;
using System.Collections.Generic;
using Assets.Scripts.Effects;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
	public enum Multiplier {
		X1,
		X2,
		X3
	}

	public enum ResultType
	{
		NONE,
		GET_MONEY,
		TAKE_ENEMY_DAMAGE
	}

	public class SpinResult
	{
		public ResultType type = ResultType.NONE;
		public List<BaseEffect> effects;
		public Sprite sprite;
	}


	public class SlotTypeManager : MonoBehaviour
	{

		public Dictionary<CellSymbol, SlotType> types = new Dictionary<CellSymbol, SlotType>();
		public CellSymbol[] damageTypes;
		public CellSymbol[] giveMoneyTypes;

		[HideInInspector] public float[] weights;
		private bool isInit = false;
		private void Awake() {
			Init();
			gameObject.SetActive(false);
		}

		public void Init() {
			if (isInit) {
				return;
			}
			isInit = true;
			var childs = GetComponentsInChildren<SlotType>(true);
			for (int i = 0; i < childs.Length; i++) {
				childs[i].Init();
				types[childs[i].type] = childs[i];
			}
			SetWeights();
		}

		private void SetWeights() {
			weights = new float[types.Count];
			for (int i = 0; i < types.Count; i++) {
				weights[i] = (i == 0 ? 0 : weights[i - 1]) + types[(CellSymbol)i].frequency;
			}
		}

		public SlotType GetRandomSymbol() {
			Init();
			if (weights.Length > 0) {
				float r = Random.Range(0, weights[weights.Length - 1]);
				for (int i = 0; i < weights.Length; i++) {
					if (r < weights[i])
						return types[(CellSymbol)i];
				}
			}
			return new SlotType();
		}

		public SpinResult CheckResult(CellSymbol lineFirst, CellSymbol lineTwo, CellSymbol lineThree)
		{
			SpinResult result = new SpinResult();
			
			if (lineFirst == lineTwo) {
				if (lineFirst == lineThree) {
					result = GetX3Result(lineFirst);
				}
				else  {
					result = GetX2Result(lineFirst);
				}
			}
			else if (lineFirst == lineThree) {
				result = GetX2Result(lineFirst);
			}
			else if (lineTwo == lineThree) {
				result = GetX2Result(lineTwo);
			}

			return result;
		}

		SpinResult GetX2Result(CellSymbol lineFirst) {
			SpinResult result = new SpinResult();
			if (CheckDamageTypes(lineFirst)) {
				result.type = ResultType.TAKE_ENEMY_DAMAGE;
			} else if (CheckMoneyTypes(lineFirst)) {
				result.type = ResultType.GET_MONEY;
			}
			result.effects = types[lineFirst].GetEffects();
			foreach(BaseEffect effect in result.effects) {
				effect.SetMultiplier(Multiplier.X2);
			}
			result.sprite = types[lineFirst].sprite;
			return result;
		}

		SpinResult GetX3Result(CellSymbol lineFirst) {
			SpinResult result = new SpinResult();
			if (CheckDamageTypes(lineFirst)) {
				result.type = ResultType.TAKE_ENEMY_DAMAGE;
			} else if (CheckMoneyTypes(lineFirst)) {
				result.type = ResultType.GET_MONEY;
			}
			result.effects = types[lineFirst].GetEffects();
			foreach (BaseEffect effect in result.effects) {
				effect.SetMultiplier(Multiplier.X3);
			}
			result.sprite = types[lineFirst].sprite;
			return result;
		}

		private bool CheckDamageTypes(CellSymbol type)
		{
			return checkTypes(damageTypes, type);
		}

		private bool CheckMoneyTypes(CellSymbol type) {
			return checkTypes(giveMoneyTypes, type);
		}

		private bool checkTypes(CellSymbol[] types, CellSymbol type) {
			for (int i = 0; i < types.Length; i++) {
				if (type == types[i]) {
					return true;
				}
			}

			return false;
		}
	}
}
