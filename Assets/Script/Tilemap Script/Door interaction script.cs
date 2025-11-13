using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Doorinteractionscript : MonoBehaviour
{
    GameObject player;
    GameObject doorxy;
    GameObject house;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("testPlayer");
        doorxy = GameObject.Find("door trigger");
        house = GameObject.Find("Idoor trigger");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.transform.localPosition+":"+ house.transform.localPosition);
        if (Vector3.Distance(player.transform.localPosition, doorxy.transform.localPosition)<2)
        {
            player.transform.localPosition = house.transform.position;
            Debug.Log(player.transform.localPosition+":"+ house.transform.localPosition);
        }
        // Debug.Log(player.transform.localPosition+":"+ doorxy.transform.localPosition);

    }
}
