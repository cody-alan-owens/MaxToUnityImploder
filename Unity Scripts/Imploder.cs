using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace PlanImploder
{
    public class Imploder : MonoBehaviour
    {
        public TextAsset PointsCSV;
        public TextAsset BasepointsCSV;
        public TextAsset OptionpointsCSV;
        public TextAsset TextpointsCSV;
        private ZoneBuilder ZoneBuilder;
        Dictionary<HierarchyZone, GameObject> ZoneObjects;

        void Start()
        {

        }

        void Update()
        {

        }

        public void CreateZones()
        {
            this.ZoneBuilder = new ZoneBuilder(ZoneBuilder.PointsInputFormat.MAX);
            List<HierarchyZone> zones = this.ZoneBuilder.GetZones(PointsCSV.text, this.BasepointsCSV.text, this.OptionpointsCSV.text, this.TextpointsCSV.text);
            ZoneObjects = new Dictionary<HierarchyZone, GameObject>();
            for(int i = 0; i < zones.Count; i++)
            {
                ZoneObjects.Add(zones[i], InstantiateGameObjects(zones[i]).gameObject);
            }
            foreach(KeyValuePair<HierarchyZone, GameObject> zoneObject in ZoneObjects)
            {
                SetParents(zoneObject.Key, zoneObject.Value.transform);
            }
        }

        private Transform InstantiateGameObjects(HierarchyZone zone)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            float x = zone.UpperLeft.X;
            float y = zone.UpperLeft.Y;
            float z = zone.UpperLeft.Z;
            if (float.IsInfinity(Mathf.Abs(x)))
            {
                x = 0;
            }
            if (float.IsInfinity(Mathf.Abs(y)))
            {
                y = 0;
            }
            if (float.IsInfinity(Mathf.Abs(z)))
            {
                z = 0;
            }
            cube.transform.position = (new Vector3(x,y,z));
            cube.transform.localScale = (new Vector3(zone.GetLength(), zone.GetWidth(), 300));
            cube.name = zone.Label;
            return cube.transform;
        }

        private Transform SetParents(HierarchyZone zone, Transform zoneTransform)
        {
            if (zone.Parent != null)
            {
                zoneTransform.SetParent(ZoneObjects[zone.Parent].transform, true);
            }
            return zoneTransform;
        }
    }
}