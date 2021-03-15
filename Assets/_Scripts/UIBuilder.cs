using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIBuilder : MonoBehaviour
{
    [SerializeField] JsonDynamic jsonDynamicRef;
    [SerializeField] GameObject prefabHeader;
    [SerializeField] GameObject prefabData;
    [SerializeField] GameObject prefabPanel;

    void Start()
    {        
        //setup headers
        SetupHeaders();
        //setup table content
        SetupData();

    }

    public void SetupData()
    {
        for (int i = 0; i < jsonDynamicRef.GetNumberOfMembers(); i++)
        {
            //need to set the parent, everyone likes chaos but not in their unity project
            GameObject gameObj = Instantiate(prefabPanel, this.transform);           

            for (int j = 0; j < jsonDynamicRef.GetNumberOfAttributes(); j++)
            {
                GameObject AttributeData = Instantiate(prefabData, gameObj.transform);                
                AttributeData.GetComponent<TextMeshProUGUI>().text = jsonDynamicRef.GetNthAttribute(i, j);
            }
        }
    }

    public void SetupHeaders()
    {
        //due to time, I'll just destroy the objects (better practice here would be pooling the objects and deactivate unnecessary
        //panels and update info on the ones that are active)
        if (transform.childCount > 0) {
            foreach (Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        //pretty much the same as before but simpler, just headers
        GameObject gameObj = Instantiate(prefabPanel, this.transform);
        
        for (int i = 0; i < jsonDynamicRef.GetNumberOfAttributes(); i++)
        {
            GameObject AttributeName = Instantiate(prefabHeader, gameObj.transform);            
            AttributeName.GetComponent<TextMeshProUGUI>().text = jsonDynamicRef.GetNthAttributeName(i);
        }
    }
}
