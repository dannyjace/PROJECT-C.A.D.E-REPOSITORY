using UnityEngine;
using UnityEngine.UI;

public class Drone : EnemyAI
{
    //private Vector3 playerDir;


    public override void Movement(Vector3 playerDir)
    {
        //playerDir = GameManager.instance.player.transform.position - transform.position;
        agent.SetDestination(GameManager.instance.player.transform.position + Vector3.up);
        if (agent.remainingDistance <= agent.stoppingDistance) { FaceTarget(playerDir); }
    }


    void FaceTarget(Vector3 playerDir)
    {
        //Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, transform.position.y, playerDir.z));
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, playerDir.y, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(transform.rotation.x, rot.y, transform.rotation.z, transform.rotation.w), Time.deltaTime * turnSpeed);
    }
}
