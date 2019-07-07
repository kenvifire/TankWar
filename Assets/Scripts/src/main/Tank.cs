using UnityEngine;

public abstract class Tank : MonoBehaviour
{
	private SpriteRenderer render;
	public Sprite[] tankSprites;
	public GameObject bulletPrefeb;
	
	
	private void Awake()
	{
		render = GetComponent<SpriteRenderer>();
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		UpdateInternal();
	}

	private void FixedUpdate()
	{
		FixedUpdateInternal();
	}
	
	internal void Move(Direction direction, float speed, float time)
	{
		float distance = speed * time;
		switch (direction)
		{
			case Direction.Up:
				transform.Translate(Vector3.up * distance, Space.World);
				break;
			case Direction.Down:
				transform.Translate(Vector3.down * distance, Space.World);
				break;
			case Direction.Left:
				transform.Translate(Vector3.left* distance, Space.World);
				break;
			case Direction.Right:
				transform.Translate(Vector3.right * distance, Space.World);
				break;
			default:
				Debug.LogWarning("incorrect direction:" + direction);
				break;
		}
		
		render.sprite = tankSprites[(int) direction];
	}


	internal void GenerateBullet(Vector3 rotation) 
	{
		GameObject bulletObject = Instantiate(bulletPrefeb, transform.position, 
			Quaternion.Euler(transform.eulerAngles + rotation)) as GameObject;
		Bullet bullet = bulletObject.GetComponent<Bullet>();
		bullet.tag = tag;
	}
	
	internal void Die()
	{
		Destroy(gameObject);
	}
	
	public abstract void Attack();

	internal abstract void FixedUpdateInternal();

	internal abstract void UpdateInternal();
}
