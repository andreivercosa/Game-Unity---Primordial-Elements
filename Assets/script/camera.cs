using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private Vector2 velocity; //velocidade da camera
    private Transform player;//captuar a localização do player

    public float smoothTimeX; // definir posição em X
    public float smoothTimeY;// definir posição em Y

    private float shakeTime; //sacudida da camer
    private float shakeAmount;//variação da sacudida


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();


    }

    void Update()
    {
        if (shakeTime >= 0f)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;//movimentação randomica da camera
            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
            shakeTime -= Time.deltaTime;
        }

        
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, smoothTimeX);//definir a posição da camera em X
            float posY = Mathf.SmoothDamp(transform.position.y, player.position.y, ref velocity.y, smoothTimeY);//definir a posição da camera em Y

            transform.position = new Vector3(posX, posY, transform.position.z);//passar para a camera a localização onde eka deve estar
        }
    }

    public void ShakeCamera(float shakeTime, float shakeAmount)
    {
        this.shakeTime = shakeTime;
        this.shakeAmount = shakeAmount;
    }
}