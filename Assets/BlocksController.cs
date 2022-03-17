using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 移動
        if( Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.gameObject.transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.gameObject.transform.position += new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.gameObject.transform.position += new Vector3(0, -1, 0);
        }

        // 回転
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.gameObject.transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90);
            foreach (Transform childBlocks in transform) {
                childBlocks.RotateAround(transform.position, new Vector3(0, 0, 1), -90);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            this.gameObject.transform.position += new Vector3(-1, 0, 0);

        }

    }
}
