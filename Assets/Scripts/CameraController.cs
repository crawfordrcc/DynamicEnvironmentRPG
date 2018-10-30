using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public static CameraController instance = null;
    public GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    private Vector3 initialPosition;
    private Vector3 normalPosition;
    private Vector3 normalOffset;

    // Use this for initialization
    void Awake(){
        if (instance == null)
            instance = this;
        else if (instance != this)
           Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start () 
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        initialPosition = transform.position;
        //offset = transform.position - player.transform.position;
        offset = transform.position;
        //Debug.Log(player.transform.position);
    }
    
    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }

    public void BattleCameraStart ()
    {
        normalOffset = offset;
        normalPosition = transform.position;
        transform.position = new Vector3(0f,0f,-10.0f);
        offset = -player.transform.position + new Vector3(0f, 0f, -10.0f);
    }

    public void ResetCamera()
    {
        offset = normalOffset;
        transform.position = normalPosition;
    }

    public void CenterCameraOnObject (Transform tform, Vector3 ofst = default(Vector3)){
        normalOffset = offset;
        offset = initialPosition + tform.position - player.transform.position + ofst;
    }

}
