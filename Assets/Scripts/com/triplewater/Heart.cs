using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.triplewater
{

	public class Heart : MonoBehaviour
	{
		private SpriteRenderer render;

		public Sprite brokenSprite;
		public GameObject explosionPrefab;
		public AudioClip dieAudio;
		
		// Start is called before the first frame update
		void Start()
		{
			render = GetComponent<SpriteRenderer>();

		}

		public void Die()
		{
			render.sprite = brokenSprite;
			Instantiate(explosionPrefab, transform.position, transform.rotation);
			AudioSource.PlayClipAtPoint(dieAudio, transform.position);
		}
	}

}
