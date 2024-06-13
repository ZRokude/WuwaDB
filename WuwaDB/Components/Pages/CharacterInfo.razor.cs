using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.Components.MudDialog;
using WuwaDB.DBAccess.Repository;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Enum;
using Microsoft.IdentityModel.Tokens;

namespace WuwaDB.Components.Pages
{
    public partial class CharacterInfo
    {
        [Inject] private AuthenticationStateProvider StateProvider { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private UserRepository UserRepository { get; set; }
        [Inject] public IDialogService DialogService { get; set; }
        [Parameter] public string CharacterName { get; set; }
        public Character_Skill? CharacterSkill { get; set; }
        public Character_Stats_Base? CharacterStatBase { get; set; }
        public Character? character { get; set; } = new();
        public List<Character_Skill> CharacterSkills { get; set; } = new();
        public List<Character_Skill_Description> CharacterSkillDescriptions { get; set; } = new();
        public List<Character_Skill_Detail>CharacterSkillDetails { get; set; } = new();
        public List<Character_Skill_Detail_Number> CharacterSkillDetailNumbers { get; set; } = new();
        public List<Character_Stats_Growth_Property> CharacterStatsGrowthProperties { get; set; } = new();
        private int[] HPArray { get; set; }
        private int[] ATKArray { get;set; }
        private int[] DEFArray { get; set; }
        private int LevelSlider { get; set; } = 1;

        private string[] highLightedTexts = ["Havoc", "Spectro", "Fusion", "Glacio", "Aero", "Electro"];

        private Dictionary<SkillType, bool> collapseStates = new();

        private Dictionary<SkillType, bool> skillDetailStates = new();

        private Dictionary<SkillType, bool> skillInfoStates = new();

        private bool skillInfoExpand = false;
        private string colorHighLight => $"color:{colorHighLightCase()}";
        protected override async void OnInitialized()
        {
            CharacterStatsGrowthProperties = await UserRepository.GetToListAsync<Character_Stats_Growth_Property>();
            character = await UserRepository.GetDataAsync<Character>(new {Name = CharacterName});
            if (character is not null)
            {
                CharacterStatBase =
                    await UserRepository.GetDataAsync<Character_Stats_Base>(propertyFilter: new
                    { CharacterId = character.Id });
                CharacterSkills = 
                    await UserRepository.GetToListAsync<Character_Skill>(propertyFilter: new 
                    { CharacterId = character.Id });
            }
            if (CharacterSkills.Count > 0)
            {
                byte[] ImageByte = CharacterSkills[0].ImageFile;
                CharacterSkillDescriptions =
                    await UserRepository.GetToListAsync<Character_Skill_Description>
                    ( new{ CharacterId = character.Id }, new string[] {"Character_Skill"});
                CharacterSkillDetails =
                    await UserRepository.GetToListAsync<Character_Skill_Detail>
                    (new {CharacterId = character.Id}, new string[] {"Character_Skill"});

            }
            if (CharacterSkillDetails.Count > 0)
            {
                CharacterSkillDetailNumbers = await UserRepository.GetToListAsync<Character_Skill_Detail_Number>
                    (new { CharacterId = character.Id }, new string[] { "Character_Skill_Detail", "Character_Skill" });
            }
            if(CharacterStatBase is not null)
                StatsCalculation();
            StateHasChanged();
        }
        private string colorHighLightCase()
        {
            switch (character.Element)
            {
                case ElementType.Aero:
                    return "#23e885";
                case ElementType.Electro:
                    return "#b05ae6";
                case ElementType.Glacio:
                    return "#4caec2";
                case ElementType.Fusion:
                    return "#de1616";
                case ElementType.Havoc:
                    return "#5e2843";
                case ElementType.Spectro:
                    return "#e0dd16";
                default: return "#ed3737";
            }

        }
        private string GetStatProp(string value)
        {
            if (value.Contains("_"))
            {
                return string.Join(" ",
                    value.ToString().Split("_").Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1).ToLower()));
            }
            return value;
        }
        private string GetSkillName(SkillType type)
        {
            var skill = CharacterSkills.FirstOrDefault(x => x.Type == type);
            if (skill is not null)
                return skill.Name;
            return "";
        }
        private bool IsCollapsed(SkillType type)
        {
            if (collapseStates.TryGetValue(type, out var isCollapsed))
                return isCollapsed;
            return false; // Default state if not found
        }

