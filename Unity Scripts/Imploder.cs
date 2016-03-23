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
        private ZoneBuilder ZoneBuilder;

        void Start()
        {

        }

        void Update()
        {

        }

        public void CreateZones()
        {
            this.ZoneBuilder = new ZoneBuilder(ZoneBuilder.PointsInputFormat.MAX);
            List<Zone> zones = this.ZoneBuilder.GetZones(PointsCSV.text, this.BasepointsCSV.text, this.OptionpointsCSV.text);
            List<GameObject> zoneObjects = new List<GameObject>();
            foreach(Zone zone in zones)
            {
                Zone tempZone = zone;
                if (zone.Parent == null)
                {
                    InstantiateGameObjects(ref tempZone, this.transform);
                }
            }
        }

        private Transform InstantiateGameObjects(ref Zone zone, Transform parent)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = (new Vector3(zone.Rectangle.UpperLeft.X, zone.Rectangle.UpperLeft.Y, zone.Rectangle.UpperLeft.Z));
            cube.transform.localScale = (new Vector3(zone.Rectangle.GetLength(), zone.Rectangle.GetWidth(), 300));
            if(zone.Children!= null)
            {
                foreach (Zone child in zone.Children)
                {
                    Zone tempZone = child;
                    InstantiateGameObjects(ref tempZone, cube.transform).SetParent(cube.transform);
                }
            }            
            return cube.transform;
        }
    }
}