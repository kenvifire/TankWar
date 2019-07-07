using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Player : MonoBehaviour
{
    public float moveSpeed = 3;
    private SpriteRenderer render;
    public Sprite[] tankSprites;
    public GameObject bulletPrefeb;
    private Vector3 bulletEulerAngles;
    private float timer = 0.5f;
    public GameObject explosionPrefab;
    private bool isDefend = true;
    private float defendTimeVal;
    public GameObject defendEffectPrefab;


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
        if (isDefend)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefend = false;
                defendEffectPrefab.SetActive(false);
            }
        }

        if (timer >= 0.4f)
        {
            Attack();

        } else
        {
            timer += Time.deltaTime;
        }
        

    }

    private void FixedUpdate()
    {

        Move();
        
        
    }

    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        
        Debug.Log("Distance of player: " + v * moveSpeed * Time.fixedDeltaTime);
        if (v < 0)
        {
            render.sprite = tankSprites[2];
            bulletEulerAngles = new Vector3(0, 0, 180);

        }
        else if (v > 0)
        {
            render.sprite = tankSprites[0];
            bulletEulerAngles = new Vector3(0, 0, 0);

        }

        if (v > 1e-6 || v < -1e-6)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        
        if (h < 0)
        {
            render.sprite = tankSprites[3];
            bulletEulerAngles = new Vector3(0, 0, 90);

        }
        else if (h > 0)
        {
            render.sprite = tankSprites[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }

    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefeb, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            
            timer = 0;
        }
    }
    

    private void Die()
    {
        if (isDefend)
        {
            return;
        }
        

        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
