using System.Collections;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

 public class r_fall : reflexion, IMotorHandler
    {
        [Depend]
        d_ground_data dgd;

        [Depend]
        s_gravity_ccc sgc;

        [Depend]
        ac_fall af;

        [Depend]
        s_motor sm;

        public void OnMotorEnd(motor m)
        {}

        protected override void Reflex()
        {
            if (!dgd.onGround && sgc.gravity < 0)
                sm.SetState (af, this);
        }
    }