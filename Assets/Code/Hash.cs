using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

public static class Pri
{
    
    public static readonly int def = 0;
    public static readonly int def2nd = 1;
    public static readonly int def3rd = 2;
    public static readonly int Action = 3;
    public static readonly int Action2nd = 4;
    public static readonly int ForcedAction = 5;
    public static readonly int Recovery = 6;
    public static readonly int SubAction = 2;

}


public static class AnimationKey
{
    public static readonly term idle = new term("idle");
    public static readonly term jump = new term("jump");
    public static readonly term fall = new term("fall");
    public static readonly term run = new term("run");
    
    public static readonly term dash = new term("dash");
    public static readonly term wall_stick = new term ("wall_stick");
}