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

        public Character_Skill CharacterSkill { get; set; } = new();
        public Character_Stats_Base CharacterStatBase { get; set; } = new();
        public Character character { get; set; } = new();
        public List<Character_Skill> CharacterSkills { get; set; } = new();
        public List<Character_Skill_Description> CharacterSkillDescriptions { get; set; } = new();
        public List<Character_Skill_Detail>CharacterSkillDetails { get; set; } = new();
        public List<Character_Skill_Detail_Number> CharacterSkillDetailNumbers { get; set; } = new();
        public List<Character_Stats_Growth_Property> CharacterStatsGrowthProperties { get; set; } = new();
        private int[] HPArray { get; set; }
        private int[] ATKArray { get;set; }
        private int[] DEFArray { get; set; }
        private int LevelSlider { get; set; } = 1;
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
            StatsCalculation();
            StateHasChanged();
        }
      
        private void StatsCalculation()
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
                double mathDEF = CharacterStatsGrowthProperties.FirstOrDefault(x=>x.Level == Level).DefRatio / baseRatioHp;
                var mathedHP = mathHP * CharacterStatBase.HP;
                var mathedATK = mathATK * CharacterStatBase.ATK;
                var mathedDEF = mathDEF * CharacterStatBase.DEF;
                HPArray[i] = (int)mathedHP;
                ATKArray[i] = (int)mathedATK;
                DEFArray[i] = (int)mathedDEF;
                Level++;
            };
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
            {
                switch (result.Data)
                {
                    case "Stat_Base":
                        OpenDialogEditStat();
                        break;
                    case SkillType.Basic_Attack:
                        OpenDialogSkill(SkillType.Basic_Attack);
                        break;
                    case SkillType.Resonance_Skill:
                        OpenDialogSkill(SkillType.Resonance_Skill);
                        break;
                    case SkillType.Forte_Circuit:
                        OpenDialogSkill(SkillType.Forte_Circuit);
                        break;
                    case SkillType.Resonance_Liberation:
                        OpenDialogSkill(SkillType.Resonance_Liberation);
                        break;
                    case SkillType.Intro_Skill:
                        OpenDialogSkill(SkillType.Intro_Skill);
                        break;
                    case SkillType.Outro_Skill:
                        OpenDialogSkill(SkillType.Outro_Skill);
                        break;
                }
            }
        }

        private async void OpenDialogEditStat()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacterStats>();
            parameters.Add(x => x.CharacterId, character.Id);
            var dialog = await DialogService.ShowAsync<EditCharacterStats>("Edit Character Stats", parameters, options);
            var result = await dialog.Result;
            if (!result.Canceled)
                OnInitialized();
        }

        private async void OpenDialogSkill(SkillType type)
        {
            var skillType = string.Join(" ",
                type.ToString().Split("_").Select(w => w.Substring(0,1).ToUpper() + w.Substring(1).ToLower()));
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<EditCharacterSkill>();
            parameters.Add(x => x.CharacterId, character.Id);
            parameters.Add(x => x.SkillType, type);
            var dialog = await DialogService.ShowAsync<EditCharacterSkill>(skillType, parameters, options);
            var result= await dialog.Result;
            if (!result.Canceled)
                OnInitialized();
        }
    }
}