        private bool SkillDetail(SkillType type)
        {
            if (skillDetailStates.TryGetValue(type, out var skillDetail))
                return skillDetail;
            return false;
        }

        private bool SkillInfo(SkillType type)
        {
            if (skillInfoStates.TryGetValue(type, out var skillInfo))
                return skillInfo;
            return false;
        }

        private void ToggleSkillInfo(SkillType type)
        {
            if (skillInfoStates.ContainsKey(type))
            {
                skillInfoStates[type] = true;
                skillDetailStates[type] = false;
            }

        }
        private void ToggleSkillDetail(SkillType type)
        {
            if (skillDetailStates.ContainsKey(type))
            {
                skillDetailStates[type] = true;
                skillInfoStates[type] = false;
            }
        }
        private void ToggleCollapse(SkillType type)
        {
            if (collapseStates.ContainsKey(type))
                collapseStates[type] = !collapseStates[type];
            else
                collapseStates[type] = true;
            if (!skillDetailStates.ContainsKey(type))
                skillDetailStates[type] = false;
            if (!skillInfoStates.ContainsKey(type))
                skillInfoStates[type] = true;
        }


        private void StatsCalculation()
        {
            if (LevelSlider  <= 90)
            {
                HPArray = new int[90];
                ATKArray = new int[90];
                DEFArray = new int[90];
                double baseRatioHp = CharacterStatsGrowthProperties.FirstOrDefault(x => x.Level == 1).LifeMaxRatio;
                int Level = 1;
                for (int i = 0; i < 90; i++)
                {
                    double mathHP = CharacterStatsGrowthProperties.FirstOrDefault(x => x.Level == Level).LifeMaxRatio / baseRatioHp;
                    double mathATK = CharacterStatsGrowthProperties.FirstOrDefault(x => x.Level == Level).AtkRatio / baseRatioHp;
                    double mathDEF = CharacterStatsGrowthProperties.FirstOrDefault(x => x.Level == Level).DefRatio / baseRatioHp;
                    var mathedHP = mathHP * CharacterStatBase.HP;
                    var mathedATK = mathATK * CharacterStatBase.ATK;
                    var mathedDEF = mathDEF * CharacterStatBase.DEF;
                    HPArray[i] = (int)mathedHP;
                    ATKArray[i] = (int)mathedATK;
                    DEFArray[i] = (int)mathedDEF;
                    Level++;
                };
            }
        }
        private string GetImage(SkillType type)
        {
            var skillType = CharacterSkills.FirstOrDefault(x => x.Type == type);

            if (skillType is not null)
            {
                var ImageByte = skillType.ImageFile;
                if(ImageByte is not null)
                {
                    string imageSrc = Convert.ToBase64String(ImageByte);

                    return string.Format("data:image/jpeg;base64,{0}", imageSrc);
                }
            }
            return null;
        }
        
        private void LevelChanged(string value)
        {
            int Level = Convert.ToInt16(value) - 1;
            if (HPArray is not null)
                CharacterStatBase.HP = HPArray[Level];
            if (ATKArray is not null)
                CharacterStatBase.ATK = ATKArray[Level];
            if(DEFArray is not null)
                CharacterStatBase.DEF = DEFArray[Level];
        }
        private async void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacter>();
            parameters.Add(x=> x.CharacterId, character.Id);
            var dialog = await DialogService.ShowAsync<EditCharacter>("Edit Character", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
                StateHasChanged();
        }
    }
}
