using System.Collections;
using CCC.DesignPattern;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public delegate void MutationEvent(Mutation mutation);

public class MutationManager : PublicSingleton<MutationManager>
{
    static public event MutationEvent OnMutationUpdate;

    public SceneInfo textCanva;
    private Text text; 



    //ACTIVES
    private AOEMutation aoeMutation;
    private DashMutation dashMutation;
    private TentacleMutation tentacleMutation;

    //PASSIVES

    //BUFFS
    private BloodlustMutation bloodlustMutation;
    //DEBUFFS
    private BunnyHopMutation bunnyHopMutation;
    private DoubleEdgeMutation doubleEdgeMutation;
    private RootedMutation rootedMutation;
    private SlimeTrailMutation slimeTrailMutation;
    private TurretMutation turretMutation;
    private DancingSwordMutation dancingSword;

    public void ApplyRandomMalus()
    {
        Mutation[] malus = { BunnyHopMutation, DoubleEdgeMutation, RootedMutation, SlimeTrailMutation, TurretMutation, DancingSwordMutation };
        Mutation mutation;
        int rand = Random.Range(0, 5);
        int coutner = 6;

        while (coutner > 0)
        {
            if (malus[rand].CurrentLevel == 0)
            {
                mutation = malus[rand];
                mutation.Upgrade();
                Debug.Log("You got " + mutation.Description.Nom);

                Scenes.Load(textCanva, (nextScene) =>
                {
                    text = nextScene.FindRootObject<Canvas>().GetComponentInChildren<Text>();
                    text.text = "New Mutation";
                    text.fontStyle = FontStyle.Bold;
                    text.fontSize = 18;
                    this.DelayedCall(() => {
                        text.text = mutation.Description.mutationLevels[0].description;
                        text.fontStyle = FontStyle.Bold;
                        text.fontSize = 16;
                    }, 1.5f);
                });
                this.DelayedCall(() => { Scenes.UnloadAsync(textCanva); }, 5.0f);

                //Apply UI Amazing Stuff
                coutner = 0;
            }
            else
            {
                coutner--;
                rand = (rand + 1) % 6;
            }
        }
    }



    //ACTIVES
    public AOEMutation AOEMutation
    {
        get
        {
            if (aoeMutation == null)
                aoeMutation = new AOEMutation();
            return aoeMutation;
        }
    }
    public DashMutation DashMutation
    {
        get
        {
            if (dashMutation == null)
                dashMutation = new DashMutation();
            return dashMutation;
        }
    }
    public TentacleMutation TentacleMutation
    {
        get
        {
            if (tentacleMutation == null)
                tentacleMutation = new TentacleMutation();
            return tentacleMutation;
        }
    }

    //PASSIVES
    //BUFFS
    public BloodlustMutation BloodlustMutation
    {
        get
        {
            if (bloodlustMutation == null)
                bloodlustMutation = new BloodlustMutation();
            return bloodlustMutation;
        }
    }


    //DEBUFFS
    public BunnyHopMutation BunnyHopMutation
    {
        get
        {
            if (bunnyHopMutation == null)
                bunnyHopMutation = new BunnyHopMutation();
            return bunnyHopMutation;
        }
    }
    public DoubleEdgeMutation DoubleEdgeMutation
    {
        get
        {
            if (doubleEdgeMutation == null)
                doubleEdgeMutation = new DoubleEdgeMutation();
            return doubleEdgeMutation;
        }
    }
    public RootedMutation RootedMutation
    {
        get
        {
            if (rootedMutation == null)
                rootedMutation = new RootedMutation();
            return rootedMutation;
        }
    }
    public SlimeTrailMutation SlimeTrailMutation
    {
        get
        {
            if (slimeTrailMutation == null)
                slimeTrailMutation = new SlimeTrailMutation();
            return slimeTrailMutation;
        }
    }

    public TurretMutation TurretMutation
    {
        get
        {
            if (turretMutation == null)
                turretMutation = new TurretMutation();
            return turretMutation;
        }
    }

    public DancingSwordMutation DancingSwordMutation
    {
        get
        {
            if (dancingSword == null)
                dancingSword = new DancingSwordMutation();
            return dancingSword;
        }
    }

    public void UpdateMutation(Mutation mutation)
    {
        if (MutationManager.OnMutationUpdate != null)
        {
            OnMutationUpdate(mutation);
            OnMutationUpdate = null;
        }
    }
}
