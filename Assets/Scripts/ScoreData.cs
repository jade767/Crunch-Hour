using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Scriptable Objects/Score Data")]
public class ScoreData : ScriptableObject
{
    public float score;
    //score multiplier
    public float flowstate;
    public float combo;
    //A-F score cutoffs, cutoffs will have to be updated later to make any sence
    public float ACutOff = 30000;
    public float BCutOff = 25000;
    public float CCutOff = 15000;
    public float DCutOff = 10000;
    public float FCutOff = 5000;

    private void OnEnable()
    {
        score = 0f;
        flowstate = 1f;
        combo = 0f;
    }
}
