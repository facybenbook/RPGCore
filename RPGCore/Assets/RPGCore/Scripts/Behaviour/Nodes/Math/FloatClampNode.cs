﻿using RPGCore.Behaviour.Connections;
using System;
using UnityEngine;

namespace RPGCore.Behaviour.Math
{
	[NodeInformation ("Float/Clamp")]
	public class FloatClampNode : BehaviourNode
	{
		public FloatInput Value;
		public FloatInput Min;
		public FloatInput Max;

		public FloatOutput Output;

		protected override void OnSetup (IBehaviourContext character)
		{
			ConnectionEntry<float> valueInput = Value.GetEntry (character);
			ConnectionEntry<float> minInput = Min.GetEntry (character);
			ConnectionEntry<float> maxInput = Max.GetEntry (character);
			ConnectionEntry<float> output = Output.GetEntry (character);

			Action updateHandler = () =>
			{
				output.Value = Mathf.Clamp (valueInput.Value, minInput.Value, maxInput.Value);
			};

			valueInput.OnAfterChanged += updateHandler;
			minInput.OnAfterChanged += updateHandler;
			maxInput.OnAfterChanged += updateHandler;

			updateHandler ();
		}

		protected override void OnRemove (IBehaviourContext character)
		{
		}

#if UNITY_EDITOR
		public override Vector2 GetDiamentions ()
		{
			return new Vector2 (140, 70);
		}
#endif
	}
}