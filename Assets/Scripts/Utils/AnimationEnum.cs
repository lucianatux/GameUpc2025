using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimName
{
    IdleAnim,
    WakeUpAnim,
    DamageAnim,
    AttackAnim,
    WalkAnim,
    DieAnim,
    InactiveAnim

}
public static class AnimNameExtensions
{
    public static string ToAnimString(this AnimName anim)
    {
        return anim switch
        {
            AnimName.IdleAnim => "idle",
            AnimName.WakeUpAnim => "wakeup",
            AnimName.DamageAnim => "damage",
            AnimName.AttackAnim => "atack",
            AnimName.WalkAnim => "walk",
            AnimName.DieAnim => "die",
            AnimName.InactiveAnim => "inactive",
            _ => ""
        };
    }
}