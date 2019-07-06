using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{

	private void Awake()
	{
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		UpdateEffect();


	}

	private void FixedUpdate()
	{

		Move();


	}

	public abstract void UpdateEffect();

	public abstract void Move();

	public abstract void Attack();


	void Die()
	{
		Destroy(gameObject);
	}
}
