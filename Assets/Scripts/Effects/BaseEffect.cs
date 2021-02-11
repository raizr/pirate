using UnityEngine;

namespace Assets.Scripts.Effects {
	public class BaseEffect : MonoBehaviour
	{
		protected Multiplier multiplier = Multiplier.X1;
		public virtual void AcceptEffect(Ship ship) {

		}
		
		public void SetMultiplier(Multiplier multiplier) {
			this.multiplier = multiplier;
		}
	}
}
