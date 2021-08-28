using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();

        player.SetDisplayName($"Player {numPlayers}");

        //player.SetDisplayColor(Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))); //moje dzia³a³o, poni¿ej to samo tylko ladniej

        Color displayColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));

        player.SetDisplayColor(displayColor);

    }
}
