using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public struct Constants
    {
        //How often do we want to update physics (60 times per second)
        public static float STEPVALUE = 1.0f / 60.0f;
        public static float G = -9.82f;

        public static float BOUNCEMULT = .40f; //ball keeps 40% of the velocity on a collision

        public static float AIRDRAG = 0.001f; // Drag in air ^2
        public static float GROUNDDRAG = 2f; // Drag on the ground
}