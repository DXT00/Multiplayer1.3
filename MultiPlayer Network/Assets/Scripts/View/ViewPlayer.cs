using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ViewPlayer : MonoBehaviour
{
    GameObject player_instance;
    public int connectID;//与Player的connectID相同；


    float speed = 50f;

    public GameObject BulletPrefabs;
    public Transform bulletSpawn;

    Rigidbody PlayerRigidbody;
    List<CustomSyncMsg> msg_list;

    int touchTerrianMask;
    float max_y = 100f;
    float last_y = 2f;
    Transform Transform;
    public void Start()
    {
        msg_list = new List<CustomSyncMsg>();
        PlayerRigidbody = GetComponent<Rigidbody>();
        touchTerrianMask = LayerMask.GetMask("touchTerrian");
        Transform = gameObject.GetComponent<Transform>();
    }


    public void Update()
    {



    }

    public List<CustomSyncMsg> get_local_input()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Move(x, z);//不移动，等server发 replyframes

        float my = move_on_the_ground();
      
        float y =  my;//这里指的y坐标,<100f时还没考虑（Ray 没有hit）


        //if (Input.GetButton("Fire1"))
        //    CmdShooting();

        msg_list.Clear();
        CustomSyncMsg input_msg = new InputMessage(connectID, new Vector3(x, y, z));
        msg_list.Add(input_msg);
        return msg_list;
    }
    public void Move(float horizontal, float vertical, float y)
    {
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = movement.normalized * speed * 0.025f;// * Time.deltaTime;注意不能誠意deltaTime,

       
        

        Debug.Log("get_Rigidbody().position.x = " + PlayerRigidbody.position.x + "get_Rigidbody().position.y = " + PlayerRigidbody.position.y);

       

        Transform.position = new Vector3(Transform.position.x, y, Transform.position.z);
        PlayerRigidbody.MovePosition(Transform.position + movement);
        Debug.Log("PlayerRigidbody.position + movement = " + (PlayerRigidbody.position + movement).x + "PlayerRigidbody.position + movement " + (PlayerRigidbody.position + movement).y);
    }



    void CmdShooting()
    {

        //GameObject bullet = Instantiate(BulletPrefabs, bulletSpawn.position, bulletSpawn.rotation);
        //bullet.GetComponent<Rigidbody>().velocity = transform.forward * 6f;


        //NetworkServer.Spawn(bullet);

        //Destroy(bullet, 2f);

    }


    public float move_on_the_ground()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, max_y, touchTerrianMask))
        {
            return hit.point.y + 1f;
        }
        else
            return -101;
    }


    public void set_player_instance(GameObject instance)
    {
        player_instance = instance;
    }
    public float get_speed()
    {
        return speed;
    }
    public Rigidbody get_Rigidbody()
    {
        return PlayerRigidbody;
    }
}

