using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Money_Item", menuName = "Scriptable Obejct/ Money_Item")]
public class Money_Item : ScriptableObject
{
    public string item_name; // 아이템 이름
    public Sprite item_img; // 아이템 이미지
    public int[] money;
}
