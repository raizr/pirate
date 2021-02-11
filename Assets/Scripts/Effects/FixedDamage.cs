namespace Assets.Scripts.Effects {
	public class FixedDamage : BaseEffect {
		public float damage = 10.0f;
		public float multiplierX2 = 1.0f;
		public float multiplierX3 = 1.4f;

		public override void AcceptEffect(Ship ship) {
			switch (multiplier) {
				case Multiplier.X1:
					ship.DoDamage(damage);
					break;
				case Multiplier.X2:
					ship.DoDamage(damage * multiplierX2);
					break;
				case Multiplier.X3:
					ship.DoDamage(damage * multiplierX3);
					break;
			}
		}
	}
}
