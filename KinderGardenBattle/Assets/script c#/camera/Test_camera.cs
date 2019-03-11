using UnityEngine;

public class Test_camera : MonoBehaviour
{
    private float panSpeed = 20f;
    private float panBorderThickness = 10f;
    private float scrollspeed = 2000f;
    public Vector2 panlimit;
    public float heightlimit;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit ray = new RaycastHit();
        Vector3 target = ray.transform.position;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {

            pos.x -= panSpeed * Time.deltaTime;
            pos.z += panSpeed * Time.deltaTime;
        }
        transform.position = pos;

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
            pos.z -= panSpeed * Time.deltaTime;
        }
        transform.position = pos;

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
            pos.x += panSpeed * Time.deltaTime;
        }
        transform.position = pos;

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
            pos.z += panSpeed * Time.deltaTime;
        }

        pos.y -= scroll * scrollspeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, 20, 20+heightlimit);
        pos.x = Mathf.Clamp(pos.x, -panlimit.x, panlimit.x);
        pos.z = Mathf.Clamp(pos.z, -panlimit.y, panlimit.y);

        transform.position = pos;
    }
}
