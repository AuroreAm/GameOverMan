using Pixify;
using System.Collections.Generic;
using Pixify.Spirit;
using UnityEngine;
using Triheroes.Code;

public class pr_shoot : reflexion
{

    [Depend]
    d_gun dg;

    [Depend]
    ss_2d s2d;

    [Depend]
    character c;
    
    [Depend]
    ss_flash flash;
    
    float interval;

    const float charge1 = .5f, charge2 = 1f;

    CharacterAuthor Bullet0, Bullet1, Bullet2;
    
    AudioSource Ru;

    public class package : PreBlock.Package <pr_shoot>
    {
        public package ( CharacterAuthor Bullet0, CharacterAuthor Bullet1, CharacterAuthor Bullet2 )
        {
            o.Bullet0 = Bullet0;
            o.Bullet1 = Bullet1;
            o.Bullet2 = Bullet2;
        }
    }

    public override void Create()
    {
        Ru = c.gameObject.AddComponent <AudioSource> ();
        Ru.spatialBlend = 0;
        Ru.loop = true;
        Ru.volume = .5f;
        Ru.outputAudioMixerGroup = SFXMaster.MainMixer;
    }

    readonly term ch1 = new term ("ch1");
    readonly term ch2 = new term ("ch2");
    int ChargeSFX = 0;
    float charge;
    protected override void Reflex()
    {
        if ( Player.Shoot.OnActive )
        {
            charge = 0;
            Shoot ();
        }

        if ( Player.Shoot.Active )
            {
                charge += Time.deltaTime;

                if (charge > charge1 && ChargeSFX == 0)
                {
                    Ru.clip = SubResources<AudioClip>.q ( ch1 );
                    Ru.loop = true;
                    Ru.Play ();
                    ChargeSFX = 1;
                }

                if (charge > charge2 && ChargeSFX == 1)
                {
                    Ru.clip = SubResources<AudioClip>.q ( ch2 );
                    Ru.loop = true;
                    Ru.Play ();
                    ChargeSFX = 2;
                }

                if ( charge >= charge1 && interval <= 0)
                {
                    flash.Switch ();

                    if (ChargeSFX == 1)
                    interval = .05f;
                    else if (ChargeSFX == 2)
                    interval = .1f;
                }

                interval -= Time.deltaTime;
            }
    
        if ( Player.Shoot.OnRelease )
        {
            if ( charge >= charge2 )
            Shoot2 ();
            else if ( charge >= charge1 )
            Shoot1 ();

            Ru.Stop ();
            ChargeSFX = 0;

            flash.Reset ();
        }
    }

    void Shoot ()
    {
        s_bullet_1d bullet = Bullet0.Spawn ( dg.GunPostion, Quaternion.identity ).GetPix <s_bullet_1d> ();
        bullet.Fire ( s2d.direction );
        bullet.FromPlayer = true;
    }

    void Shoot1 ()
    {
        s_bullet_1d bullet = Bullet1.Spawn ( dg.GunPostion, Quaternion.identity ).GetPix <s_bullet_1d> ();
        bullet.Fire ( s2d.direction );
        bullet.FromPlayer = true;
    }

    void Shoot2 ()
    {
        s_bullet_1d bullet = Bullet2.Spawn ( dg.GunPostion, Quaternion.identity ).GetPix <s_bullet_1d> ();
        bullet.Fire ( s2d.direction );
        bullet.FromPlayer = true;
    }
}