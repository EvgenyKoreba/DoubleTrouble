using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Custom
{
	public class Interpolation : MonoBehaviour
	{
        #region Bezeir
        static public Vector3 Bezier(float u, List<Vector3> points, int leftIndex = 0, int rightIndex = -1)
		{
			if (rightIndex == -1) rightIndex = points.Count - 1;
			if (leftIndex == rightIndex)
			{
				return (points[leftIndex]);
			}

			Vector3 l = Bezier(u, points, leftIndex, rightIndex - 1);
			Vector3 r = Bezier(u, points, leftIndex + 1, rightIndex);
			Vector3 res = Lerp(l, r, u);
			return (res);
		}
		static public Vector3 Bezier(float u, params Vector3[] arrayPoints)
		{
			return (Bezier(u, new List<Vector3>(arrayPoints)));
		}

		static public Vector2 Bezier(float u, List<Vector2> points, int leftIndex = 0, int rightIndex = -1)
		{
			if (rightIndex == -1) rightIndex = points.Count - 1;
			if (leftIndex == rightIndex)
			{
				return (points[leftIndex]);
			}

			Vector2 l = Bezier(u, points, leftIndex, rightIndex - 1);
			Vector2 r = Bezier(u, points, leftIndex + 1, rightIndex);
			Vector2 res = Lerp(l, r, u);
			return (res);
		}
		static public Vector2 Bezier(float u, params Vector2[] arrayPoints)
		{
			return (Bezier(u, new List<Vector2>(arrayPoints)));
		}

		static public float Bezier(float u, List<float> points, int leftIndex = 0, int rightIndex = -1)
		{
			if (rightIndex == -1) rightIndex = points.Count - 1;
			if (leftIndex == rightIndex)
			{
				return (points[leftIndex]);
			}

			float l = Bezier(u, points, leftIndex, rightIndex - 1);
			float r = Bezier(u, points, leftIndex + 1, rightIndex);
			float res = Lerp(l, r, u);
			return (res);
		}
		static public float Bezier(float u, params float[] arrayPoints)
		{
			return (Bezier(u, new List<float>(arrayPoints)));
		}

		static public Quaternion Bezier(float u, List<Quaternion> points, int leftIndex = 0, int rightIndex = -1)
		{
			if (rightIndex == -1) rightIndex = points.Count - 1;
			if (leftIndex == rightIndex)
			{
				return (points[leftIndex]);
			}

			Quaternion l = Bezier(u, points, leftIndex, rightIndex - 1);
			Quaternion r = Bezier(u, points, leftIndex + 1, rightIndex);
			Quaternion res = Quaternion.SlerpUnclamped(l, r, u);
			return (res);
		}
		static public Quaternion Bezier(float u, params Quaternion[] arrayPoints)
		{
			return (Bezier(u, new List<Quaternion>(arrayPoints)));
		}
        #endregion
        #region Linear
        static public Vector3 Lerp(Vector3 from, Vector3 to, float u)
		{
			Vector3 res = (1 - u) * from + u * to;
			return (res);
		}

		static public Vector2 Lerp(Vector2 from, Vector2 to, float u)
		{
			Vector2 res = (1 - u) * from + u * to;
			return (res);
		}

		static public float Lerp(float from, float to, float u)
		{
			float res = (1 - u) * from + u * to;
			return (res);
		}
		#endregion
	}
}
