using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agent
{
    public class CameraFlockFollow: MonoBehaviour
    {
        public AgentController controller;
        private List <GameObject> agents = new List<GameObject>();
        public float offset;
        public float turnSpeed;
        Vector3 average;

        private void Update()
        {
            average = Vector3.zero;
            agents = controller.agents;
            foreach(GameObject agent in agents)
            {
                average += agent.transform.position;
            }

            average = average / agents.Count;
            transform.position = Vector3.Lerp(transform.position, average + (Vector3.up * offset),Time.deltaTime * 1f);
            Vector3 direction = average - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion limitedRotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            transform.rotation = limitedRotation;
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.black;
        //    Gizmos.DrawCube(average, Vector3.one);
        //}


    }
}
