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


    float z = 0;
    float x = 0;


 float mouse_sensitive = 30f;
    float dist_sensitive = 20f;
    float rot_x = 0;
    float rot_y = 0;

    int key_x1 = 0;
    int key_x2 = 0;
    Transform Transform;

    public CameraFollow camera;
    public void Start()
    {
        msg_list = new List<CustomSyncMsg>();
        PlayerRigidbody = GetComponent<Rigidbody>();
        touchTerrianMask = LayerMask.GetMask("touchTerrian");
        Transform = gameObject.GetComponent<Transform>();
        PlayerRigidbody.freezeRotation = true;
    }


    public void FixedUpdate()
    {

        //  float _mouseX = Input.GetAxis("Mouse X");
        //  float _mouseY = Input.GetAxis("Mouse Y");


        x += Input.GetAxis("Horizontal");
        z += Input.GetAxis("Vertical");




        if (Input.GetKeyDown(KeyCode.Q))
        {
            key_x1++;
            rot_x = 0;
           // rot_y += _mouseY * mouse_sensitive;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            key_x2++;
            rot_x = 0;
            // rot_y += _mouseY * mouse_sensitive;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            rot_x += key_x1 * (-mouse_sensitive);
            key_x1 = 0;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
          
            rot_x+= key_x2*(mouse_sensitive);
            key_x2 = 0;
            // rot_y += _mouseY * mouse_sensitive;
        }

    }

    public List<CustomSyncMsg> get_local_input()
    {
      
        //Move(x, z);//不移动，等server发 replyframes

        float my = move_on_the_ground();
      
        float y =  my;//这里指的y坐标,<100f时还没考虑（Ray 没有hit）


        //if (Input.GetButton("Fire1"))
        //    CmdShooting();

        msg_list.Clear();
        if (x != 0 || z != 0)
        {
            CustomSyncMsg input_msg = new InputMessage(connectID, new Vector3(x, y, z));
            msg_list.Add(input_msg);
        }
       
        x = 0;
        z = 0;

       


     

        
        if (rot_x != 0f || rot_y != 0f)
        {
            CustomSyncMsg rot_msg = new RotateMessage(connectID, new Vector2(rot_x, rot_y));
            msg_list.Add(rot_msg);
            rot_x = 0;
            rot_y = 0;
        }

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

    public void Rotate(float delta_x, float delta_y)
    {
        //现在只有绕自己的y轴旋转
        Transform.Rotate(transform.up, delta_x, Space.Self);
        Tool.Print("......................Rotating .........delta_x = " + delta_x.ToString());
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

    public void bind_cameraFollow(CameraFollow cameraFollow)
    {
        this.camera = cameraFollow;
    }
}

