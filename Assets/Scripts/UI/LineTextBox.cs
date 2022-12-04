using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineTextBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI _talkerText;
    [SerializeField] private TextMeshProUGUI _lineText;
    [SerializeField] private List<string> _dividedLine;
    [SerializeField] private string _talkerString;
    [SerializeField] private string _lineString;

    public float charDisp_Interval;
    public int dispLineNum;

    private float _charDisp_interval_counter;
    private int _wordCounter;
    public int _curDividedLineIndex { get; private set; }


    public bool isDisplaing_DividedLine { get; private set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void FindTMPro() 
    {
        _lineText = transform.Find("Line").GetComponent<TextMeshProUGUI>();
        _talkerText = transform.Find("TalkerBox/Talker").GetComponent<TextMeshProUGUI>();
    }


    public void SetTalkerAndLine (string line,string talker)
    {
        SetTalkerAndDividedLine(new List<string>{ line }  , talker);
    }
    public void SetTalkerAndDividedLine(List<string> dividedLine, string talker)
    {
        _talkerString = talker;
        _lineString = "";
        foreach (string aLine in dividedLine) { _lineString += aLine; }
        _dividedLine = new List<string> (dividedLine);
        _charDisp_interval_counter = 0;
        _wordCounter = 1;
    }

    public bool Disp_NextDividedLine() 
    {

        return true;
    }
    public void Disp_CurDividedLine_All() { }


}
