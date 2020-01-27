using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : NetworkBehaviour
{
    [SyncVar]
    public int playersConnected;

    [SyncVar]
    public float TimeToStart = 10f;

    //public TextMeshProUGUI PlayerCounter;
    //public Image TimerImage;
    //public GameObject StartGameHost;

    CustomNetworkManager nmc;
    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            //StartGameHost.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (nmc == null)
                nmc = FindObjectOfType<CustomNetworkManager>();

            playersConnected = CustomNetworkManager.singleton.numPlayers;
            //PlayerCounter.text = playersConnected.ToString();

            if (playersConnected > 1)
            {
                TimeToStart -= Time.deltaTime;
                //TimerImage.fillAmount = TimeToStart / 10;
            }
            if (playersConnected < 2)
            {
                TimeToStart = 10f;
            }

            if (TimeToStart <= 0)
            {
                TimeToStart = 10;
                CustomNetworkManager.singleton.ServerChangeScene("Multiplayer");
            }
        }

        if (isClient)
        {
            //PlayerCounter.text = playersConnected.ToString();
            //TimerImage.fillAmount = TimeToStart / 10;
        }
    }

    public void ForceStart()
    {
        CustomNetworkManager.singleton.ServerChangeScene("Multiplayer");
    }

    [Command]
    public void CmdAddToList(NetworkIdentity ID)
    {
        ////
    }
}