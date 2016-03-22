using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace PlanImploder
{
    public class Imploder : MonoBehaviour
    {
        public TextAsset PointsCSV;
        private ZoneBuilder ZoneBuilder;
        private List<Assignment> Assignments;

        void Start()
        {

        }

        void Update()
        {

        }

        private void CreateZones()
        {
            this.ZoneBuilder = new ZoneBuilder(PointsCSV.text, ZoneBuilder.PointsInputFormat.MAX);
        }
    }
}