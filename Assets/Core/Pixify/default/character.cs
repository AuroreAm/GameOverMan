using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class character : pix
    {
        public GameObject gameObject {private set; get;}
        public Transform Coord {private set; get;}
        public Vector3 position 
        {
            get
            {
                if (Coord) _position = Coord.position;
                return _position;
            }
        }

        Vector3 _position;

        public class package : PreBlock.Package <character>
        {
            public package ( GameObject g )
            {     
                o.gameObject = g;
                o.Coord = g.transform;
            }
        }

        protected override void Destroy()
        {
            gameObject.SetActive (false);
        }
    }
}