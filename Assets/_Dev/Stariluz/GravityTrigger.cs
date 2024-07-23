using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stariluz
{
    public class GravityTrigger : MonoBehaviour
    {
        [SerializeField] private float gravityChangeDegree = 0;

        public float GetGravityChangeDegree()
        {
            return gravityChangeDegree;
        }
    }

}
