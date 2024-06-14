using System.Collections;
using UnityEngine;

namespace Kalkatos.DottedArrow
{
	public class CombatManager : MonoBehaviour
	{
		public static CombatManager instance;

		[SerializeField]
		private Arrow arrow;

		[SerializeField]
		private AnimationCurve attackAnimCurve;

		private Card attacker;

		public Arrow Arrow
		{
			get
			{
				return arrow;
			}
			set
			{
				arrow = value;
			}
		}

		private void Awake()
		{
			instance = this;
		}

		private IEnumerator AttackAnimationCoroutine(Card attacker, King receiver)
		{
			Vector3 originalUp = attacker.transform.up;
			Vector3 startPos = attacker.transform.position;
			yield return MoveTo(attacker.transform, startPos + Vector3.back, 0.2f);
			yield return new WaitForSeconds(0.1f);
			Vector3 vector = receiver.transform.position - startPos;
			vector = Vector3.MoveTowards(vector, vector * 0.001f, 1f);
			attacker.transform.up = vector;
			yield return MoveTo(attacker.transform, startPos + vector, 0.3f, attackAnimCurve);
			receiver.TakeDamage(attacker.Power);
			yield return MoveTo(attacker.transform, startPos, 0.3f);
			attacker.transform.up = originalUp;
		}

		private IEnumerator MoveTo(Transform transform, Vector3 endPos, float time, AnimationCurve curve = null)
		{
			float startTime = Time.time;
			float elapsed = 0f;
			Vector3 startPos = transform.position;
			while (elapsed < time)
			{
				elapsed = Time.time - startTime;
				float t = curve?.Evaluate(elapsed / time) ?? (elapsed / time);
				transform.position = Vector3.Lerp(startPos, endPos, t);
				yield return null;
			}
			transform.position = endPos;
		}

		public void BeginAttack(Card card)
		{
			CancelAttack();
			arrow.SetupAndActivate(card.transform);
			attacker = card;
		}

		public void EndAttack(King king)
		{
			arrow.Deactivate();
			StartCoroutine(AttackAnimationCoroutine(attacker, king));
			attacker.EndAttack();
		}

		public void CancelAttack()
		{
			arrow.Deactivate();
			if (attacker != null)
			{
				attacker.EndAttack();
				attacker = null;
			}
		}
	}
}
