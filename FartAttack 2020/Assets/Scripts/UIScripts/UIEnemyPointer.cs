using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyPointer : MonoBehaviour
{
    public Transform enemyTrans;
    Transform playerTrans;
    public Transform enemyHealthBarTrans;
    Vector3 enemyPos;
    Vector3 playerPos;
    Vector3 enemyHealthBarPos;

    RectTransform pointerTransform;
    Vector3 pointerConstraintScreenPos;

    Vector3 enemyPosScreenPoint;
    Vector3 enemyHealthPosScreenPoint;

    float borderX;
    float borderY;

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        borderX = Screen.width / 40;
        borderY = Screen.height / 10;

        pointerTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }

    void Update()
    {
        enemyPos = enemyTrans.position;
        playerPos = playerTrans.position;
        enemyHealthBarPos = enemyHealthBarTrans.position;

        RotatePointer();
        CheckIfOffScreen();
    }

    void CheckIfOffScreen()
    {
        enemyPosScreenPoint = Camera.main.WorldToScreenPoint(enemyPos);
        enemyHealthPosScreenPoint = Camera.main.WorldToScreenPoint(enemyHealthBarPos);
        bool offScreen = enemyHealthPosScreenPoint.x <= 0 || enemyHealthPosScreenPoint.x >= Screen.width || enemyHealthPosScreenPoint.y <= 0 || enemyHealthPosScreenPoint.y >= Screen.height;

        if (offScreen)
        {
            pointerTransform.gameObject.SetActive(true);

            pointerConstraintScreenPos = enemyPosScreenPoint;
            if (pointerConstraintScreenPos.x <= borderX) pointerConstraintScreenPos.x = borderX;
            if (pointerConstraintScreenPos.x >= Screen.width - borderX) pointerConstraintScreenPos.x = Screen.width - borderX;
            if (pointerConstraintScreenPos.y <= borderY) pointerConstraintScreenPos.y = borderY + 100f;
            if (pointerConstraintScreenPos.y >= Screen.height - borderY) pointerConstraintScreenPos.y = Screen.height - borderY;

            pointerTransform.position = pointerConstraintScreenPos;
        }
        else
        {
            pointerTransform.gameObject.SetActive(false);
        }
    }

    void RotatePointer()
    {
        Vector3 dir = (enemyPos - playerPos);
        float angle = Vector3.Angle(dir, transform.forward);

        if (playerPos.x > enemyPos.x)
        {
            pointerTransform.localEulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            pointerTransform.localEulerAngles = new Vector3(0, 0, -angle);
        }
    }
}
