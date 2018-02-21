using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agent
{
    public class AgentController: MonoBehaviour
    {

        [SerializeField]
        private int _numberOfAgents;
        [SerializeField]
        private float _spawnRadius;
        [SerializeField]
        private float _spawnRandRotation;
        [SerializeField]
        private GameObject _agentPrefab;


        public float _agentVelocity;
        public float _agentRotateSpeed;
        public float _distFromEachOther;

        public float _controllerMoveSpeed;
        public float _controllerAngle;
        public LayerMask _physicsLayer;
        public float dist;
        public Vector3 _controllerRotateSpeed;
        public float birdAngle;

        public List<GameObject> agents = new List<GameObject>();

        private void Start()
        {
            Spawn(_numberOfAgents);
        }

        private void Update()
        {
            birdAngle += Random.Range(0f, 2f) * Time.deltaTime;
            _distFromEachOther += Mathf.Sin(birdAngle) * 0.1f;
            if(_distFromEachOther < 0)
            {
                _distFromEachOther = 0;
            }
            Move();
        }

        private void Spawn(int numberOfAgents )
        {
            for(int i = 0; i < numberOfAgents; i++)
            {
                
                GameObject go = Instantiate(_agentPrefab, 
                    transform.position + Random.insideUnitSphere * _spawnRadius,
                    Quaternion.Euler(0f, 0f, Random.Range(-_spawnRandRotation, _spawnRandRotation)));
                go.GetComponent<AgentBehaviour>()._controller = this;
                agents.Add(go);
            }
        }

        private void Move()
        {
            transform.Rotate(_controllerRotateSpeed * Time.deltaTime);
            _controllerAngle += _controllerMoveSpeed * Time.deltaTime;
            float x = dist * Mathf.Sin(_controllerAngle);
            float y = dist *Mathf.Cos(_controllerAngle);
            transform.localPosition = new Vector3(x, y, transform.position.z + _controllerMoveSpeed * Time.deltaTime);
        }

        public void SpawnSingle()
        {
            GameObject go = Instantiate(_agentPrefab,
                    transform.position + Random.insideUnitSphere * _spawnRadius,
                    Quaternion.Euler(0f, 0f, Random.Range(-_spawnRandRotation, _spawnRandRotation)));
            go.GetComponent<AgentBehaviour>()._controller = this;
            agents.Add(go);
        }

    }
}
