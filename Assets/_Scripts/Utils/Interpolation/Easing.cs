using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom
{
	public enum EasingCurve
    {
		Linear,
		In,
		Out,
		InOut,
		Sin,
		SinIn,
		SinOut
    }

	public class Easing
    {
		[System.Serializable]
		public class EasingCachedCurve
		{
			public List<EasingCurve> Curves = new List<EasingCurve>();
			public List<float> Mods = new List<float>();
		}

		static public Dictionary<EasingCurve, EasingCachedCurve> CASHE;

		static public float Ease(float u, params EasingCurve[] curveParams)
		{
			if (CASHE is null)
			{
				CASHE = new Dictionary<EasingCurve, EasingCachedCurve>();
			}

			float u2 = u;
			foreach (EasingCurve curve in curveParams)
			{
				if (!CASHE.ContainsKey(curve))
				{
					EaseParse(curve);
				}
				u2 = EaseP(u2, CASHE[curve]);
			}
			return (u2);
		}

		static private void EaseParse(EasingCurve curveIn)
		{
			EasingCachedCurve ecc = new EasingCachedCurve();
			CASHE.Add(curveIn, ecc);
		}

		static public float Ease(float u, EasingCurve curve, float mod)
		{
			return (EaseP(u, curve, mod));
		}

		static private float EaseP(float u, EasingCachedCurve ec)
		{
			float u2 = u;
			for (int i = 0; i < ec.Curves.Count; i++)
			{
				u2 = EaseP(u2, ec.Curves[i], ec.Mods[i]);
			}
			return (u2);
		}

		static private float EaseP(float u, EasingCurve curve, float mod)
		{
			float u2 = u;

			switch (curve)
			{
				case EasingCurve.In:
					if (float.IsNaN(mod)) mod = 2;
					u2 = Mathf.Pow(u, mod);
					break;

				case EasingCurve.Out:
					if (float.IsNaN(mod)) mod = 2;
					u2 = 1 - Mathf.Pow(1 - u, mod);
					break;

				case EasingCurve.InOut:
					if (float.IsNaN(mod)) mod = 2;
					if (u <= 0.5f)
					{
						u2 = 0.5f * Mathf.Pow(u * 2, mod);
					}
					else
					{
						u2 = 0.5f + 0.5f * (1 - Mathf.Pow(1 - (2 * (u - 0.5f)), mod));
					}
					break;

				case EasingCurve.Sin:
					if (float.IsNaN(mod)) mod = 0.15f;
					u2 = u + mod * Mathf.Sin(2 * Mathf.PI * u);
					break;

				case EasingCurve.SinIn:
					u2 = 1 - Mathf.Cos(u * Mathf.PI * 0.5f);
					break;

				case EasingCurve.SinOut:
					u2 = Mathf.Sin(u * Mathf.PI * 0.5f);
					break;

				case EasingCurve.Linear:
				default:
					break;
			}

			return (u2);
		}
	}
}
