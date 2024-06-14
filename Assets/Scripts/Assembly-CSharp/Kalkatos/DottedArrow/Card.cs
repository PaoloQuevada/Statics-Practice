using TMPro;
using UnityEngine;

namespace Kalkatos.DottedArrow
{
	public class Card : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text powerText;

		[SerializeField]
		private TMP_Text healthText;

		[SerializeField]
		private int power;

		[SerializeField]
		private int health;

		private bool isAttacking;

		public int Power => power;

		private void Awake()
		{
			powerText.text = power.ToString();
			healthText.text = health.ToString();
		}

		public void BeginAttack()
		{
			if (!isAttacking)
			{
				isAttacking = true;
				CombatManager.instance.BeginAttack(this);
			}
		}

		public void EndAttack()
		{
			isAttacking = false;
		}
	}
}
