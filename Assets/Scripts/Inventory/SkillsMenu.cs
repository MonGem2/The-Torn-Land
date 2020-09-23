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
    public void RemoveSkill(object x)
    {
        SkillButton skillButton = (SkillButton)x;
        if (skillButton == LeftButtonSkill)
        {
            player.OnLeftClickSkill = null;
        }
        if (skillButton == RightButtonSkill)
        {
            player.OnRightClickSkill = null;
            player.ActiveSkill = null;
        }
        skillButton.Remove();
        skillButton.Show();
    }
    //public SkillButton skill1;
    void Start()
    {
        RightButtonSkill.SetPlayer(player);
        LeftButtonSkill.SetPlayer(player);
        RightButtonSkill.OnClick += OnSetButtonClick;
        LeftButtonSkill.OnClick += OnSetButtonClick;
        RightButtonSkill.OnRightClick += RemoveSkill;
        LeftButtonSkill.OnRightClick += RemoveSkill;
        RightButtonSkill.Usable = false;
        LeftButtonSkill.Usable = false;
        foreach (var item in skillBar.buttons)
        {
            item.OnClick += OnSetButtonClick;
            
        //    Debug.Log(item.OnClick.Method.Name);
        }
        
        skills = new List<SkillButton>();
        RightButtonSkill.transform.localPosition = new Vector3(RightButtonSkill.transform.localPosition.x, RightButtonSkill.transform.localPosition.y, 0);
        LeftButtonSkill.transform.localPosition = new Vector3(LeftButtonSkill.transform.localPosition.x, LeftButtonSkill.transform.localPosition.y, 0);
        player.OnSkillRemoved += (x) => { UpdateContent(); };
        player.OnSkillAdded += (x) => { UpdateContent(); };
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
        if (RightButtonSkill.player == null)
        {
            RightButtonSkill.SetPlayer(player);
        }
        if (player.OnLeftClickSkill != null)
        {
            LeftButtonSkill.Set(player.OnLeftClickSkill, false);
            LeftButtonSkill.transform.localPosition = new Vector3(LeftButtonSkill.transform.localPosition.x, LeftButtonSkill.transform.localPosition.y, 0);
        }
        if (player.OnRightClickSkill != null)
        {
            RightButtonSkill.Show();
            RightButtonSkill.Set(player.OnRightClickSkill, false);
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
        if (skill != null)
        {
            skill.OffGreen();
        }
      //  DeactivateMoving = true;
        Debug.LogWarning("PSDA");
        SkillButton button = (SkillButton)x;
        button.OnGreen();
        Debug.LogWarning("PSDA1");
      //  skill1 = button;
        skill = button;
        skillBar.OnAllGreen();
        Debug.LogWarning("PSDA2");
        RightButtonSkill.OnGreen();
        LeftButtonSkill.OnGreen();
        Debug.LogWarning("PSDA3");
    }
    public void OnSetButtonClick(object x)
    {
       // Debug.Log(skill1 == null);
       // Debug.Log("Co to za noher"+skill1.Hiden);
        if (skill != null)
        {
            
            SkillButton button = (SkillButton)x;
            //Debug.Log("Pizdec  " + (skill == button));
            //Debug.Log(button.skill.skillType);

            if (button == RightButtonSkill)
            {
                button.Set(skill.skill, false);
                player.OnRightClickSkill = button.skill;
                player.ActiveSkill = button.skill;
            }
            else if (button == LeftButtonSkill)
            {
                button.Set(skill.skill, false);
                player.OnLeftClickSkill = button.skill;
            }
            else
            {
                button.Set(skill.skill, false);
            }
            skillBar.OffGreen();
            RightButtonSkill.OffGreen();
            LeftButtonSkill.OffGreen();
            skill.OffGreen();
            //        skill1 = null;
            skill = null;

            Debug.Log("Da blyat");
            //    DeactivateMoving = false;

        //    skill1.OffGreen();
          //  skill1 = null;
            
        }

    }
    //public bool DeactivateMoving = false;
   // public  int updateskip = 0;
    // Update is called once per frame
    void Update()
    {

        if (RightButtonSkill.Hiden)
        {
            RightButtonSkill.Show();
        }
        if(Input.GetMouseButtonDown(1))
        {
            if (skill != null)
            {
                Debug.Log("Da blyat");
            //    DeactivateMoving = false;
                skillBar.OffGreen();
                RightButtonSkill.OffGreen();
                LeftButtonSkill.OffGreen();
                skill.OffGreen();
                skill = null;
            }
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
        foreach (var item in skillBar.buttons)
        {
            item.OnRightClick += RemoveSkill;
            //    Debug.Log(item.OnClick.Method.Name);
        }
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
        foreach (var item in skillBar.buttons)
        {
            item.OnRightClick -= RemoveSkill;
            //    Debug.Log(item.OnClick.Method.Name);
        }
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
