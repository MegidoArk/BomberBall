using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 0, 0);

    void Update()
    {
        // Rotate the object every frame
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}