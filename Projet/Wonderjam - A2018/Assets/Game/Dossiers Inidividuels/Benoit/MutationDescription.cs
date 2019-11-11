using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public enum MutationType { Bonus, Malus };
[System.Serializable]
public enum Mutations { AOEMutation , DashMutation , TentacleMutation, BloodlustMutation, BunnyHopMutation, DoubleEdgeMutation, RootedMutation, SlimeTrailMutation, TurretMutation, DancingSwordMutation };

[System.Serializable]
public struct MutationLevel
{
    public string description;
    public int price;
}

[CreateAssetMenu(fileName = "Mutation Description", menuName = "Mutation Descruption", order = 1)]
public class MutationDescription : ScriptableObject
{
    public string Nom;
    public Sprite Icon;
    public MutationType type;
    public List<MutationLevel> mutationLevels;
}

