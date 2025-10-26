using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    public void GetHPAction(float damage)
    {
        
    }
}
