using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using Triheroes.Code;
using UnityEngine;

public class s_bullet_1d : action
{
    [Depend]
    ss_2d s2d;

    [Depend]
    s_skin ss;

    public bool FromPlayer = false;

    float radius;
    float speed;
    float Damage;
    int sfx;

    Vector2 dir;

    public class package : PreBlock.Package<s_bullet_1d>
    {
        public package(float speed, float radius, float Damage, int sfx)
        {
            o.radius = radius;
            o.speed = speed;
            o.Damage = Damage;
            o.sfx = sfx;
        }
    }

    public void Fire(float direction)
    {
        s2d.SetDirection(direction);
        dir = new Vector2(direction, 0);
        Stage.Start(this);
    }

    public void Fire(Vector3 direction)
    {
        dir = direction.normalized;
        Stage.Start(this);
    }

    protected override void Start()
    {
        a_sfx.Play (sfx, ss.Coord.position);
        ShootCast();
    }

    protected override void Step()
    {
        ShootCast();
    }

    void ShootCast()
    {
        float spd = speed * Time.deltaTime;

        if (
            Physics.Raycast(ss.Coord.position, dir, out RaycastHit hit, spd * 1.2f , Vecteur.SolidHitBox) ||
            Physics.Raycast(ss.Coord.position + new Vector3(0, radius, 0), dir, out hit, spd * 1.2f, Vecteur.CHARACTER)
            ||
            Physics.Raycast(ss.Coord.position - new Vector3(0, radius, 0), dir, out hit, spd * 1.2f , Vecteur.CHARACTER)
            )
        {
            if (FromPlayer)
                if (HitBoxList.Contains(hit.collider.id()) && !HitBoxList.IsPlayer(hit.collider.id()))
                {
                    HitBoxList.Hit(hit.collider.id(), Damage);
                    b.Destroy();
                }

            if (!FromPlayer)
                if (HitBoxList.Contains(hit.collider.id()) && HitBoxList.IsPlayer(hit.collider.id()))
                {
                    HitBoxList.Hit(hit.collider.id(), Damage);
                    b.Destroy();
                }

            if (hit.collider.gameObject.layer == Vecteur.SOLID)
            {
                b.Destroy();
            }
        }

        ss.Coord.position += new Vector3(dir.x, dir.y, 0) * spd;
    }
}