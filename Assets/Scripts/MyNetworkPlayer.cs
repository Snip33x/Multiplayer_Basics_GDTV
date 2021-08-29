using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText = null;
    [SerializeField] private Renderer displayColorRenderer = null;


    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing Name";
    
    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color displayColor = Color.white;

    #region Server

    [Server] //server attribute is used here for protection 
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server] //server attribute is used here for protection 
    public void SetDisplayColor(Color newDisplayColor)
    {
        displayColor = newDisplayColor;
    }

    [Command]
    private void CmdDisplayName(string newDisplayName)
    {
        if (newDisplayName.Length > 20)
        {
            Debug.Log("Name is too long");
            return;
        }
        else if (newDisplayName.Length < 5)
        {
            Debug.Log("Name is too short");
            return;
        }
        else
        {
            RpcLogNewName(newDisplayName);

            SetDisplayName(newDisplayName);
        }
    }


    #endregion

    #region Client

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor) //mirror requires 2 arguments even if we are not using old color/argument
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")] //prawy przycisk myszy na skrypcie
    private void SetMyName()
    {
        CmdDisplayName("My New Name asdlkfjasldkfjalsdkjf");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion
}
