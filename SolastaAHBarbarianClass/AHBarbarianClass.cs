using SolastaModApi;
using SolastaModApi.Extensions;
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
            //Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(AHBarbarianClassRageClassPowerAdditionalUse2Builder.RageClassPower, 6)); //Add additional uses through subclasses since most times they alter the rage power anyways.

            //SubclassFeature at level 6
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionCombatAffinitys.CombatAffinityEagerForBattle, 7));
            //Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionCampAffinitys.CampAffinityFeatFocusedSleeper, 7)); //Helps not be surprised in camp I guess?  Could add the full Oblivion domain thing maybe.

            //ADV on init - Also should add can't get surpised if you rage but no idea if that's possible.  Could add the no surprise in camp affinity?
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionFeatureSets.FeatureSetAbilityScoreChoice, 8));
            Definition.FeatureUnlocks.Add(new FeatureUnlockByLevel(DatabaseHelper.FeatureDefinitionAttributeModifiers.AttributeModifierMartialChampionImprovedCritical, 9)); //Ideally we could add extra damage on crit but I don't think that's possible

            var subclassChoicesGuiPresentation = new GuiPresentation();
            subclassChoicesGuiPresentation.Title = "Feature/&AHBarbarianSubclassPathTitle";
            subclassChoicesGuiPresentation.Description = "Class/&AHBarbarianSubclassPathDescription";
            FeatureDefinitionSubclassChoice featureDefinitionSubclassChoice = this.BuildSubclassChoice(3, "Path", false, "SubclassChoiceBarbarianSpecialistArchetypes", subclassChoicesGuiPresentation, AHBarbarianClassSubclassesGuid);
            CharacterSubclassDefinition characterSubclassDefinition = AHBarbarianSubClassPathOfTheBear.Build();
            featureDefinitionSubclassChoice.Subclasses.Add(characterSubclassDefinition.Name);
        }

        public static void BuildAndAddClassToDB()
        {
            var barbarianClass = new AHBarbarianClassBuilder(AHBarbarianClassName, AHBarbarianClassNameGuid).AddToDB();
            
        }
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
                    .AddFeatureAtLevel(DatabaseHelper.FeatureDefinitionPowers.PowerReckless, 3)
                    .AddFeatureAtLevel(AHBarbarianClassPathOfBearRageClassPowerBuilder.RageClassPower, 3)
                    .AddFeatureAtLevel(AHBarbarianClassFastMovementMovementAffinityBuilder.FastMovementMovementAffinity, 7)
                    .AddFeatureAtLevel(AHBarbarianClassFastMovementMovementPowerForLevelUpDescriptionBuilder.FastMovementMovementPowerForLevelUpDescription, 7)
                    .AddFeatureAtLevel(DatabaseHelper.FeatureDefinitionAttributeModifiers.AttributeModifierMartialChampionImprovedCritical, 10)
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
            var strengthSaveAffinityGroup = new SavingThrowAffinityGroup();
            strengthSaveAffinityGroup.affinity = RuleDefinitions.CharacterSavingThrowAffinity.Advantage;
            strengthSaveAffinityGroup.abilityScoreName = "Dexterity";
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

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.None); //Short rest for the subclass
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

    internal class AHBarbarianClassRageClassPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RageClassPowerName = "AHBarbarianClassRageClassPower";
        const string RageClassPowerNameGuid = "1fe90786-46cf-407d-b315-1a4622c2aca7";

        protected AHBarbarianClassRageClassPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerDomainElementalFireBurst, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassPowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest); //Short rest for the subclass
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

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest); //Short rest for the subclass
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
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassConditionDescription";

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
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassConditionDescription";

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
}
