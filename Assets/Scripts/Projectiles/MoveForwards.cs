using UnityEngine;

public class MoveForwards : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float projSpeed = 40f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward *  projSpeed * Time.deltaTime);
    }
}
