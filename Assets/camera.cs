using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + new Vector3(0.0f, 0.0f, -10f);
    }
}
