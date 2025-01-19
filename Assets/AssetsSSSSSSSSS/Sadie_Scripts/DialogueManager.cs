using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string dialogueTx;
        public Sprite prawndice;
        //Can add other variables like names, text bg, etc

        [Range(1f, 25f)]
        public float lettersPerSecond;

        public AudioClip spokenLine;
    } //END Dialogue class

    //public Dialogue[] dialogue;
    [SerializeField] private TextMeshProUGUI textMesh;
    private bool currentlySpeaking;
    private int dialogueIndex;
    [SerializeField] Image prawndice;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Image textBox;
    public Dialogue[] intro;
    public Dialogue[] bankBalanceTutoial;
    public Dialogue[] wackTutorial;
    public Dialogue[] shrimpBobbingTutorial;
    public Dialogue[] rollerCosterTutorial;
    public Dialogue[] attractionNotBroken;
    public Dialogue[] cantBuy;
    public Dialogue[] caroselcantBuy;
    public Dialogue[] netProfit750000;
    public Dialogue[] riskFactor10;
    public Dialogue[] riskFactor25;
    public Dialogue[] riskFactor50;
    public Dialogue[] win;
    public Dialogue[] lose;

    

    private void Start()
    {
        //Test dialgue
        //StartCoroutine(PlayDialogue(dialogue[0]));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            if(currentlySpeaking)
            {
                currentlySpeaking = false;
                textBox.enabled = false;
                textMesh.enabled = false;
                prawndice.enabled = false;
            }
            /*if(dialogueIndex == 1)
            {
                enabled = false;
                return;
            }*/

            /*if(!currentlySpeaking)
            {
                StartCoroutine(PlayDialogue(dialogue[dialogueIndex]));
            }
            else
            {
                ;
            }*/
        }
    }


    public IEnumerator PlayDialogue(Dialogue[] _currentSegement)
    {
        currentlySpeaking = true;
        textMesh.SetText(string.Empty);
        prawndice.GetComponent<Image>().sprite = _currentSegement[0].prawndice;
        float _delay = 1f / _currentSegement[0].lettersPerSecond;

        for(int i = 0; i < _currentSegement[0].dialogueTx.Length; i++)
        {
            Debug.Log(_currentSegement[0].dialogueTx);
            string _add = string.Empty;
            _add += _currentSegement[0].dialogueTx[i];

            if(_currentSegement[0].dialogueTx[i] == ' ' && i < _currentSegement[0].dialogueTx.Length - 1)
            {
                _add = _currentSegement[0].dialogueTx.Substring(i, 2);
                i++;
            }

            textMesh.text += _add;
            Debug.Log(_add + "Add");
            yield return new WaitForSeconds(_delay);
            StartCoroutine(PlayDialogue(_currentSegement));
        }

        currentlySpeaking = false;
        dialogueIndex++;

        
    }

    public void  ReplaceText(Dialogue[] _currentSegment)
    {
        currentlySpeaking = true;
        textBox.enabled = true;
        textMesh.enabled = true;
        prawndice.GetComponent<Image>().enabled = true;
        prawndice.GetComponent<Image>().sprite = _currentSegment[0].prawndice;

        textMesh.text = _currentSegment[0].dialogueTx;

        audioSource.clip = _currentSegment[0].spokenLine;
        audioSource.Play();
    }
    
}

