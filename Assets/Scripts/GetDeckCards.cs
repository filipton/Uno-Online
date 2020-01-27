using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDeckCards : NetworkBehaviour
{
    public NetworkIdentity ID;
    public PlayerInventory Inv;

    // Start is called before the first frame update
    void Start()
    {
        foreach (PlayerInventory inv in FindObjectsOfType<PlayerInventory>())
        {
            if (inv.isLocalPlayer)
            {
                ID = inv.GetComponent<NetworkIdentity>();
                Inv = inv;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCardButton()
    {
        Inv.CmdAddCard(ID);
    }
}