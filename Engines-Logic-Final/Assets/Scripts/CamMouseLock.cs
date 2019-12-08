using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouseLock : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0F;
    public float smoothing = 2.0F;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //the character is a parent to the camera
        player = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        //get x and y axis for the mouse movement
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
        //you can only look as far as straight up and straight down along the Y axis
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        //make the character face the direction you are looking in
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
    }
}