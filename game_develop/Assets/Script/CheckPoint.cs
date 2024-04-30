using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject FpsController;
    [SerializeField] List<GameObject> CheckPoints;
    [SerializeField] Vector3 vectorPoint;
    [SerializeField] float dead;

    void Update()
    {
        if (FpsController.transform.position.y < -dead)
        {
            FpsController.transform.position = vectorPoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        vectorPoint = FpsController.transform.position;
        Destroy(other.gameObject);
    }
}
