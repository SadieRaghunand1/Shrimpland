using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string dialogueTx;

        //Can add other variables like names, text bg, etc

        [Range(1f, 25f)]
        public float lettersPerSecond;
    } //END Dialogue class

    [SerializeField] private Dialogue[] dialogue;
    [SerializeField] private TextMeshProUGUI textMesh;
    private bool currentlySpeaking;
    private int dialogueIndex;

    private void Start()
    {
        //Test dialgue
        StartCoroutine(PlayDialogue(dialogue[0]));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(dialogueIndex == dialogue.Length)
            {
                enabled = false;
                return;
            }

            if(!currentlySpeaking)
            {
                StartCoroutine(PlayDialogue(dialogue[dialogueIndex]));
            }
            else
            {
                ;
            }
        }
    }


    private IEnumerator PlayDialogue(Dialogue _currentSegement)
    {
        currentlySpeaking = true;
        textMesh.SetText(string.Empty);

        float _delay = 1f / _currentSegement.lettersPerSecond;

        for(int i = 0; i < _currentSegement.dialogueTx.Length; i++)
        {
            string _add = string.Empty;
            _add += _currentSegement.dialogueTx[i];

            if(_currentSegement.dialogueTx[i] == ' ' && i < _currentSegement.dialogueTx.Length - 1)
            {
                _add = _currentSegement.dialogueTx.Substring(i, 2);
                i++;
            }

            textMesh.text += _add;
            yield return new WaitForSeconds(_delay);
           
        }

        currentlySpeaking = false;
        dialogueIndex++;

        
    }
}


//https://www.youtube.com/watch?v=ObSgyhBa-bo
