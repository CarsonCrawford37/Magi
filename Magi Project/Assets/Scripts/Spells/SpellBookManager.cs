using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellBookManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI spellsUI;
    private List<string> spell_List = new List<string>();

    private int i = 0;

    public void AddSpellToBook(string rune)
    {

        spell_List.Add(rune);

        string newSpell = spell_List[i];

        spellsUI.text += (newSpell + "<br>");
        i++;
    }
}
