using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] private float rotationAngle = -90;

    public Action OnClick;

   public void Click()
    {
        OnClick?.Invoke();

        transform.Rotate(Vector3.forward, rotationAngle);
    }

}
