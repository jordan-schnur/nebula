using NebulaModel.Packets.Players;
using System.Collections.Generic;
using UnityEngine;

namespace NebulaWorld.MonoBehaviours.Local
{
    public class Pinging : MonoBehaviour
    {
        public const float PING_LIVE_TIME = 10;

        Dictionary<float, GameObject> pings = new Dictionary<float, GameObject>();
        private void Update()
        {
            if(Input.GetMouseButtonDown(2))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;

                    Debug.Log("Object hit");

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    cube.transform.position = hit.point;

                    cube.name = "Cube" + Time.frameCount;
                    pings.Add(Time.time, cube);
                }
            }
        }

        private void DeleteOldPings()
        {
            float currentTime = Time.time;
            foreach (KeyValuePair<float, GameObject> entry in pings)
            {
                if((currentTime - entry.Key) > PING_LIVE_TIME)
                {
                    GameObject.Destroy(entry.Value);
                }
            }
        }
    }
}
