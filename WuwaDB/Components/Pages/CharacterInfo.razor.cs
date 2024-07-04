using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WuwaDB.Components.MudDialog;
using WuwaDB.DBAccess.Repository;
using WuwaDB.DBAccess.Entities.Character;
using WuwaDB.DBAccess.Enum;
using WuwaDB.Components.MudDialog.CharacterDialog;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.ObjectPool;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public Character? Character { get; set; } = new();
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
        private Dictionary<string, string> imageData = new();
        private Dictionary<SkillType, int> SkillTypeLevelSlider = new();

        private bool skillInfoExpand = false;
        private string colorHighLight => $"color:{colorHighLightCase()}";
        private bool isLoading { get; set; } = false;
        protected override async void OnInitialized()
        {
            isLoading = true;

            CharacterStatsGrowthProperties = await UserRepository.GetToListAsync<Character_Stats_Growth_Property>();

            Character = await UserRepository.GetDataAsync<Character>(new {Name = CharacterName});
            if (Character is not null)
            {
                CharacterStatBase =
                    await UserRepository.GetDataAsync<Character_Stats_Base>(propertyFilter: new
                    { CharacterId = Character.Id });
                CharacterSkills = 
                    await UserRepository.GetToListAsync<Character_Skill>(propertyFilter: new 
                    { CharacterId = Character.Id });
                var CharacterImageModel = await UserRepository.GetDataAsync<Character_ImageModel>(new {CharacterId = Character.Id});
                if (CharacterImageModel is not null)
                    SetImage(nameof(CharacterImageModel), CharacterImageModel.Image);

            }
            if (CharacterSkills.Count > 0)
            {
                CharacterSkillDescriptions =
                    await UserRepository.GetToListAsync<Character_Skill_Description>
                    ( new{ CharacterId = Character.Id }, new string[] {"Character_Skill"});
                CharacterSkillDetails =
                    await UserRepository.GetToListAsync<Character_Skill_Detail>
                    (new {CharacterId = Character.Id}, new string[] {"Character_Skill"});
                

            }
            if (CharacterSkillDetails.Count > 0)
            {
                CharacterSkillDetailNumbers = await UserRepository.GetToListAsync<Character_Skill_Detail_Number>
                    (new { CharacterId = Character.Id }, new string[] { "Character_Skill_Detail", "Character_Skill" }, new string[] {"NumberMultipliers"});
            }
            if(CharacterStatBase is not null)
                StatsCalculation();
            foreach(SkillType type in  Enum.GetValues(typeof(SkillType)))
            {
                SkillTypeLevelSlider.Add(type, 1);
            }
            isLoading = false;

            StateHasChanged();
        }
        private string GetSkillDetailNumber(int level, Guid Id)
        {
            var numbers = CharacterSkillDetailNumbers.FirstOrDefault(x => x.CharacterSkillDetailId == Id && x.Level == level)?.NumberMultipliers;
            int i = 0;
            string numberJoin = "";
            if(numbers is not null)
            {
                foreach (var number in numbers)
                {
                    if (i > 0)
                        numberJoin = numberJoin + " + " + number.Number + "%";
                    else
                        numberJoin = number.Number + "%";
                    if (number.Multiplier is not null)
                        numberJoin = numberJoin + " * " + number.Multiplier;
                    i++;
                }
            }
            return numberJoin;
        }
        //private string GetSkillDetailMultiplier(int level, Guid Id)
        //{
        //    var multiplier = CharacterSkillDetailNumbers.Find(x => x.CharacterSkillDetailId == Id && x.Level == level)?.Multiplier.ToString();
        //    if(multiplier is not null)
        //    {
        //        return string.Join("*", multiplier);
        //    }
        //    return string.Empty;
        //}
        private void SkillDetailLevelChanged(string value)
        {
            
        }
        private string TextSkillInfo(SkillType type)
        {
            var Text = CharacterSkills.FirstOrDefault(x => x.Type == type)?.Description;
            return Text;
        }
        IEnumerable<string> GetHighlightedSegments(string text)
        {
            var segments = text.Split('.');
            foreach (var segment in segments)
            {
                if (!string.IsNullOrWhiteSpace(segment))
                {
                    yield return segment.Trim() + ".";
                }
            }
        }

        private string colorHighLightCase()
        {
            switch (Character.Element)
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

        private void SetImage(string type, byte[] image)
        {
            string imageSrc = Convert.ToBase64String(image);
            string imageString = string.Format("data:image/jpeg;base64,{0}", imageSrc);
            imageData.TryAdd(type, imageString);
        }
        private string GetImageSkillRoot(string CharacterName, string SkillName) => $"Character/SkillIcon/{CharacterName}/{SkillName}.png";
        private string GetImageModelRoot (string Name) => $"Character/Model/Model_{Name}.png";
        private void StatLevelChanged(string value)
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
            parameters.Add(x=> x.CharacterId, Character.Id);
            var dialog = await DialogService.ShowAsync<EditCharacter>("Edit Character", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                OnInitialized();
                StateHasChanged();
            }
               
                
        }
    }
}
