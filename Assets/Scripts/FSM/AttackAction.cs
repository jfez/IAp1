﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        if (controller.CheckIfCountDownElapsed(controller.enemyStats.attackRate) && Vector2.Distance(GameObject.FindGameObjectWithTag("Player").gameObject.transform.position, controller.eyes.position) < controller.enemyStats.attackRange)
        {
            GameObject instance = Instantiate(controller.bullet, controller.transform.position + new Vector3(0,0,-1), controller.transform.rotation);
            Rigidbody rigidbody = instance.GetComponent<Rigidbody>();
            rigidbody.AddRelativeForce((Vector3.up + new Vector3(-0.25f, 0f, 0f)).normalized * controller.enemyStats.attackForce);
            //rigidbody.AddRelativeTorque(Vector3.forward * controller.enemyStats.attackForce);

            instance = Instantiate(controller.bullet, controller.transform.position + new Vector3(0,0,-1), controller.transform.rotation);
            rigidbody = instance.GetComponent<Rigidbody>();
            rigidbody.AddRelativeForce(Vector3.up * controller.enemyStats.attackForce * 0.9f);
            //rigidbody.AddRelativeTorque(Vector3.forward * controller.enemyStats.attackForce * 0.9f);

            instance = Instantiate(controller.bullet, controller.transform.position + new Vector3(0,0,-1), controller.transform.rotation);
            rigidbody = instance.GetComponent<Rigidbody>();
            rigidbody.AddRelativeForce((Vector3.up + new Vector3(0.25f, 0f, 0f)).normalized * controller.enemyStats.attackForce * 0.8f);
            //rigidbody.AddRelativeTorque(Vector3.forward * controller.enemyStats.attackForce * 0.8f);

            controller.stateTimeElapsed = 0f;
        }
    }
}