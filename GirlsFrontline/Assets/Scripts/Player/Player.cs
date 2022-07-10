using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public int ID;
    private void Start()
    {
        ID = UIManager.Instance.TeamNum++;
        //TODO:用SriptObject保存
        PlayerPrefs.SetInt("Team"+ID.ToString(), ID);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
