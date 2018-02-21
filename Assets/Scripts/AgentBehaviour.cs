using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agent
{
    public class AgentBehaviour: MonoBehaviour
    {

        public AgentController _controller;

        public Vector3 GetSeparationVector(Transform target)
        {
            var diff = transform.position - target.transform.position;
            var diffLen = diff.magnitude;
            var scaler = Mathf.Clamp01(1.0f - diffLen / _controller._distFromEachOther);
            return diff * (scaler / diffLen);
        }

        // Update is called once per frame
        private void Update()
        {
            Vector3 currentPos = transform.position;
            Quaternion currentRotation = transform.rotation;

            float velocity = _controller._agentVelocity;

            Vector3 separation = Vector3.zero;
            Vector3 aligment = _controller.transform.forward;
            Vector3 cohesion = _controller.transform.position;

            Collider[] birds = Physics.OverlapSphere(transform.position, _controller._distFromEachOther, _controller._physicsLayer);

            foreach (Collider agent in birds )
            {
                if(agent.gameObject == this.gameObject)
                    continue;
                
                separation += GetSeparationVector(agent.transform);
                aligment += agent.transform.forward;
                cohesion += agent.transform.position;
            }

            float average = 1f / birds.Length;
            aligment *= average;
            cohesion *= average;
            cohesion = (cohesion - currentPos).normalized;

            Vector3 newDirection = separation + aligment + cohesion;

            Quaternion newRotation = Quaternion.FromToRotation(Vector3.forward, newDirection.normalized);
            if(newRotation != currentRotation)
            {
                transform.rotation = Quaternion.Slerp(newRotation,currentRotation, Mathf.Exp(-_controller._agentRotateSpeed * Time.deltaTime));
            }

            transform.position = currentPos + transform.forward * (velocity * Time.deltaTime);
        }
    }
}
