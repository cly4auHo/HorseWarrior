using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IncomeLabel : MonoBehaviour
{
    [SerializeField] private float timeToLive = 1;
    [SerializeField] private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDesctruct());
    }

    private IEnumerator SelfDesctruct()
    {
       yield return new WaitForSecondsRealtime(timeToLive);

       Destroy(gameObject);
    }

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
