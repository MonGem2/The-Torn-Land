using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GridLayoutGroup grid;
    public SkillButton perhub;
    public SkillBarScript skillBar;
    public SkillButton LeftButtonSkill;
    public SkillButton RightButtonSkill;
    public List<SkillButton> skills;
    public Player player;
    public SkillButton skill;
    void Start()
    {
        RightButtonSkill.OnClick += OnSetButtonClick;
        LeftButtonSkill.OnClick += OnSetButtonClick;
        RightButtonSkill.Usable = false;
        LeftButtonSkill.Usable = false;
        foreach (var item in skillBar.buttons)
        {
            item.OnClick += OnSetButtonClick;
        }
        skills = new List<SkillButton>();
        RightButtonSkill.transform.localPosition = new Vector3(RightButtonSkill.transform.localPosition.x, RightButtonSkill.transform.localPosition.y, 0);
        LeftButtonSkill.transform.localPosition = new Vector3(LeftButtonSkill.transform.localPosition.x, LeftButtonSkill.transform.localPosition.y, 0);
        if (player.Loaded)
        {
            UpdateContent();
        }
        else
        { player.OnLoaded += (x) => { UpdateContent(); }; }
    }
    public void UpdateContent()
    {
        SetGrid();
        if (LeftButtonSkill.player == null)
        {
            LeftButtonSkill.SetPlayer(player);
        }
        if (player.OnLeftClickSkill != null)
        {
            LeftButtonSkill.Set(player.OnLeftClickSkill);
            LeftButtonSkill.transform.localPosition = new Vector3(LeftButtonSkill.transform.localPosition.x, LeftButtonSkill.transform.localPosition.y, 0);
        }
        if (player.OnRightClickSkill != null)
        {
            RightButtonSkill.Show();
            RightButtonSkill.Set(player.OnRightClickSkill);
            RightButtonSkill.transform.localPosition = new Vector3(RightButtonSkill.transform.localPosition.x, RightButtonSkill.transform.localPosition.y, 0);
            
        }
    }
    public void DestroyGrid()
    {

        foreach (var item in skills)
        {
            item.OnClick -= OnSkillClick;
            Destroy(item.gameObject);
        }
        skills.Clear();
    }
    public void SetGrid() 
    {
        Debug.Log("HI");
        DestroyGrid();
        foreach (var item in player.Skills)
        {
            Debug.Log("HI");
            SkillButton button = Instantiate(perhub);
            button.SetPlayer(player);
            button.Set(item, false);
            button.transform.SetParent(grid.transform, false);
            button.transform.localPosition = new Vector3(button.transform.localPosition.x, button.transform.localPosition.y, 0);
            button.OnClick+= OnSkillClick;
            //Debug.LogWarning(button.transform.position.z);
            skills.Add(button);
        }
    }
    public void OnSkillClick(object x)
    {
        Debug.LogWarning("PSDA");
        SkillButton button = (SkillButton)x;
        button.OnGreen();
        Debug.LogWarning("PSDA1");
        skill = button;
        skillBar.OnAllGreen();
        Debug.LogWarning("PSDA2");
        RightButtonSkill.OnGreen();
        LeftButtonSkill.OnGreen();
        Debug.LogWarning("PSDA3");
    }
    public void OnSetButtonClick(object x)
    {
        Debug.Log("Co to za noher"+skill.Hiden);
        if (skill != null)
        {
            Debug.Log("Pizdec");
            SkillButton button = (SkillButton)x;
            Debug.Log(button.skill.skillType);   
            button.Set(button.skill);
            if (button == RightButtonSkill)
            {
                player.OnRightClickSkill = button.skill;
            }
            if (button == LeftButtonSkill)
            {
                player.OnLeftClickSkill = button.skill;
            }
            skill = null;
        }

    }
    public  int updateskip = 0;
    // Update is called once per frame
    void Update()
    {
        if (updateskip != 0) { updateskip++; }
        if (updateskip>40)
        {
            if (skill != null)
            {
                Debug.Log("Da blyat");
                skillBar.OffGreen();
                RightButtonSkill.OffGreen();
                LeftButtonSkill.OffGreen();
                skill.OffGreen();
                skill = null;
            }
            updateskip = 0;
 
        }
        if (RightButtonSkill.Hiden)
        {
            RightButtonSkill.Show();
        }
        if(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1))
        {
            Debug.Log("Fuck");
            updateskip++;
        }
    }
    private void OnEnable()
    {
        //Debug.Log("Enabling");
        //RightButtonSkill.OnClick += OnSetButtonClick;
        //LeftButtonSkill.OnClick += OnSetButtonClick;
        //foreach (var item in skillBar.buttons)
        //{
        //    item.OnClick += OnSetButtonClick;
        //}
        skillBar.SetUsableFalse();
        skillBar.ShowAll();
        
        //UpdateContent();
    }
    private void OnDisable()
    {
        //Debug.Log("Disabling");
        //RightButtonSkill.OnClick -= OnSetButtonClick;
        //LeftButtonSkill.OnClick -= OnSetButtonClick;
        //foreach (var item in skillBar.buttons)
        //{
        //    item.OnClick -= OnSetButtonClick;
        //}
        //DestroyGrid();
        skillBar.SetUsableTrue();
        skillBar.HideAll();
        if (skill != null)
        {
            skillBar.OffGreen();
            RightButtonSkill.OffGreen();
            LeftButtonSkill.OffGreen();
            Debug.Log("Pidor?");
            skill = null;
        }
    }
}
