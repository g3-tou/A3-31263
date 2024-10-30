using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherry;
    private Camera sceneCamera;
    public float speed = 5.0f;
    Vector3 startpos;
    Vector3 endpos;
    private GameObject cCherry;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        sceneCamera = Camera.main;

        startpos = sceneCamera.ViewportToWorldPoint(new Vector3(-0.1f, 0.5f,sceneCamera.nearClipPlane));
        startpos.z = 0;

        endpos = sceneCamera.ViewportToWorldPoint(new Vector3(1.1f, 0.5f,sceneCamera.nearClipPlane));
        endpos.z = 0;

        StartCoroutine(MakeCherry());
    }

    IEnumerator MakeCherry(){
        while (true)
        {
            yield return new WaitForSeconds(10f);

            cCherry = Instantiate(cherry, startpos, Quaternion.identity) as GameObject;
            StartCoroutine(MoveObject(cCherry));
            
        }
    }

    IEnumerator MoveObject(GameObject obj){
        isMoving = true;
        while(obj != null && Vector3.Distance(obj.transform.position, endpos) > 0.1f){
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, endpos, speed * Time.deltaTime);
            yield return null;
        }
        if(obj != null){
            Destroy(obj);
            //cCherry = null;
        }
        isMoving = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}