using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Drop_Item : MonoBehaviour
{
    public Equip_Item equip;

    public int mon_num;
    public int type;
    public int money;
    public Equip_Item equip_item;
    public Use_Item use_item;
    public Etc_Item etc_item;
    public int use_max;
    public int etc_max;
    public SpriteRenderer rend;
    Rigidbody2D rigid;
    void Start() 
    {
        rend = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (collision.CompareTag("Player")) 
        {
            // 방어구같은건 typ를 4로주자
            if (type == 0)
            {
                GameManager.instance.gold += money;
            }
            else if (type == 1)
            {
                InvenTory_Equipment inven_item = new InvenTory_Equipment();
                float[] plus_option = equip_item.item_option;
                // 추가옵션 랜덤 지정
                if (equip_item.item_option != null && equip_item.item_option.Length > 0)
                {
                    // 장비
                    if (type == 1)
                    {
                        if (equip_item.type == 1)
                        {
                            for (int i = 0; i < equip_item.item_option.Length; i++)
                            {
                                plus_option[i] = equip_item.item_option[i] = Mathf.RoundToInt(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]));
                                if (i == 1 && equip_item.attack_type == 1)
                                {
                                    plus_option[i] = equip_item.item_option[i] = Mathf.Round(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]) * 100f) / 100;
                                }
                                if (i == 2)
                                {
                                    plus_option[i] = equip_item.item_option[i] = Mathf.Round(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]) * 100f) / 100;
                                }
                            }
                        }
                        // 상의
                        else if (equip_item.type == 3)
                        {
                            for (int i = 0; i < equip_item.item_option.Length; i++)
                            {
                                plus_option[i] = equip_item.item_option[i] = Mathf.RoundToInt(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]));
                                if (i == 1 && equip_item.attack_type == 1)
                                {
                                    plus_option[i] = equip_item.item_option[i] = Mathf.RoundToInt(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]) * 100f) / 100;
                                }
                                if (i == 2)
                                {
                                    plus_option[i] = equip_item.item_option[i] = Mathf.RoundToInt(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]) * 100f) / 100;
                                }
                            }
                        }
                        // 하의
                        else if (equip_item.type == 4)
                        {
                            for (int i = 0; i < equip_item.item_option.Length; i++)
                            {
                                plus_option[i] = equip_item.item_option[i] = Mathf.RoundToInt(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]));
                                if (i == 1 && equip_item.attack_type == 1)
                                {
                                    plus_option[i] = equip_item.item_option[i] = Mathf.RoundToInt(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]) * 100f) / 100;
                                }
                                if (i == 2)
                                {
                                    plus_option[i] = equip_item.item_option[i] = Mathf.Round(Random.Range(equip_item.item_option_min[i], equip_item.item_option_max[i]) * 100f) / 100;
                                }
                            }
                        }
                    }
                }

                // 인벤토리 추가 로직
                inven_item.item_id = equip_item.item_id;
                inven_item.item_name = equip_item.item_name;
                inven_item.item_exp = equip_item.item_exp;
                inven_item.item_lv = equip_item.item_lv;
                inven_item.type = equip_item.type;
                inven_item.attack_type = equip_item.attack_type;
                inven_item.base_option = equip_item.base_option;
                inven_item.enforce = equip_item.enforce;
                inven_item.sell_price = equip_item.sell_price;
                inven_item.rare = equip_item.rare;
                inven_item.item_option = plus_option;
                //inven_item.item_skill_option = equip.item_skill_option; 아직 스킬로직은 없으니!

                GameManager.instance.inventory_equip.Add(inven_item);
            }
            else if (type == 2) 
            {
                InvenTory_Use inven_item = new InvenTory_Use();
                inven_item.item_id = use_item.item_id;
                inven_item.item_type = use_item.item_type;
                inven_item.item_name = use_item.item_name;
                inven_item.item_exp = use_item.item_exp;
                inven_item.sell_price = use_item.sell_price;
                inven_item.rare = use_item.rare;
                
                inven_item.hp_recover = use_item.hp_recover;
                inven_item.mp_recover = use_item.mp_recover;
                inven_item.map_name = use_item.map_name;
                inven_item.reserves = use_max;
                if (GameManager.instance.inventory_use.Count >= 1)
                {
                    bool found = false;
                    for (int i = 0; i < GameManager.instance.inventory_use.Count; i++)
                    {
                        if (GameManager.instance.inventory_use[i].item_id == inven_item.item_id)
                        {
                            GameManager.instance.inventory_use[i].reserves += inven_item.reserves;
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        GameManager.instance.inventory_use.Add(inven_item);
                    }
                }
                else 
                {
                    GameManager.instance.inventory_use.Add(inven_item);
                }
            }
            else if (type == 3)
            {
                InvenTory_Etc inven_item = new InvenTory_Etc();

                inven_item.item_id = etc_item.item_id;
                inven_item.item_name = etc_item.item_name;
                inven_item.item_exp = etc_item.item_exp;
                inven_item.sell_price = etc_item.sell_price;
                inven_item.rare = etc_item.rare;
                inven_item.reserves = etc_max;

                if (GameManager.instance.inventory_etc.Count >= 1)
                {
                    bool found = false;
                    for (int i = 0; i < GameManager.instance.inventory_etc.Count; i++)
                    {
                        if (GameManager.instance.inventory_etc[i].item_id == inven_item.item_id)
                        {
                            GameManager.instance.inventory_etc[i].reserves += inven_item.reserves;
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        GameManager.instance.inventory_etc.Add(inven_item);
                    }
                }
                else
                {
                    GameManager.instance.inventory_etc.Add(inven_item);
                }
            }
            Destroy(gameObject);
        }
    }
}
