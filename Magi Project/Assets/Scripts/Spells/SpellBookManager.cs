using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpellBookManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI spellsUI;
    [SerializeField] TextMeshProUGUI pronuncUI;
    [SerializeField] TextMeshProUGUI descripUI;
    private List<string> spell_List = new List<string>();
    private List<string> pronunc_List = new List<string>();
    private List<string> descrip_List = new List<string>();

    private int i = 0;

    public void AddSpellToBook(string rune, string pronuciation, string description)
    {

        spell_List.Add(rune);
        pronunc_List.Add(pronuciation);
        descrip_List.Add(description);

        string newSpell = spell_List[i];
        string newPronuc = pronunc_List[i];
        string newDescrip = descrip_List[i];

        spellsUI.text += (newSpell + "<br>");
        pronuncUI.text += (newPronuc + "<br>");
        descripUI.text += (newDescrip + "<br>");
        i++;
    }
}
