using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Title_Btn : MonoBehaviour
{
    public GameObject alert_Object;
    public void Start_Game()
    {
        string path = Path.Combine(Application.persistentDataPath, "SaveData/Save.json");
        if (File.Exists(path))
        {
            alert_Object.SetActive(true);
        }
        else
        {
            Loading_Scene.LoadScene("Map_1");
        }
    }

    public void New_Game() 
    {
        Loading_Scene.LoadScene("Map_1");
    }
    public void Cancle() 
    {
        alert_Object.SetActive(false);
    }

    public void Load_Game() 
    {
        GameManager.instance.Load_PlayerData();
        if (GameManager.instance.save_map != "")
        {
            Loading_Scene.LoadScene(GameManager.instance.save_map);
        }
        else 
        {
            Debug.Log("세이브 데이터가 없습니다.");
        }
    }
}
