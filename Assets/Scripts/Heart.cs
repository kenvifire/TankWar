using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
	private SpriteRenderer render;

	public Sprite brokenSprite;
    // Start is called before the first frame update
    void Start()
    {
		render = GetComponent<SpriteRenderer>();
        
    }

    public void Die()
	{
		render.sprite = brokenSprite;
	}
}
