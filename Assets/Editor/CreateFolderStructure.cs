using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateFolderStructure : EditorWindow
{
    [MenuItem("Tools/Create Folder Structure")]
    public static void ShowWindow()
    {
        GetWindow<CreateFolderStructure>("Create Folder Structure");
    }

    private void OnGUI()
    {
        GUILayout.Label("Click the button to create the folder structure.", EditorStyles.wordWrappedLabel);

        if (GUILayout.Button("Create Folder Structure"))
        {
            CreateStructure();
        }
    }

    private void CreateStructure()
    {
        string projectFolder = "Logan";
        string[] subfolders = { "Scripts", "Resource", "Scene", "UI", "Animation", "Materials","Prefab","Tools" };

        string projectPath = Application.dataPath + "/" + projectFolder;

        
        string projectName = PlayerSettings.productName;

       
        string readmePath = projectPath + "/README.md";

        AssetDatabase.CreateFolder("Assets" , projectFolder);


        foreach (string subfolder in subfolders)
        {
            string subfolderPath = projectPath + "/" + subfolder;
            if (!AssetDatabase.IsValidFolder("Assets/" + projectFolder + "/" + subfolder))
            {
                AssetDatabase.CreateFolder("Assets/" + projectFolder, subfolder);
                Debug.Log("Created subfolder: " + subfolderPath);
            }
            else
            {
                Debug.Log("Folder already exists: " + subfolderPath);
            }
        }
        if (!File.Exists(readmePath))
        {
            string readmeContent = GetReadmeContent(projectName);
            File.WriteAllText(readmePath, readmeContent);
            Debug.Log("Created README file: " + readmePath);
        }
        else
        {
            Debug.Log("README file already exists: " + readmePath);
        }
        AssetDatabase.Refresh();
    }

    private string GetReadmeContent(string projectName)
    {
        return $"# Welcome to {projectName}\n\n" +
               $"## Overview\n" +
               $"This project is intended for [brief description of the project].\n\n" +
               $"## Notes & Updates \n"; 
             
               
    }
}
