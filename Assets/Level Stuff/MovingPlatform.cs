using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform Parent;
    [SerializeField] private Transform[] stations;
    int stationIndex;
    public float Speed;
    private Transform playerParent;


 
    void Start()
    {
        if (stations == null || stations.Length == 0)
        {
            stations = Parent.GetComponentsInChildren<Transform>();
            stations = Array.FindAll(stations, t => t != this.transform && t != Parent);


        }
        stationIndex = 0;
        playerParent = GameObject.FindGameObjectWithTag("Level").transform;
    }

  
    void FixedUpdate()
    {
        if (stations == null || stations.Length == 0)
        {
            return;
        }

        Transform aim = stations[stationIndex];

        transform.position = Vector2.MoveTowards(transform.position, aim.position, Speed * Time.deltaTime);

        if ((Vector2.Distance(transform.position, aim.position) < .1f)) stationIndex = (stationIndex + 1) % stations.Length;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {

            collision.transform.SetParent(this.transform);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            collision.transform.SetParent(playerParent);


        }
    }
    private void OnDrawGizmos()
    {
        if (stations == null || stations.Length < 2)
        {
            return;
        }





        for (int i = 0; i < stations.Length; i++)
        {
            Gizmos.DrawLine(stations[i].position, stations[(i + 1) % stations.Length].position);
        }




    }
}