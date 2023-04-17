using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Viego playerScript;

    
    void Start()
    {
        playerScript = GameObject.Find("Viego").GetComponent<Viego>();
    }

    // Update is called once per frame
    void Update()
    {
        SaveData();
        LoadData();
        
    }

    
    void LoadData()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            string fileName = "character.txt";

            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // ??c 6 dòng ??u tiên và l?u giá tr? vào các bi?n t??ng ?ng
                    playerScript.level = int.Parse(Decrypt(sr.ReadLine()));
                    playerScript.hp = float.Parse(Decrypt(sr.ReadLine()));
                    playerScript.maxHp = float.Parse(Decrypt(sr.ReadLine()));
                    playerScript.exp = float.Parse(Decrypt(sr.ReadLine()));
                    playerScript.maxExp = float.Parse(Decrypt(sr.ReadLine()));
                    playerScript.damage = float.Parse(Decrypt(sr.ReadLine()));
                }
            }
            else
            {
                Console.WriteLine("Không tìm th?y file!");
            }
        }
    }

    void SaveData()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            string fileName = "character.txt";
            string[] linesToWrite = new string[]
            {
                Encrypt(playerScript.level.ToString()),
                Encrypt(playerScript.hp.ToString()),
                Encrypt(playerScript.maxHp.ToString()),
                Encrypt(playerScript.exp.ToString()),
                Encrypt(playerScript.maxExp.ToString()),
                Encrypt(playerScript.damage.ToString())
            };

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (string line in linesToWrite)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
    string Encrypt(string s)
    {
        string[] map = { "a", "b", "c", "d", "e", "f", "g", "h", "j", "i", "k" };
        StringBuilder result = new StringBuilder();
        foreach (char c in s)
        {
            if (c >= '0' && c <= '9')
            {
                result.Append(map[c - '0']);
            }
            else if (c == '.')
            {
                result.Append(map[10]);
            }
        }
        return result.ToString();
    }
    string Decrypt(string s)
    {
        string[] map = { "a", "b", "c", "d", "e", "f", "g", "h", "j", "i", "k" };
        Dictionary<string, string> reverseMap = new Dictionary<string, string>();
        for (int i = 0; i < map.Length; i++)
        {
            reverseMap[map[i]] = i.ToString();
        }
        reverseMap[map[10]] = ".";

        StringBuilder result = new StringBuilder();
        foreach (char c in s)
        {
            if (reverseMap.ContainsKey(c.ToString()))
            {
                result.Append(reverseMap[c.ToString()]);
            }
            else
            {
                result.Append(c);
            }
        }
        return result.ToString();
    }
}
