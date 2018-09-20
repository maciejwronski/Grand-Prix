using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BonusTextScript : MonoBehaviour {

    [SerializeField] Text bonusTextReference;
    List<ActiveBonus> activeBonusList;

	void Start () {
        bonusTextReference.text = "Active bonuses:\n";
        activeBonusList = new List<ActiveBonus>();
	}

    // Update is called once per frame
    void Update() {
        if(activeBonusList.Count > 0)
            FormatNewStringAndCheckForIntervals();
        else
            bonusTextReference.text = "Active bonuses:\n";
    }
    private void FormatNewStringAndCheckForIntervals() {
        bonusTextReference.text = "Active bonuses:\n";
        foreach (ActiveBonus bonus in activeBonusList) {
            bonusTextReference.text += bonus.getName() + "  " + bonus.getDuration().ToString("#.0") + "\n";
            bonus.TimeReduce(Time.deltaTime);
            if (bonus.getDuration() <= 0f) {
                activeBonusList.Remove(bonus);
                break;
            }
        }
    }
    public void AddBonusText(string BonusName, float duration) {
        var matchingvalues = activeBonusList.Exists(stringToCheck => stringToCheck.getName().Equals(BonusName));
        if(matchingvalues == false) {
            activeBonusList.Add(new ActiveBonus(BonusName, duration));
            return;
        }
        activeBonusList.Remove(activeBonusList.Find(delegate (ActiveBonus bonus) { return bonus.getName() == BonusName; }));
        activeBonusList.Add(new ActiveBonus(BonusName, duration));

    }
    public class ActiveBonus{
        string bonusName;
        float duration;

        public ActiveBonus(string BonusName, float duration) {
            this.bonusName = BonusName;
            this.duration = duration;
        }
        public void TimeReduce(float byTime) {
            duration -= byTime;
        }
        public float getDuration() {
            return duration;
        }
        public string getName() {
            return bonusName;
        }
    }
}
