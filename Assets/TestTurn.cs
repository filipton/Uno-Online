using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestTurn : MonoBehaviour
{
    public List<PlayerXD> players = new List<PlayerXD>();
    public int actualplayerindex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            NextPlayer();
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            players.Reverse();
            NextPlayer();
        }
    }

    void NextPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            int nextPlayer = i + 1;
            if (nextPlayer >= players.Count)
            {
                nextPlayer = 0;
            }

            if (players[i].b)
            {
                players[i].b = false;
                players[nextPlayer].b = true;
                break;
            }
        }
    }
}

[System.Serializable]
public class PlayerXD
{
    public string name;
    public bool b;
}