using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target; 

    // Start is called before the first frame update
    void Start()
    {
        //找到玩家控制器组件的Transform组件
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    
    void LateUpdate()
    {
        //改变相机位置
        this.transform.position = new Vector3(target.position.x, target.position.y, this.transform.position.z);
    }
}
