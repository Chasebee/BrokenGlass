using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Damage : MonoBehaviour
{
    public float move_speed;
    public float alpha_speed;
    Color alpha;
    TextMeshPro text;

    void Start() 
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        Invoke("Destroy_Self", 0.6f);
    }
    void Update()
    {
        transform.Translate(new Vector3(0, move_speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alpha_speed);
        text.color = alpha;
    }
    public void Destroy_Self() 
    {
        Destroy(gameObject);
    }
}
