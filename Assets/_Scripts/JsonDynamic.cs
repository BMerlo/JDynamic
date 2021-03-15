using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JsonDynamic : MonoBehaviour
{    
    //need these strings to load and handle data
    string path = null;    
    string textAsset = null;

    [SerializeField] UIBuilder uiBuilderRef;
    
    List<string> headersList = new List<string>();
    List<string> memberList = new List<string>();
    
    public List<List<string>> ListofLists = new List<List<string>>();
        
    void Awake()
    {
        //in awake so I'm sure that the info is ready when called from UI Builder

        //read the file
        path = Application.dataPath + "/StreamingAssets/JsonChallenge.json";
        //make it a string
        textAsset = System.IO.File.ReadAllText(path);

        //splitting file for better handling
        string[] txtArray = textAsset.Split(char.Parse("["), char.Parse("]"));

        //getting headers for table
        HeaderListFiller(txtArray);

        // Parsing Data for each member    
        MemberListFiller(txtArray);

        //Filling up list of lists to handle info before showing it
        ListOfListsFiller();
    }

    private void HeaderListFiller(string[] txtArray)
    {
        //in 1 is the data that I need for this
        string[] headers = txtArray[1].Split(char.Parse("\""));

        //then it's added to a list
        for (int i = 0; i < headers.Length - 1; i++)
        {
            if (i % 2 != 0)
            {
                headersList.Add(headers[i]);
            }
        }
    }

    private void MemberListFiller(string[] txtArray)
    {
        //in 3 is the data that I need for this
        string[] dataArray = txtArray[3].Split(char.Parse("{"), char.Parse("}"));

        //then it's added to a list
        for (int i = 0; i < dataArray.Length - 1; i++)
        {
            if (i % 2 != 0)
            {
                memberList.Add(dataArray[i]);
            }
        }
    }

    private void ListOfListsFiller()
    {
        for (int i = 0; i < memberList.Count; i++)
        {
            ListofLists.Add(new List<string>());

            string[] InfoMiembro = memberList[i].Split(char.Parse(","));

            for (int j = 0; j < headersList.Count; j++)
            {
                //I need to clean it a little bit before passing to UI Builder
                string[] ListALimpiar = InfoMiembro[j].Split(char.Parse(":"));

                //I don't care about the attribute, so I'll just keep the data
                string ListaLimpia = ListALimpiar[1].Replace("\"", string.Empty);
                ListofLists.ElementAt(i).Add(ListaLimpia);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //methods to create UI and keep things tidy 
    public int GetNumberOfMembers()
    {
        return memberList.Count;
    }

    public int GetNumberOfAttributes()
    {
        return headersList.Count;
    }

    public string GetNthAttributeName(int number) {
        return headersList.ElementAt(number);
    }

    public string GetNthAttribute(int member, int attribute) {

        return ListofLists.ElementAt(member).ElementAt(attribute);
    }

    //methods to handle data updates
    public void UpdateJsonFile()
    {       
        textAsset = System.IO.File.ReadAllText(path);
        UpdateLists();
    }

    public void UpdateLists() {
        headersList.Clear();
        memberList.Clear();
        ListofLists.Clear();
        string[] txtArray = textAsset.Split(char.Parse("["), char.Parse("]"));
        HeaderListFiller(txtArray);
        MemberListFiller(txtArray);
        ListOfListsFiller();
    }
}
