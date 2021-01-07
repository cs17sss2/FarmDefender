using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdDieAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
