using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] float minionRate = 1;
    public Action<Vector3> OnMinion;
    private Vector3 labelPositon;

    void Start()
    {
        labelPositon = GetComponent<RectTransform>().position;
        StartCoroutine(SlowUpdate());
    }

    private IEnumerator SlowUpdate()
    {
        while (true)
        {
            OnMinion?.Invoke(labelPositon);
            yield return new WaitForSecondsRealtime(minionRate);
            transform.Rotate(Vector3.forward, 90);
        }
    }
}
