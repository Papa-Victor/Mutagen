using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skillinfo : MonoBehaviour {

    private MutationDescription desc;

    public Text title;
    public Text level1;
    public Text level2;
    public Text level3;

    

    public void Init(MutationDescription description)
    {

        AOEMutation AOEMutation = MutationManager.Instance.AOEMutation;

        desc = description;

        title.text = desc.Nom;
        level1.text = "Lv.1 " + desc.mutationLevels[0].description;

        if (desc.type == MutationType.Malus)
        {
            level2.gameObject.SetActive(false);
            level3.gameObject.SetActive(false);
            return;
        }
        level2.gameObject.SetActive(true);
        level3.gameObject.SetActive(true);

        level2.text = "Lv.2 " + desc.mutationLevels[1].description;
        level3.text = "Lv.3 " + desc.mutationLevels[2].description;
    }
}
