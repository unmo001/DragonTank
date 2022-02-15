using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public GameObject turret;
    public GameObject cam;
    Quaternion cameraRot, characterRot, turretRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;
    bool cursorLock = true;

    float maxAngleX = 0;
    float minAngleX = -30;
    float speedX = 0.5f;

    //変数の宣言(角度の制限用)
    float minX = -90f, maxX = 90f;

    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        turretRot = turret.gameObject.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);
        turretRot *= Quaternion.Euler(-yRot, 0, 0);
        cameraRot = ClampRotation(cameraRot);

        Vector3 turretXRot = turretRot.eulerAngles;
        float turretXRot_X = turretXRot.x;

       
        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;
        turret.gameObject.transform.localRotation = turretRot;
   
        
        UpdateCursorLock();
    }

    

    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }


}