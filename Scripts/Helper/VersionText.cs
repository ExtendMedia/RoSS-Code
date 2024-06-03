using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Shows version number on screen
/// </summary>

namespace RoSS
{
    public class VersionText : MonoBehaviour
    {
        void Start()
        {
            GetComponent<TMP_Text>().text += Application.version;
        }


    }
}