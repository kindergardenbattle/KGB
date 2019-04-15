using UnityEngine;

public class Test_camera : MonoBehaviour
{
    private float panSpeed = 5f;
    private float panBorderThickness = 10f;
    private float scrollspeed = 1500f;
    public Vector2 panlimit;
    public float heightlimit;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 target = new Vector3();
        Vector3 pos = transform.position;
        /*if (Input.GetMouseButtonUp(0) && Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Player"))
        {
            target = hit.transform.position;
            pos.x = target.x - 2;
            pos.z = target.z - 2;
            pos.y = target.y + 2;
        }*/
       // else
       // {


            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {

                pos.x += panSpeed * Time.deltaTime;
                pos.z += panSpeed * Time.deltaTime;
            }

            transform.position = pos;

            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                pos.x -= panSpeed * Time.deltaTime;
                pos.z += panSpeed * Time.deltaTime;
            }

            transform.position = pos;

            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                pos.z -= panSpeed * Time.deltaTime;
                pos.x -= panSpeed * Time.deltaTime;
            }

            transform.position = pos;

            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.x += panSpeed * Time.deltaTime;
                pos.z -= panSpeed * Time.deltaTime;
            }

            pos.x += scroll * scrollspeed * Time.deltaTime;
            pos.y -= scroll * scrollspeed * Time.deltaTime;
            pos.z += scroll * scrollspeed * Time.deltaTime;
      //  }

        pos.y = Mathf.Clamp(pos.y, 2, heightlimit);
        pos.x = Mathf.Clamp(pos.x, -panlimit.x, panlimit.x);
        pos.z = Mathf.Clamp(pos.z, -panlimit.y, panlimit.y);
    

        transform.position = pos;
    }
}
