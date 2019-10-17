using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tacticsoft;

public class TestCounterCell : TableViewCell
{
    public Image NineumImage;
        public Text LevelText;
        public Text StatText;

        public void SetNineum(string ninea)
    {
        //Sprite sprite = Resources.Load<Sprite>("Nineum/RareBubble");
        //NineumImage.sprite = sprite;
        LevelText.text = "Alrighty Strength";
        StatText.text = "+5 Strength";
    }
}
