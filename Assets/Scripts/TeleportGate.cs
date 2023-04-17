using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    private GameObject player;
    public Viego playerScript;
    public GameObject TeleportGateUI;
    public GameObject[] TeleportPoints;
    private GameObject UIManager;
    private Vector3 offset;
    void Start()
    {
        offset = new Vector3(0, 0, -0.5f);
        player = GameObject.Find("Viego");
        playerScript = GameObject.Find("Viego").GetComponent<Viego>();
        UIManager = GameObject.Find("UIManager");
    }
    public void CloseTeleportGateUI()
    {
        TeleportGateUI.SetActive(false);
    }
    public void GoToArea1()
    {
        player.transform.position = TeleportPoints[0].transform.position + offset;
    }
    public void GoToArea2()
    {
        player.transform.position = TeleportPoints[1].transform.position + offset;
    }
    public void GoToArea3()
    {
        player.transform.position = TeleportPoints[2].transform.position + offset;
    }
    public void GoToArea4()
    {
        player.transform.position = TeleportPoints[3].transform.position + offset;
    }
    public void GoToArea5()
    {
        player.transform.position = TeleportPoints[4].transform.position + offset;
    }
    public void GoToArea6()
    {
        player.transform.position = TeleportPoints[5].transform.position + offset;
    }
    public void GoToArea7()
    {
        player.transform.position = TeleportPoints[6].transform.position + offset;
    }
    public void GoToArea8()
    {
        player.transform.position = TeleportPoints[7].transform.position + offset;
    }
    public void GoToArea9()
    {
        player.transform.position = TeleportPoints[8].transform.position + offset;
    }
    public void GoToArea10()
    {
        player.transform.position = TeleportPoints[9].transform.position + offset;
    }
    public void GoToArea11()
    {
        player.transform.position = TeleportPoints[10].transform.position + offset;
    }
    public void GoToArea12()
    {
        player.transform.position = TeleportPoints[11].transform.position + offset;
    }
    public void GoToArea13()
    {
        player.transform.position = TeleportPoints[12].transform.position + offset;
    }
    public void GoToArea14()
    {
        player.transform.position = TeleportPoints[13].transform.position + offset;
    }
    public void GoToArea15()
    {
        player.transform.position = TeleportPoints[14].transform.position + offset;
    }
    public void GoToArea16()
    {
        player.transform.position = TeleportPoints[15].transform.position + offset;
    }
    public void GoToArea17()
    {
        player.transform.position = TeleportPoints[16].transform.position + offset;
    }
    public void GoToArea18()
    {
        player.transform.position = TeleportPoints[17].transform.position + offset;
    }
    public void GoToArea19()
    {
        player.transform.position = TeleportPoints[18].transform.position + offset;
    }
    public void GoToArea20()
    {
        player.transform.position = TeleportPoints[19].transform.position + offset;
    }
    public void GoToArea21()
    {
        player.transform.position = TeleportPoints[20].transform.position + offset;
    }
    public void GoToArea22()
    {
        player.transform.position = TeleportPoints[21].transform.position + offset;
    }
    public void GoToArea23()
    {
        player.transform.position = TeleportPoints[22].transform.position + offset;
    }
    public void GoToArea24()
    {
        player.transform.position = TeleportPoints[23].transform.position + offset;
    }
    public void GoToArea25()
    {
        player.transform.position = TeleportPoints[24].transform.position + offset;
    }
    public void GoToArea26()
    {
        player.transform.position = TeleportPoints[25].transform.position + offset;
    }
    public void GoToArea27()
    {
        player.transform.position = TeleportPoints[26].transform.position + offset;
    }
    public void GoToArea28()
    {
        player.transform.position = TeleportPoints[27].transform.position + offset;
    }
    public void GoToArea29()
    {
        player.transform.position = TeleportPoints[28].transform.position + offset;
    }
    public void GoToArea30()
    {
        player.transform.position = TeleportPoints[29].transform.position + offset;
    }
    public void GoToArea31()
    {
        player.transform.position = TeleportPoints[30].transform.position + offset;
    }
    public void GoToArea32()
    {
        player.transform.position = TeleportPoints[31].transform.position + offset;
    }

}
