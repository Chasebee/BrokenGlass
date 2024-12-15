using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Trace : MonoBehaviour
{
    // Gizmo 설정
    public Vector2 center, size;
    float width, height;
    // 캐릭터 추적
    public float speed = 3;
    GameObject player;
    Transform target_pos;
    float c_size = 10f;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        target_pos = player.GetComponent<Transform>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
    void LateUpdate()
    {
        Vector2 t_pos = new Vector2(target_pos.position.x, (target_pos.position.y + 5.5f));
        transform.position = Vector3.Lerp(transform.position, t_pos, Time.deltaTime * GameManager.instance.speed);
        //transform.position = new Vector3(transform.position.x, transform.position.y, -20);

        float lx = size.x * 0.5f - width;
        float clapX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);
        
        float ly = size.y * 0.5f - height;
        float clapy = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clapX, clapy, -20);
    }
}
