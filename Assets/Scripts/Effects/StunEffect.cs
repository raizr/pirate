using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Effects;
using UnityEngine;

public class StunEffect : BaseEffect {
	public float stunTime = 10.0f;
	public float multiplierX2 = 1.0f;
	public float multiplierX3 = 1.4f;

	public override void AcceptEffect(Ship ship) {
		switch (multiplier) {
			case Multiplier.X1:
				ship.SetStun(stunTime);
				break;
			case Multiplier.X2:
				ship.SetStun(stunTime * multiplierX2);
				break;
			case Multiplier.X3:
				ship.SetStun(stunTime * multiplierX3);
				break;
		}
	}
}
