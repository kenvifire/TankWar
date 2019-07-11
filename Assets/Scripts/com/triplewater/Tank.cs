using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.triplewater
{
	class Position
	{
		public float x, y;

		public Position(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		
	}


	public abstract class Tank : MonoBehaviour
	{
		public GameObject bulletPrefeb;
		public GameObject explosionPrefab;
		public GameObject defendEffectPrefab;
		public float moveSpeed;
		public Sprite[] tankSprites;

		private SpriteRenderer _render;

		internal Direction currentDirection;
		internal float velocity;
		internal float bulletTime;
		internal bool isDefend;
		internal float defendTime;
		internal Role role;

		

		private void Awake()
		{
			_render = GetComponent<SpriteRenderer>();
		}

		// Start is called before the first frame update
		void Start()
		{
			Init();
		}

		// Update is called once per frame
		void Update()
		{
			UpdateInternal();
			Defend();
			Attack();
		}

		private void FixedUpdate()
		{
			FixedUpdateInternal();
			Move(Time.fixedDeltaTime);
		}

		internal void Move(float time)
		{
			float distance = velocity * moveSpeed * time;
			switch (currentDirection)
			{
				case Direction.Up:
					transform.Translate(Vector3.up * distance, Space.World);
					break;
				case Direction.Down:
					transform.Translate(Vector3.down * distance, Space.World);
					break;
				case Direction.Left:
					transform.Translate(Vector3.left * distance, Space.World);
					break;
				case Direction.Right:
					transform.Translate(Vector3.right * distance, Space.World);
					break;
				default:
					Debug.LogWarning("incorrect direction:" + currentDirection);
					break;
			}

			_render.sprite = tankSprites[(int) currentDirection];
		}


		internal void GenerateBullet()
		{
			GameObject bulletObject = Instantiate(bulletPrefeb, transform.position,
				Quaternion.Euler(transform.eulerAngles + GetBulletDirection())) as GameObject;
			Bullet bullet = bulletObject.GetComponent<Bullet>();
			bullet.role = role;
		}

		Vector3 GetBulletDirection()
		{
			float z = 0;

			switch (currentDirection)
			{
				case Direction.Up:
					z = 0;
					break;
				case Direction.Down:
					z = -180;
					break;
				case Direction.Left:
					z = 90;
					break;
				case Direction.Right:
					z = -90;
					break;
			}

			return new Vector3(0, 0, z);
		}

		internal void Die()
		{
			if (isDefend)
			{
				return;
			}
			Destroy(gameObject);
			Explode();
		}

		private void Explode()
		{
			GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
			explosion.GetComponent<Explosion>().StartCoroutine(DelayDestory(explosion, 0.20f));
		}

		internal void Defend()
		{
			if (isDefend)
			{
				defendEffectPrefab.SetActive(true);
				defendTime -= Time.deltaTime;
				if (defendTime < 0)
				{
					defendEffectPrefab.SetActive(false);
					isDefend = false;
				}
			}
			else
			{
				defendEffectPrefab.SetActive(false);
			}
			defendEffectPrefab.SetActive(false);
			
		}

		private IEnumerator DelayDestory(Object obj, float delayTime)
		{
			yield return new WaitForSeconds(delayTime);
			Destroy(obj);
		}

		internal abstract void Init();

		internal abstract void Attack();

		protected abstract void FixedUpdateInternal();

		internal abstract void UpdateInternal();

	}
}
