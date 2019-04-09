using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class Player
{
    int playerID;
    int connectionID;
    public Player(int playerID,int connectionID)
    {
        this.playerID = playerID;
        this.connectionID = connectionID;
    }

    public int getID()
    {
        return playerID;
    }

    public int getConnectionID()
    {
        return connectionID;
    }
}

