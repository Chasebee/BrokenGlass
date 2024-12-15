using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Money_Item", menuName = "Scriptable Obejct/ Money_Item")]
public class Money_Item : ScriptableObject
{
    public string item_name; // ������ �̸�
    public Sprite item_img; // ������ �̹���
    public int[] money;
}
