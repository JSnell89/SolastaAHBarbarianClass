using SolastaModApi;
using SolastaModApi.Extensions;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using static FeatureDefinitionSavingThrowAffinity;

namespace SolastaAHBarbarianClass
{
    internal class AHBarbarianClassBuilder : CharacterClassDefinitionBuilder
    {
        const string AHBarbarianClassName = "AHBarbarianClass";
        const string AHBarbarianClassNameGuid = "c92ac575-dfb8-4df4-8cfc-44688be631a4";
        const string AHBarbarianClassSubclassesGuid = "47355966-3070-443e-8c80-8bea3a20b557";

        protected AHBarbarianClassBuilder(string name, string guid) : base(name, guid)
        {
            var fighter = DatabaseHelper.CharacterClassDefinitions.Fighter;
            Definition.GuiPresentation.Title = "Class/&AHBarbarianClassTitle";
            Definition.GuiPresentation.Description = "Class/&AHBarbarianClassDescription";
            Definition.GuiPresentation.SetSpriteReference(fighter.GuiPresentation.SpriteReference);

            Definition.SetClassAnimationId(AnimationDefinitions.ClassAnimationId.Fighter);
            Definition.SetClassPictogramReference(fighter.ClassPictogramReference);
            Definition.SetDefaultBattleDecisions(fighter.DefaultBattleDecisions);
            Definition.SetHitDice(RuleDefinitions.DieType.D12);
            Definition.SetIngredientGatheringOdds(fighter.IngredientGatheringOdds);
            Definition.SetRequiresDeity(false);

            Definition.AbilityScoresPriority.AddRange(fighter.AbilityScoresPriority);
            Definition.EquipmentRows.AddRange(fighter.EquipmentRows);
            Definition.FeatAutolearnPreference.AddRange(fighter.FeatAutolearnPreference);
            Definition.PersonalityFlagOccurences.AddRange(fighter.PersonalityFlagOccurences);
            Definition.SkillAutolearnPreference.AddRange(fighter.SkillAutolearnPreference);
            Definition.ToolAutolearnPreference.AddRange(fighter.ToolAutolearnPreference);

            Definition.EquipmentRows.Clear();
            List<CharacterClassDefinition.HeroEquipmentOption> list = new List<CharacterClassDefinition.HeroEquipmentOption>();
            List<CharacterClassDefinition.HeroEquipmentOption> list2 = new List<CharacterClassDefinition.HeroEquipmentOption>();
            list.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Greataxe, EquipmentDefinitions.OptionWeapon, 1));
            list2.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Greataxe, EquipmentDefinitions.OptionWeaponMartialChoice, 1));
            List<CharacterClassDefinition.HeroEquipmentOption> list3 = new List<CharacterClassDefinition.HeroEquipmentOption>();
            List<CharacterClassDefinition.HeroEquipmentOption> list4 = new List<CharacterClassDefinition.HeroEquipmentOption>();
            list3.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Handaxe, EquipmentDefinitions.OptionWeapon, 2));
            list4.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Mace, EquipmentDefinitions.OptionWeaponSimpleChoice, 1));
            this.AddEquipmentRow(list, list2);
            this.AddEquipmentRow(list3, list4);
            this.AddEquipmentRow(new List<CharacterClassDefinition.HeroEquipmentOption>
            {
                EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Javelin, EquipmentDefinitions.OptionWeapon, 4),
                EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.ExplorerPack, EquipmentDefinitions.OptionStarterPack, 1)
            });

            Definition.FeatureUnlocks.Clear();
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyFighterSavingThrow, 1)); //Same saves as fighter :)
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyClericArmor, 1)); //Same armor as cleric :)
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyFighterWeapon, 1)); //Same weapons as fighter :)
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHBarbarianClassSkillPointPoolBuilder.AHBarbarianClassSkillPointPool, 1)); //Custom skills
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHBarbarianClassRageClassPowerBuilder.RageClassPower, 1));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionPowers.PowerReckless, 2));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHBarbarianClassDangerSenseDexteritySavingThrowAffinityBuilder.AHBarbarianClassDangerSenseDexteritySavingThrowAffinity, 2)); //Not as restrictive as true danger sense
            //Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHBarbarianClassRageClassPowerAdditionalUse1Builder.RageClassPower, 3)); //Add additional uses through subclasses since most times they alter the rage power anyways.
            //Subclass feature at level 3
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetAbilityScoreChoice, 4));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionAttributeModifiers.AttributeModifierFighterExtraAttack, 5));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHBarbarianClassFastMovementMovementAffinityBuilder.FastMovementMovementAffinity, 5));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHBarbarianClassFastMovementMovementPowerForLevelUpDescriptionBuilder.FastMovementMovementPowerForLevelUpDescription, 5));
            //Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHBarbarianClassRageClassPowerAdditionalUse2Builder.RageClassPower, 6)); //Add additional rage uses through subclasses since most times they alter the rage power anyways.
            //SubclassFeature at level 6
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionCombatAffinitys.CombatAffinityEagerForBattle, 7));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHBarbarianClassInitiativeAdvantagePowerForLevelUpDescriptionBuilder.InitiativeAdvantagePowerForLevelUpDescription, 7));
            //Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionCampAffinitys.CampAffinityFeatFocusedSleeper, 7)); //Could use this to helps not be asleep in camp maybe?  Could add the full Oblivion domain thing? Not sure
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetAbilityScoreChoice, 8));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionAttributeModifiers.AttributeModifierMartialChampionImprovedCritical, 9)); //Ideally we could add extra damage on crit but I don't think that's possible

            var subclassChoicesGuiPresentation = new GuiPresentation();
            subclassChoicesGuiPresentation.Title = "Subclass/&AHBarbarianSubclassPathTitle";
            subclassChoicesGuiPresentation.Description = "Subclass/&AHBarbarianSubclassPathDescription";
            BarbarianFeatureDefinitionSubclassChoice = this.BuildSubclassChoice(3, "Path", false, "SubclassChoiceBarbarianSpecialistArchetypes", subclassChoicesGuiPresentation, AHBarbarianClassSubclassesGuid);
        }

        public static void BuildAndAddClassToDB()
        {
            var barbarianClass = new AHBarbarianClassBuilder(AHBarbarianClassName, AHBarbarianClassNameGuid).AddToDB();
            //Might need to add subclasses after the calss is in the DB?
            CharacterSubclassDefinition characterSubclassDefinition = AHBarbarianSubClassPathOfTheBear.Build();
            BarbarianFeatureDefinitionSubclassChoice.Subclasses.Add(characterSubclassDefinition.Name);
        }

        private static FeatureDefinitionSubclassChoice BarbarianFeatureDefinitionSubclassChoice;
    }

    public static class AHBarbarianSubClassPathOfTheBear
    {
        const string AHBarbarianSubClassPathOfTheBearName = "AHBarbarianSubclassPathOfTheBear";
        const string AHBarbarianSubClassPathOfTheBearNameGuid = "04c5dd39-352f-460b-814e-52abec921437";

        public static CharacterSubclassDefinition Build()
        {
            var subclassGuiPresentation = new GuiPresentationBuilder(
                    "Subclass/&AHBarbarianSubclassPathOfTheBearDescription",
                    "Subclass/&AHBarbarianSubclassPathOfTheBearTitle")
                    .SetSpriteReference(DatabaseHelper.CharacterSubclassDefinitions.MartialChampion.GuiPresentation.SpriteReference)
                    .Build();

            CharacterSubclassDefinition definition = new CharacterSubclassDefinitionBuilder(AHBarbarianSubClassPathOfTheBearName, AHBarbarianSubClassPathOfTheBearNameGuid)
                    .SetGuiPresentation(subclassGuiPresentation)
                    .AddFeatureAtLevel(AHBarbarianClassPathOfBearRageClassPowerBuilder.RageClassPower, 3) // Special rage and increase rage count to 3
                    //.AddFeatureAtLevel(AHBarbarianClassPathOfBearRageClassPowerBuilder.RageClassPower, 3) // TODO something more?
                    .AddFeatureAtLevel(AHBarbarianClassPathOfBearRageClassPowerLevel6Builder.RageClassPower, 6) //Up rage count to 4 - Do in subclass since BearRage has its own characteristics
                    .AddFeatureAtLevel(DatabaseHelper.FeatureDefinitionEquipmentAffinitys.EquipmentAffinityBullsStrength, 6) //Double carry cap
                    .AddFeatureAtLevel(AHBarbarianClassPathOfBearRageClassPowerLevel9Builder.RageClassPower, 9) //Up damage on rage - Do in subclass since BearRage has its own characteristics
                    .AddFeatureAtLevel(DatabaseHelper.FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityDwarvenPlateResistShove, 10) //TODO Extra feature totem has commune with nature but not sure what to add here.
                    .AddToDB();

            return definition;
        }
    }

    internal class AHBarbarianClassSkillPointPoolBuilder : BaseDefinitionBuilder<FeatureDefinitionPointPool>
    {
        const string FastMovementMovementAffinityName = "AHBarbarianClassSkillPointPool";
        const string FastMovementMovementAffinityNameGuid = "11ad4edb-9e0f-487a-b3af-aecf00c33748";

        protected AHBarbarianClassSkillPointPoolBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPointPools.PointPoolFighterSkillPoints, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassSkillPointPoolTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassSkillPointPoolDescription";

            Definition.SetPoolAmount(2);
            Definition.SetPoolType(HeroDefinitions.PointsPoolType.Skill);
            Definition.RestrictedChoices.Clear();
            Definition.RestrictedChoices.AddRange(new string[] { "AnimalHandling", "Athletics", "Intimidation", "Nature", "Perception", "Survival", });
        }

        public static FeatureDefinitionPointPool CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassSkillPointPoolBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPointPool AHBarbarianClassSkillPointPool = CreateAndAddToDB(FastMovementMovementAffinityName, FastMovementMovementAffinityNameGuid);
    }

    internal class AHBarbarianClassDangerSenseDexteritySavingThrowAffinityBuilder : BaseDefinitionBuilder<FeatureDefinitionSavingThrowAffinity>
    {
        const string AHBarbarianClassDangerSenseDexteritySavingThrowAffinityName = "AHBarbarianClassDangerSenseDexteritySavingThrowAffinity";
        const string AHBarbarianClassDangerSenseDexteritySavingThrowAffinityNameGuid = "5ee158ce-758c-4214-b951-9b17638c6bb0";

        protected AHBarbarianClassDangerSenseDexteritySavingThrowAffinityBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionSavingThrowAffinitys.SavingThrowAffinityCreedOfArun, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassAHBarbarianClassDangerSenseDexteritySavingThrowAffinityTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassAHBarbarianClassDangerSenseDexteritySavingThrowAffinityDescription";

            //Just always gives Dex save ADV since making it work contextually with the effect originating within 30ft would require too much work.
            //The condition restrictions might be easier to implement, but I won't bother for now at least.
            Definition.AffinityGroups.Clear();
            var dexSaveAffinityGroup = new SavingThrowAffinityGroup();
            dexSaveAffinityGroup.affinity = RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
            dexSaveAffinityGroup.abilityScoreName = "Dexterity";
            dexSaveAffinityGroup.savingThrowContext = RuleDefinitions.SavingThrowContext.None;
            dexSaveAffinityGroup.savingThrowModifierType = ModifierType.AddDice;
            Definition.AffinityGroups.Add(dexSaveAffinityGroup);
        }

        public static FeatureDefinitionSavingThrowAffinity CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassDangerSenseDexteritySavingThrowAffinityBuilder(name, guid).AddToDB();

        public static FeatureDefinitionSavingThrowAffinity AHBarbarianClassDangerSenseDexteritySavingThrowAffinity = CreateAndAddToDB(AHBarbarianClassDangerSenseDexteritySavingThrowAffinityName, AHBarbarianClassDangerSenseDexteritySavingThrowAffinityNameGuid);
    }

    internal class AHBarbarianClassFastMovementMovementAffinityBuilder : BaseDefinitionBuilder<FeatureDefinitionMovementAffinity>
    {
        const string FastMovementMovementAffinityName = "AHBarbarianClassFastMovementMovementAffinity";
        const string FastMovementMovementAffinityNameGuid = "0e698d9a-206b-4be8-b52f-de6f2df0fc5a";

        protected AHBarbarianClassFastMovementMovementAffinityBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionMovementAffinitys.MovementAffinityLongstrider, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassFastMovementMovementAffinityTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassFastMovementMovementAffinityDescription";
        }

        public static FeatureDefinitionMovementAffinity CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassFastMovementMovementAffinityBuilder(name, guid).AddToDB();

        public static FeatureDefinitionMovementAffinity FastMovementMovementAffinity
            = CreateAndAddToDB(FastMovementMovementAffinityName, FastMovementMovementAffinityNameGuid);
    }

    internal class AHBarbarianClassFastMovementMovementPowerForLevelUpDescriptionBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string FastMovementMovementAffinityName = "AHBarbarianClassFastMovementMovementPowerForLevelUpDescription";
        const string FastMovementMovementAffinityNameGuid = "25b202af-4768-4b4a-a8d5-bf6d2976f86b";

        protected AHBarbarianClassFastMovementMovementPowerForLevelUpDescriptionBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerDomainElementalFireBurst, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassFastMovementMovementAffinityTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassFastMovementMovementAffinityDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.None); 
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Permanent);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(0);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassFastMovementMovementAffinityTitle");
            Definition.SetEffectDescription(new EffectDescription());
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassFastMovementMovementPowerForLevelUpDescriptionBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower FastMovementMovementPowerForLevelUpDescription
            = CreateAndAddToDB(FastMovementMovementAffinityName, FastMovementMovementAffinityNameGuid);
    }

    internal class AHBarbarianClassInitiativeAdvantagePowerForLevelUpDescriptionBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string FastMovementMovementAffinityName = "AHBarbarianClassInitiativeAdvantagePowerForLevelUpDescription";
        const string FastMovementMovementAffinityNameGuid = "b35ba296-477f-4a78-9306-39ad1b2efda9";

        protected AHBarbarianClassInitiativeAdvantagePowerForLevelUpDescriptionBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerDomainElementalFireBurst, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassInitiativeAdvantageTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassInitiativeAdvantageDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.None);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Permanent);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(0);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassInitiativeAdvantageTitle");
            Definition.SetEffectDescription(new EffectDescription());
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassInitiativeAdvantagePowerForLevelUpDescriptionBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower InitiativeAdvantagePowerForLevelUpDescription
            = CreateAndAddToDB(FastMovementMovementAffinityName, FastMovementMovementAffinityNameGuid);
    }

    internal class AHBarbarianClassRageClassPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RageClassPowerName = "AHBarbarianClassRageClassPower";
        const string RageClassPowerNameGuid = "1fe90786-46cf-407d-b315-1a4622c2aca7";

        protected AHBarbarianClassRageClassPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerDomainElementalFireBurst, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassPowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(2);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassRageClassPowerTitle");

            //Create the power attack effect
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianClassRageClassConditionBuilder.RageClassCondition;

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(rageEffect);
            newEffectDescription.HasSavingThrow = false;
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
            newEffectDescription.DurationParameter = 1;
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
            newEffectDescription.SetCanBePlacedOnCharacter(true);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassRageClassPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(RageClassPowerName, RageClassPowerNameGuid);
    }

    internal class AHBarbarianClassPathOfBearRageClassPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RageClassPowerName = "AHBarbarianClassPathOfBearRageClassPower";
        const string RageClassPowerNameGuid = "5028d6ad-7f61-4b45-817e-f73ab769de33";

        protected AHBarbarianClassPathOfBearRageClassPowerBuilder(string name, string guid) : base(AHBarbarianClassRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfBearRageClassPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfBearRageClassPowerDescription";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassRageClassPowerBuilder.RageClassPower);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(3); //3 uses at level 3 when this is introduced
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassPathOfBearRageClassPowerTitle");

            //Create the power attack effect
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfTheBearRageClassConditionBuilder.PathOfBearRageCondition;

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(rageEffect);
            newEffectDescription.HasSavingThrow = false;
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
            newEffectDescription.DurationParameter = 1;
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
            newEffectDescription.SetCanBePlacedOnCharacter(true);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassPathOfBearRageClassPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(RageClassPowerName, RageClassPowerNameGuid);
    }

    internal class AHBarbarianClassPathOfBearRageClassPowerLevel6Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RageClassPowerName = "AHBarbarianClassPathOfBearRageClassPowerLevel6";
        const string RageClassPowerNameGuid = "f5257a1f-758d-494a-9cb9-6b82b38bdb57";

        protected AHBarbarianClassPathOfBearRageClassPowerLevel6Builder(string name, string guid) : base(AHBarbarianClassPathOfBearRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfBearRageClassPowerLevel6Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfBearRageClassPowerLevel6Description";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassPathOfBearRageClassPowerBuilder.RageClassPower);
            Definition.SetFixedUsesPerRecharge(4); //Just increase the use count
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassPathOfBearRageClassPowerLevel6Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(RageClassPowerName, RageClassPowerNameGuid);
    }

    internal class AHBarbarianClassPathOfBearRageClassPowerLevel9Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RageClassPowerName = "AHBarbarianClassPathOfBearRageClassPowerLevel9";
        const string RageClassPowerNameGuid = "de884797-52e9-4a86-928c-ac82a4f2f98d";

        protected AHBarbarianClassPathOfBearRageClassPowerLevel9Builder(string name, string guid) : base(AHBarbarianClassPathOfBearRageClassPowerLevel6Builder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfBearRageClassPowerLevel9Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfBearRageClassPowerLevel9Description";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassPathOfBearRageClassPowerLevel6Builder.RageClassPower);
            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(4); //4 uses at level 9
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassPathOfBearRageClassPowerTitle");

            //Create the power rage condition - this time Bear With level 9 extra damage
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfTheBearRageClassConditionLevel9Builder.RageClassCondition;

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(rageEffect);
            newEffectDescription.HasSavingThrow = false;
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
            newEffectDescription.DurationParameter = 1;
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
            newEffectDescription.SetCanBePlacedOnCharacter(true);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassPathOfBearRageClassPowerLevel9Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(RageClassPowerName, RageClassPowerNameGuid);
    }


    internal class AHBarbarianClassRageClassConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string RageClassConditionName = "AHBarbarianClassRageClassCondition";
        const string RageClassConditionNameGuid = "0aaf52dd-c649-4b98-9eca-5f4ee2fc78b7";

        protected AHBarbarianClassRageClassConditionBuilder(string name, string guid) : base(DatabaseHelper.ConditionDefinitions.ConditionHeraldOfBattle, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassConditionDescription";

            Definition.SetAllowMultipleInstances(false);
            Definition.Features.Clear();
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityBludgeoningResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinitySlashingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPiercingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityConditionBullsStrength);
            Definition.Features.Add(AHBarbarianClassRageClassStrengthSavingThrowAffinityBuilder.RageClassStrengthSavingThrowAffinity);
            Definition.Features.Add(AHBarbarianClassRageClassDamageBonusAttackModifierBuilder.RageClassDamageBonusAttackModifier);
            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
            Definition.SetDurationParameter(1);


            Definition.SetDurationType(RuleDefinitions.DurationType.Turn);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassRageClassConditionBuilder(name, guid).AddToDB();

        public static ConditionDefinition RageClassCondition
            = CreateAndAddToDB(RageClassConditionName, RageClassConditionNameGuid);
    }

    internal class AHBarbarianPathOfTheBearRageClassConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string RageClassConditionName = "AHBarbarianPathOfTheBearRageClassCondition";
        const string RageClassConditionNameGuid = "8438ff4e-1891-45af-acb5-04061a76933f";

        protected AHBarbarianPathOfTheBearRageClassConditionBuilder(string name, string guid) : base(DatabaseHelper.ConditionDefinitions.ConditionHeraldOfBattle, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassBearConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassBearConditionDescription";

            //Resitance to all damage so as not to straight copy Totem bear :)
            Definition.SetAllowMultipleInstances(false);
            Definition.Features.Clear();
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityAcidResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityBludgeoningResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityColdResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityFireResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityForceDamageResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityLightningResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityNecroticResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPiercingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPoisonResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPsychicResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityRadiantResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinitySlashingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityThunderResistance);

            Definition.Features.Add(DatabaseHelper.FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityConditionBullsStrength);
            Definition.Features.Add(AHBarbarianClassRageClassStrengthSavingThrowAffinityBuilder.RageClassStrengthSavingThrowAffinity);
            Definition.Features.Add(AHBarbarianClassRageClassDamageBonusAttackModifierBuilder.RageClassDamageBonusAttackModifier);
            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
            Definition.SetDurationParameter(1);


            Definition.SetDurationType(RuleDefinitions.DurationType.Turn);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheBearRageClassConditionBuilder(name, guid).AddToDB();

        public static ConditionDefinition PathOfBearRageCondition
            = CreateAndAddToDB(RageClassConditionName, RageClassConditionNameGuid);
    }

    /// <summary>
    /// Up the rage damage at level 9 - otherwise this is the same.
    /// Maybe there's a better way to do this
    /// </summary>
    internal class AHBarbarianPathOfTheBearRageClassConditionLevel9Builder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string RageClassConditionName = "AHBarbarianPathOfTheBearRageClassConditionLevel9";
        const string RageClassConditionNameGuid = "562bea52-c4e2-4ce9-a7b0-5cf754c28cac";

        protected AHBarbarianPathOfTheBearRageClassConditionLevel9Builder(string name, string guid) : base(DatabaseHelper.ConditionDefinitions.ConditionHeraldOfBattle, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassBearConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassBearConditionDescription";

            //Resitance to all damage so as not to straight copy Totem bear :)
            Definition.SetAllowMultipleInstances(false);
            Definition.Features.Clear();
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityAcidResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityBludgeoningResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityColdResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityFireResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityForceDamageResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityLightningResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityNecroticResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPiercingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPoisonResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPsychicResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityRadiantResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinitySlashingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityThunderResistance);

            Definition.Features.Add(DatabaseHelper.FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityConditionBullsStrength);
            Definition.Features.Add(AHBarbarianClassRageClassStrengthSavingThrowAffinityBuilder.RageClassStrengthSavingThrowAffinity);
            Definition.Features.Add(AHBarbarianClassRageClassDamageBonusAttackModifierLevel9Builder.RageClassDamageBonusAttackLevel9Modifier);
            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
            Definition.SetDurationParameter(1);


            Definition.SetDurationType(RuleDefinitions.DurationType.Turn);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheBearRageClassConditionLevel9Builder(name, guid).AddToDB();

        public static ConditionDefinition RageClassCondition
            = CreateAndAddToDB(RageClassConditionName, RageClassConditionNameGuid);
    }

    internal class AHBarbarianClassRageClassStrengthSavingThrowAffinityBuilder : BaseDefinitionBuilder<FeatureDefinitionSavingThrowAffinity>
    {
        const string RageClassStrengthSavingThrowAffinityName = "AHBarbarianClassRageClassStrengthSavingThrowAffinity";
        const string RageClassStrengthSavingThrowAffinityNameGuid = "97bbd929-30c1-4c6a-b137-9273ffa4dfd4";

        protected AHBarbarianClassRageClassStrengthSavingThrowAffinityBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionSavingThrowAffinitys.SavingThrowAffinityCreedOfArun, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassStrengthSavingThrowAffinityTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassStrengthSavingThrowAffinityDescription";

            Definition.AffinityGroups.Clear();
            var strengthSaveAffinityGroup = new SavingThrowAffinityGroup();
            strengthSaveAffinityGroup.affinity = RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
            strengthSaveAffinityGroup.abilityScoreName = "Strength";
            Definition.AffinityGroups.Add(strengthSaveAffinityGroup);
        }

        public static FeatureDefinitionSavingThrowAffinity CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassRageClassStrengthSavingThrowAffinityBuilder(name, guid).AddToDB();

        public static FeatureDefinitionSavingThrowAffinity RageClassStrengthSavingThrowAffinity
            = CreateAndAddToDB(RageClassStrengthSavingThrowAffinityName, RageClassStrengthSavingThrowAffinityNameGuid);
    }

    internal class AHBarbarianClassRageClassDamageBonusAttackModifierBuilder : BaseDefinitionBuilder<FeatureDefinitionAttackModifier>
    {
        const string RageClassDamageBonusAttackModifierName = "AHBarbarianClassRageClassDamageBonusAttackModifier";
        const string RageClassDamageBonusAttackModifierNameGuid = "5af57a42-479a-4c3f-993b-ea09108cacbb";

        protected AHBarbarianClassRageClassDamageBonusAttackModifierBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAttackModifiers.AttackModifierFightingStyleArchery, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassDamageBonusAttackModifierTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassDamageBonusAttackModifierDescription";

            Definition.SetAttackRollModifier(0);
            Definition.SetDamageRollModifier(2);//Could find a way to up this at level 9 to match barb but that seems like a lot of work right now :)
        }

        public static FeatureDefinitionAttackModifier CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassRageClassDamageBonusAttackModifierBuilder(name, guid).AddToDB();

        public static FeatureDefinitionAttackModifier RageClassDamageBonusAttackModifier
            = CreateAndAddToDB(RageClassDamageBonusAttackModifierName, RageClassDamageBonusAttackModifierNameGuid);
    }

    internal class AHBarbarianClassRageClassDamageBonusAttackModifierLevel9Builder : BaseDefinitionBuilder<FeatureDefinitionAttackModifier>
    {
        const string RageClassDamageBonusAttackModifierName = "AHBarbarianClassRageClassDamageBonusAttackModifierLevel9";
        const string RageClassDamageBonusAttackModifierNameGuid = "a79155d5-95da-464a-8e35-b9426be6cef7";

        protected AHBarbarianClassRageClassDamageBonusAttackModifierLevel9Builder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAttackModifiers.AttackModifierFightingStyleArchery, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassDamageBonusAttackModifierTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassDamageBonusAttackModifierDescription";

            Definition.SetAttackRollModifier(0);
            Definition.SetDamageRollModifier(3);
        }

        public static FeatureDefinitionAttackModifier CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassRageClassDamageBonusAttackModifierLevel9Builder(name, guid).AddToDB();

        public static FeatureDefinitionAttackModifier RageClassDamageBonusAttackLevel9Modifier
            = CreateAndAddToDB(RageClassDamageBonusAttackModifierName, RageClassDamageBonusAttackModifierNameGuid);
    }

    //internal class AHBarbarianClassBuilder2 : CharacterClassDefinitionBuilder
    //{
    //    const string AHBarbarianClassName = "AHBarbarianClass2";
    //    const string AHBarbarianClassNameGuid = "f32b88a0-2354-4a0d-aafa-5118d9cc7501";
    //    const string AHBarbarianClassSubclassesGuid = "e11377bc-5a49-42a9-be0e-b19645a88935";

    //    protected AHBarbarianClassBuilder2(string name, string guid) : base(name, guid)
    //    {
    //        CharacterClassDefinitionBuilder characterClassDefinitionBuilder = new CharacterClassDefinitionBuilder(AHBarbarianClassName, AHBarbarianClassNameGuid);
    //        characterClassDefinitionBuilder.SetHitDice(RuleDefinitions.DieType.D2);
    //        characterClassDefinitionBuilder.AddPersonality(DatabaseHelper.PersonalityFlagDefinitions.GpCombat, 3);
    //        characterClassDefinitionBuilder.AddPersonality(DatabaseHelper.PersonalityFlagDefinitions.GpExplorer, 1);
    //        characterClassDefinitionBuilder.AddPersonality(DatabaseHelper.PersonalityFlagDefinitions.Normal, 3);
    //        characterClassDefinitionBuilder.SetIngredientGatheringOdds(5);
    //        characterClassDefinitionBuilder.SetBattleAI(DatabaseHelper.DecisionPackageDefinitions.DefaultMeleeWithBackupRangeDecisions);
    //        characterClassDefinitionBuilder.SetAnimationId(AnimationDefinitions.ClassAnimationId.Fighter);
    //        characterClassDefinitionBuilder.SetAbilityScorePriorities("Strength", "Constitution", "Dexterity", "Wisdom", "Charisma", "Intelligence");
    //        characterClassDefinitionBuilder.AddSkillPreference(DatabaseHelper.SkillDefinitions.Athletics);
    //        characterClassDefinitionBuilder.AddSkillPreference(DatabaseHelper.SkillDefinitions.Acrobatics);
    //        characterClassDefinitionBuilder.AddSkillPreference(DatabaseHelper.SkillDefinitions.AnimalHandling);
    //        characterClassDefinitionBuilder.AddSkillPreference(DatabaseHelper.SkillDefinitions.Perception);
    //        characterClassDefinitionBuilder.AddSkillPreference(DatabaseHelper.SkillDefinitions.Survival);
    //        characterClassDefinitionBuilder.AddFeatPreference(DatabaseHelper.FeatDefinitions.RushToBattle);
    //        characterClassDefinitionBuilder.AddFeatPreference(DatabaseHelper.FeatDefinitions.EagerForBattle);
    //        characterClassDefinitionBuilder.AddFeatPreference(DatabaseHelper.FeatDefinitions.FollowUpStrike);
    //        characterClassDefinitionBuilder.SetPictogram(DatabaseHelper.CharacterClassDefinitions.Fighter.ClassPictogramReference);
    //        GuiPresentationBuilder guiPresentationBuilder = new GuiPresentationBuilder("Class/&AHBarbarianClassDescription", "Class/&AHBarbarianClassTitle");
    //        guiPresentationBuilder.SetSortOrder(1);
    //        guiPresentationBuilder.SetSpriteReference(new AssetReferenceSprite(DatabaseHelper.CharacterClassDefinitions.Fighter.GuiPresentation.SpriteReference.AssetGUID));
    //        characterClassDefinitionBuilder.SetGuiPresentation(guiPresentationBuilder.Build());
    //        List<CharacterClassDefinition.HeroEquipmentOption> list = new List<CharacterClassDefinition.HeroEquipmentOption>();
    //        CharacterClassDefinition.HeroEquipmentOption item = EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Greataxe, EquipmentDefinitions.OptionWeaponMartialMeleeChoice, 1);
    //        list.Add(item);
    //        characterClassDefinitionBuilder.AddEquipmentRow(list);
    //        List<CharacterClassDefinition.HeroEquipmentOption> list2 = new List<CharacterClassDefinition.HeroEquipmentOption>();
    //        List<CharacterClassDefinition.HeroEquipmentOption> list3 = new List<CharacterClassDefinition.HeroEquipmentOption>();
    //        list2.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Handaxe, EquipmentDefinitions.OptionWeaponSimpleChoice, 2));
    //        list3.Add(EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Mace, EquipmentDefinitions.OptionWeaponSimpleChoice, 1));
    //        characterClassDefinitionBuilder.AddEquipmentRow(list2, list3);
    //        characterClassDefinitionBuilder.AddEquipmentRow(new List<CharacterClassDefinition.HeroEquipmentOption>
    //        {
    //            EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.Javelin, EquipmentDefinitions.OptionWeaponSimpleChoice, 4),
    //            EquipmentOptionsBuilder.Option(DatabaseHelper.ItemDefinitions.ExplorerPack, EquipmentDefinitions.OptionStarterPack, 1)
    //        });
    //        characterClassDefinitionBuilder.AddFeatureAtLevel(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyClericArmor, 1);
    //        characterClassDefinitionBuilder.AddFeatureAtLevel(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyFighterWeapon, 1);
    //        characterClassDefinitionBuilder.AddFeatureAtLevel(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyFighterSavingThrow, 1);
    //        characterClassDefinitionBuilder.AddFeatureAtLevel(AHBarbarianClassSkillPointPoolBuilder.AHBarbarianClassSkillPointPool, 1);
            
    //        var subclassChoicesGuiPresentation = new GuiPresentation();
    //        subclassChoicesGuiPresentation.Title = "Feature/&AHBarbarianSubclassPathTitle";
    //        subclassChoicesGuiPresentation.Description = "Class/&AHBarbarianSubclassPathDescription";
    //        FeatureDefinitionSubclassChoice featureDefinitionSubclassChoice = this.BuildSubclassChoice(3, "Path", false, "SubclassChoiceBarbarianSpecialistArchetypes", subclassChoicesGuiPresentation, AHBarbarianClassSubclassesGuid);
    //        CharacterSubclassDefinition characterSubclassDefinition = AHBarbarianSubClassPathOfTheBear.Build();
    //        featureDefinitionSubclassChoice.Subclasses.Add(characterSubclassDefinition.Name);

    //        //TODO level 3 and up features
    //    }

    //    public static void BuildAndAddClassToDB()
    //    {
    //        var barbarianClass = new AHBarbarianClassBuilder2(AHBarbarianClassName, AHBarbarianClassNameGuid).AddToDB();

    //    }
    //}
}
