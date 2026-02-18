using System.Collections;
using System.Collections.Generic;
using Pixify;

public class d_actor : pix
{
        [Depend]
        public character c;
        [Depend]
        public s_skin ss;
        [Depend]
        public d_dimension dd;
        public bool IsPlayer { private set; get; }

        public override void Create()
        {
            c.gameObject.layer = Vecteur.CHARACTER;
        }

        public class package : PreBlock.Package <d_actor>
        {
            public package ( bool isPlayer )
            {
                o.IsPlayer = isPlayer;

                if ( isPlayer )
                play.o.SetPlayer (o);
            }
        }
}
