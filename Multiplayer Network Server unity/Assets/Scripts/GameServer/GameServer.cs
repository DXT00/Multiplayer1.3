using System.Collections.Generic;
using UnityEngine;

public class GameServer
{
    NetworkManager networkManager;
    Room room;
    int t = 0;
    public GameServer()
    {
        networkManager = new NetworkManager();
        CreateRoom();
    }

    public void init()
    {
        networkManager.init();
    }


    public void Update()
    {
        networkManager.ReceiveData();
       
        Tick();
        
       
        networkManager.SendData();
    }


    public void Tick()
    {
        room.Tick();
    }


    public void OnPlayerJoin(int clientID)
    {
        room.JoinRoom(clientID);
        List<string> player_name = room.GetPlayerID();
        NetworkMsg msg = new ReplyJoin(clientID, player_name);
        NetworkMsg msg2 = new ReplyID(clientID);
        networkManager.SendDataTo(clientID, msg2);
        //Debug.Log("sending replyID = " + clientID.ToString());
        room.roomBroadCast(msg);
    }

    void CreateRoom()
    {
        room = new Room();
        room.RegisterNetworkMgr(networkManager);
    }

    public void OnGameStart(int clientID)
    {
        room.StartGame();
        NetworkMsg msg = new ReplyStart(room.isGameStart);
        room.roomBroadCast(msg);

    }
    public void OnGetFrame(Frame frame, int clientID)
    {
        room.RecordFrame(frame, clientID);
      
        Tool.printFrameMsgList("OnGetFrame",frame);
    }

    public void OnAskFrame(List<int> frames, int clientID)
    {

        if (frames.Count > 1)
        {
            Debug.Log("Error:AskFrame count>1");
        }
        if (frames.Count <= 0)
        {
            Debug.Log("Error:AskFrame count<=0");
            return;

        }

        int startFrame = frames[0];

        Dictionary<int, List<Frame>> replyFrames = room.GetFrame(startFrame);
      

        NetworkMsg msg = new ReplyAskFrame(replyFrames);
        networkManager.SendDataTo(clientID, msg);

    }

}

