using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class Program : MonoBehaviour
{
    GameServer gameServer;
    private void Start()
    {
        gameServer = new GameServer();
        gameServer.init();
        NetCommon.gameServer = gameServer;
 
    }

    private void Update()
    {
        gameServer.Update();
    }



}

