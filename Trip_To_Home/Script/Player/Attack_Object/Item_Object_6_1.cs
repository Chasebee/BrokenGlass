using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Object_6_1 : MonoBehaviour
{
    public int type = 0;
    public float timmer;
    public GameObject explosion;
    Transform pos;
    Vector3 explosion_pos;
    void Start()
    {
        pos = transform.parent.GetComponent<Transform>();
    }

    void Update()
    {
        timmer += Time.deltaTime;
        if (timmer >= 3f)
        {
            explosion_pos = new Vector3(pos.position.x, pos.position.y, -3);
            explosion.GetComponent<Item_Object_6_2>().type = type;
            Instantiate(explosion, explosion_pos, transform.rotation);
            Destroy(gameObject);
        }
    }
}
