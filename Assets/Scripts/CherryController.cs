using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab;
    public float moveSpeed = 5f;
    private Camera cam;
    private Vector3 centre;

    void Start()
    {
        cam = Camera.main;
        centre = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane));
        centre.z = 0;

        StartCoroutine(CherrySpawn());
    }

    IEnumerator CherrySpawn(){
        while (true){
            yield return new WaitForSeconds(10f);

            //random side between numbers 0-4
            int randomSide = Random.Range(0, 4);
            Vector3 startpos = RandStartPos(randomSide);
            Vector3 endpos = EndPos(startpos);

            //make cherry + start coroutine for it
            GameObject cherry = Instantiate(cherryPrefab, startpos, Quaternion.identity);
            StartCoroutine(CherryMove(cherry, startpos, endpos));
        }
    }

    Vector3 RandStartPos(int side){
        //cases for chosen spawn side
        switch (side){
            case 0:
                return cam.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(0f, 1f), cam.nearClipPlane)); //left
            case 1:
                return cam.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(0f, 1f), cam.nearClipPlane)); //right
            case 2:
                return cam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), 1.1f, cam.nearClipPlane)); //top
            case 3:
                return cam.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), -0.1f, cam.nearClipPlane)); //bottom
            default:
                return Vector3.zero;
        }
    }

    Vector3 EndPos(Vector3 startpos){
        Vector3 direction = (centre - startpos).normalized;
        float disToEdge = Mathf.Max(Mathf.Abs(cam.orthographicSize / direction.y), Mathf.Abs(cam.orthographicSize * cam.aspect / direction.x));

        return startpos + direction * disToEdge * 1.5f;
    }

    IEnumerator CherryMove(GameObject cherry, Vector3 startpos, Vector3 endpos){
        while (cherry != null){
            cherry.transform.position = Vector3.MoveTowards(cherry.transform.position, endpos, moveSpeed * Time.deltaTime);

            /*Debug.Log("cherry pos: " + cherry.transform.position);
            Debug.Log("end pos: " + endpos);*/

            //check if the cherry has reached the end position
            if (Vector3.Distance(cherry.transform.position, endpos) < 0.1f){
                Destroy(cherry);
                yield break;
            }

            yield return null;
        }
    }
}
