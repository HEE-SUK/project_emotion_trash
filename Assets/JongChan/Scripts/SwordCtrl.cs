using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCtrl : MonoBehaviour
{
    BoxCollider2D bc;

    enum Wepon
    {
        spir = 0,
        longspir,
        dagger,
        longsword,
        excalibur,
        poopBranch,
        fish
    }

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mPosition = Input.mousePosition;
        Vector3 oPosition = transform.position;

        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

        if (Input.GetMouseButtonDown(0))
        {
            bc.enabled = true;

            Invoke("BoxColliderOn", 0.1f);
        }
    }

    void BoxColliderOn()
    {
        bc.enabled = false;
        //bc.size.x = 
    }
}
