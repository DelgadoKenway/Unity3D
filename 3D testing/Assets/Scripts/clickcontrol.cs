using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickcontrol : MonoBehaviour
{
    public static string nameofobj;
    public GameObject objnametext;
    public Transform objnametextPos;
    public Transform sucessclick;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()

    {
        nameofobj = gameObject.name;
        //Debug.Log (nameofobj);
        Destroy(gameObject);
        Destroy(objnametext);
        trackingclicks.totalclicks = 0;
        //Instantiate(sucessclick, objnametextPos.position, sucessclick.rotation);
    }


}
