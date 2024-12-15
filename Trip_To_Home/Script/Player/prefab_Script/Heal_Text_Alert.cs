using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Heal_Text_Alert : MonoBehaviour
{
    public GameObject player;
    public Transform pos;
    public float move_speed;
    public float alpha_Speed;
    TextMeshPro text;
    Color alpha;
    private float value;
    void Start()
    {
        value = Random.Range(-1, 2);
        player = GameObject.FindWithTag("Player");
        pos = player.GetComponent<Transform>();
        gameObject.transform.localPosition = new Vector3(pos.position.x+value, pos.position.y, -3);
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        Invoke("Text_Destory", 3f);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, move_speed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alpha_Speed);
        text.color = alpha;
    }
    private void Text_Destory()
    {
        Destroy(gameObject);
    }
}
