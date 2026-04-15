using UnityEngine;

public class StarRotator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 60) * Time.deltaTime);
    }
}
