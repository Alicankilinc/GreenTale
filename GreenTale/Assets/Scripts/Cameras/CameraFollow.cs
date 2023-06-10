using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float xMargin = 1f;
    public float yMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;


    private Transform m_Player;
    // Start is called before the first frame update
    private void Awake()
    {
        m_Player = GameObject.Find("Player").transform;
    }

    private bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x-m_Player.position.x)>xMargin;
    }
    private bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y-m_Player.position.y)>yMargin;
    }

    private void TrackPlayer()
    {
        float targetX= transform.position.x;
        float targetY= transform.position.y;
        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, m_Player.position.x, xSmooth * Time.deltaTime);
        }
        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, m_Player.position.y, ySmooth * Time.deltaTime);
        }


        targetX=Mathf.Clamp(targetX,minXAndY.x, maxXAndY.x);
        targetY=Mathf.Clamp(targetY,minXAndY.y, maxXAndY.y);

        transform.position= new Vector3(targetX,targetY,transform.position.z);
    }

}
