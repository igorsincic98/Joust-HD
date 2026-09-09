using UnityEngine;

public class Wrapping : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 6.3f)
        {
            transform.position = new Vector3(-6.29999f, transform.position.y, transform.position.z) ;
        }
        if (transform.position.x <= -6.3f)
        {
            transform.position = new Vector3(6.29999f, transform.position.y, transform.position.z) ;
        }
    }
}
