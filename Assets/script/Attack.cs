using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float timeDestroy;
    public float speed;
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
