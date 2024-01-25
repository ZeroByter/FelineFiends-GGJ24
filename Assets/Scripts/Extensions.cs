using System.Linq;
using UnityEngine;

namespace Extensions
{
	static class Extensions
	{
		public static Vector2 Direction(Vector2 source, Vector2 target)
		{
			return target - source;
		}

		public static Vector2 Direction(this Transform source, Vector2 target)
		{
			return Direction(source.position, target);
		}

		public static Vector2 Direction(this Transform source, Transform target)
		{
			return Direction(source.position, target.position);
		}

		public static void LookAt2D(this Transform transform, Vector2 target)
		{
			transform.right = Direction(transform.position, target);
		}

		public static void LookAt2D(this Transform transform, Transform target)
		{
			LookAt2D(transform, target.position);
		}

		public static Vector2 Rotate2DDeg(this Vector2 vector, float degrees)
		{
			return vector.Rotate2DRad(degrees * Mathf.Deg2Rad);
		}

		public static Vector2 Rotate2DRad(this Vector2 vector, float radians)
		{
			float sin = Mathf.Sin(radians);
			float cos = Mathf.Cos(radians);
			return new((cos * vector.x) - (sin * vector.y), (sin * vector.x) + (cos * vector.y));
		}

		public static bool Contains(this LayerMask mask, int layer)
		{
			return mask == (mask | (1 << layer));
		}

		public static int LayerIndex(this LayerMask mask)
        {
			return Mathf.RoundToInt(Mathf.Log(mask.value, 2));
		}

		public static Vector2 GetAverageContactPoint(this Collision2D collision)
		{
			var points = new ContactPoint2D[collision.contactCount];
			collision.GetContacts(points);
			Vector2 averagePoint = Vector2.zero;
			foreach (var point in points)
				averagePoint += point.point;
			averagePoint /= points.Length;
			return averagePoint;
		}

		public static bool GetRandomBool()
		{
			return Random.Range(0, 2) == 1;
		}

		public static float GetRandomPolarity(float multiplier = 1f)
		{
			return GetRandomBool() ? multiplier : -multiplier;
		}

		public static Vector2 GetRandomDirection2DRad(float rangeRadians)
		{
			return new(Mathf.Cos(rangeRadians), Mathf.Sin(rangeRadians));
		}

		public static Vector2 GetRandomDirection2DDeg(float rangeDegrees)
		{
			return GetRandomDirection2DRad(rangeDegrees*Mathf.Deg2Rad);
		}

		public static Transform[] GetTransformsInChildrenExcludingParent(this Transform parent)
		{
			return parent.GetComponentsInChildren<Transform>().Where(o => o != parent).ToArray();
		}
	}
}
