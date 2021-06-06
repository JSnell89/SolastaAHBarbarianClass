using SolastaModApi;
using SolastaModApi.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using static FeatureDefinitionSavingThrowAffinity;

namespace SolastaAHBarbarianClass
{
    public static class AHBarbarianSubclassPathOfFrenzy
    {
        public static Guid AHBarbarianSubclassPathOfFrenzyGuid = new Guid("a21286d4-bb03-40ea-afa9-0ee1324a8ba4");

        const string AHBarbarianSubClassPathOfFrenzyName = "AHBarbarianSubclassPathOfFrenzy";
        private static readonly string AHBarbarianSubClassPathOfTheBearNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, AHBarbarianSubClassPathOfFrenzyName).ToString();

        public static CharacterSubclassDefinition Build()
        {
            var subclassGuiPresentation = new GuiPresentationBuilder(
                    "Subclass/&AHBarbarianSubclassPathOfFrenzyDescription",
                    "Subclass/&AHBarbarianSubclassPathOfFrenzyTitle")
                    .SetSpriteReference(DatabaseHelper.CharacterSubclassDefinitions.MartialMountaineer.GuiPresentation.SpriteReference)
                    .Build();

            CharacterSubclassDefinition definition = new CharacterSubclassDefinitionBuilder(AHBarbarianSubClassPathOfFrenzyName, AHBarbarianSubClassPathOfTheBearNameGuid)
                    .SetGuiPresentation(subclassGuiPresentation)
                    .AddFeatureAtLevel(AHBarbarianClassPathOfFrenzyRageClassPowerBuilder.RageClassPower, 3) // Special rage and increase rage count to 3
                    .AddFeatureAtLevel(AHBarbarianClassPathOfFrenzyRageClassPowerLevel6Builder.RageClassPower, 6) //Up rage count to 4 - Do in subclass since BearRage has its own characteristics
                    .AddFeatureAtLevel(AHBarbarianClassPathOfFrenzyRageClassPowerLevel9Builder.RageClassPower, 9) //Up damage on rage - Do in subclass since BearRage has its own characteristics
                    .AddFeatureAtLevel(AHBarbarianPathOfFrenzyIntimidatePowerBuilder.IntimidatePower, 10)
                    .AddToDB();

            return definition;
        }
    }

    internal class AHBarbarianClassPathOfFrenzyRageClassPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHBarbarianClassPathOfFrenzyRageClassPowerName = "AHBarbarianClassPathOfFrenzyRageClassPower";
        private static readonly string AHBarbarianClassPathOfFrenzyRageClassPowerNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, AHBarbarianClassPathOfFrenzyRageClassPowerName).ToString();

        protected AHBarbarianClassPathOfFrenzyRageClassPowerBuilder(string name, string guid) : base(AHBarbarianClassRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfFrenzyRageClassPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfFrenzyRageClassPowerDescription";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassRageClassPowerBuilder.RageClassPower);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(3); //3 uses at level 3 when this is introduced
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassPathOfFrenzyRageClassPowerTitle");

            //Create the rage
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfFrenzyRageClassConditionBuilder.PathOfFrenzyRageCondition;

            //Create the way to remove the hindered effect of ending rage
            EffectForm removeHindrancesEffect = new EffectForm();
            removeHindrancesEffect.ConditionForm = new ConditionForm();
            removeHindrancesEffect.FormType = EffectForm.EffectFormType.Condition;
            removeHindrancesEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.RemoveDetrimentalAll;
            removeHindrancesEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder.PathOfFrenzyHinderedCondition;
            removeHindrancesEffect.ConditionForm.DetrimentalConditions.Add(AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder.PathOfFrenzyHinderedCondition);

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(rageEffect);
            newEffectDescription.EffectForms.Add(removeHindrancesEffect);
            newEffectDescription.HasSavingThrow = false;
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
            newEffectDescription.DurationParameter = 1;
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
            newEffectDescription.SetCanBePlacedOnCharacter(true);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassPathOfFrenzyRageClassPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(AHBarbarianClassPathOfFrenzyRageClassPowerName, AHBarbarianClassPathOfFrenzyRageClassPowerNameGuid);
    }

    internal class AHBarbarianClassPathOfFrenzyRageClassPowerLevel6Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHBarbarianClassPathOfFrenzyRageClassPowerName = "AHBarbarianClassPathOfFrenzyRageClassPowerLevel6";
        private static readonly string AHBarbarianClassPathOfFrenzyRageClassPowerNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, AHBarbarianClassPathOfFrenzyRageClassPowerName).ToString();

        protected AHBarbarianClassPathOfFrenzyRageClassPowerLevel6Builder(string name, string guid) : base(AHBarbarianClassRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfFrenzyRageClassPowerLevel6Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfFrenzyRageClassPowerLevel6Description";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassPathOfFrenzyRageClassPowerBuilder.RageClassPower);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(4);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassPathOfFrenzyRageClassPowerLevel6Title");

            //Create the rage
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfFrenzyRageClassConditionLevel6Builder.PathOfFrenzyRageConditionLevel6;

            //Create the way to remove the hindered effect of ending rage
            EffectForm removeHindrancesEffect = new EffectForm();
            removeHindrancesEffect.ConditionForm = new ConditionForm();
            removeHindrancesEffect.FormType = EffectForm.EffectFormType.Condition;
            removeHindrancesEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.RemoveDetrimentalAll;
            removeHindrancesEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder.PathOfFrenzyHinderedCondition;
            removeHindrancesEffect.ConditionForm.DetrimentalConditions.Add(AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder.PathOfFrenzyHinderedCondition);
            removeHindrancesEffect.ConditionForm.DetrimentalConditions.Add(DatabaseHelper.ConditionDefinitions.ConditionCharmed);
            removeHindrancesEffect.ConditionForm.DetrimentalConditions.Add(DatabaseHelper.ConditionDefinitions.ConditionFrightened);

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(rageEffect);
            newEffectDescription.EffectForms.Add(removeHindrancesEffect);
            newEffectDescription.HasSavingThrow = false;
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
            newEffectDescription.DurationParameter = 1;
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
            newEffectDescription.SetCanBePlacedOnCharacter(true);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassPathOfFrenzyRageClassPowerLevel6Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(AHBarbarianClassPathOfFrenzyRageClassPowerName, AHBarbarianClassPathOfFrenzyRageClassPowerNameGuid);
    }



    internal class AHBarbarianClassPathOfFrenzyRageClassPowerLevel9Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHBarbarianClassPathOfFrenzyRageClassPowerName = "AHBarbarianClassPathOfFrenzyRageClassPowerLevel9";
        private static readonly string AHBarbarianClassPathOfFrenzyRageClassPowerNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, AHBarbarianClassPathOfFrenzyRageClassPowerName).ToString();

        protected AHBarbarianClassPathOfFrenzyRageClassPowerLevel9Builder(string name, string guid) : base(AHBarbarianClassRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfFrenzyRageClassPowerLevel9Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfFrenzyRageClassPowerLevel9Description";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassPathOfFrenzyRageClassPowerLevel6Builder.RageClassPower);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(4);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassPathOfFrenzyRageClassPowerLevel9Title");

            //Create the rage
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfFrenzyRageClassConditionLevel9Builder.PathOfFrenzyRageConditionLevel9;

            //Create the way to remove the hindered effect of ending rage
            EffectForm removeHindrancesEffect = new EffectForm();
            removeHindrancesEffect.ConditionForm = new ConditionForm();
            removeHindrancesEffect.FormType = EffectForm.EffectFormType.Condition;
            removeHindrancesEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.RemoveDetrimentalAll;
            removeHindrancesEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder.PathOfFrenzyHinderedCondition;
            removeHindrancesEffect.ConditionForm.DetrimentalConditions.Add(AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder.PathOfFrenzyHinderedCondition);
            removeHindrancesEffect.ConditionForm.DetrimentalConditions.Add(DatabaseHelper.ConditionDefinitions.ConditionCharmed);
            removeHindrancesEffect.ConditionForm.DetrimentalConditions.Add(DatabaseHelper.ConditionDefinitions.ConditionFrightened);

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(rageEffect);
            newEffectDescription.EffectForms.Add(removeHindrancesEffect);
            newEffectDescription.HasSavingThrow = false;
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
            newEffectDescription.DurationParameter = 1;
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Ally);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Self);
            newEffectDescription.SetCanBePlacedOnCharacter(true);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassPathOfFrenzyRageClassPowerLevel9Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(AHBarbarianClassPathOfFrenzyRageClassPowerName, AHBarbarianClassPathOfFrenzyRageClassPowerNameGuid);
    }


    internal class AHBarbarianPathOfFrenzyRageClassConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHBarbarianPathOfFrenzyRageClassConditionName = "AHBarbarianPathOfFrenzyRageClassCondition";
        private static readonly string AHBarbarianPathOfFrenzyRageClassConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, AHBarbarianPathOfFrenzyRageClassConditionName).ToString();

        protected AHBarbarianPathOfFrenzyRageClassConditionBuilder(string name, string guid) : base(AHBarbarianClassRageClassConditionBuilder.RageClassCondition, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfFrenzyRageClassConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfFrenzyRageClassConditionDescription";

            Definition.SetAllowMultipleInstances(false);
            //Already has the damage resist & damage features
            Definition.Features.Add(AHBarbarianPathOfFrenzyExtraAttackConditionBuilder.PathOfFrenzyExtraAttack);
            Definition.SetSubsequentOnRemoval(AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder.PathOfFrenzyHinderedCondition); //Consider a different detriment?
            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);

            Definition.SetDurationParameter(1);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfFrenzyRageClassConditionBuilder(name, guid).AddToDB();

        public static ConditionDefinition PathOfFrenzyRageCondition
            = CreateAndAddToDB(AHBarbarianPathOfFrenzyRageClassConditionName, AHBarbarianPathOfFrenzyRageClassConditionNameGuid);
    }

    internal class AHBarbarianPathOfFrenzyRageClassConditionLevel6Builder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHBarbarianPathOfFrenzyRageClassConditionName = "AHBarbarianPathOfFrenzyRageClassConditionLevel6";
        private static readonly string AHBarbarianPathOfFrenzyRageClassConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, AHBarbarianPathOfFrenzyRageClassConditionName).ToString();

        protected AHBarbarianPathOfFrenzyRageClassConditionLevel6Builder(string name, string guid) : base(AHBarbarianClassRageClassConditionBuilder.RageClassCondition, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfFrenzyRageClassConditionLevel6Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfFrenzyRageClassConditionLevel6Description";

            Definition.SetAllowMultipleInstances(false);
            //Already has the damage resist & damage features
            Definition.Features.Add(AHBarbarianPathOfFrenzyExtraAttackConditionBuilder.PathOfFrenzyExtraAttack);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionConditionAffinitys.ConditionAffinityCharmImmunity); //Add Charm/Frighten immunity
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionConditionAffinitys.ConditionAffinityFrightenedImmunity);
            Definition.SetSubsequentOnRemoval(AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder.PathOfFrenzyHinderedCondition);
            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
            Definition.SetDurationParameter(1);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfFrenzyRageClassConditionLevel6Builder(name, guid).AddToDB();

        public static ConditionDefinition PathOfFrenzyRageConditionLevel6
            = CreateAndAddToDB(AHBarbarianPathOfFrenzyRageClassConditionName, AHBarbarianPathOfFrenzyRageClassConditionNameGuid);
    }

    internal class AHBarbarianPathOfFrenzyRageClassConditionLevel9Builder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHBarbarianPathOfFrenzyRageClassConditionName = "AHBarbarianPathOfFrenzyRageClassConditionLevel9";
        private static readonly string AHBarbarianPathOfFrenzyRageClassConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, AHBarbarianPathOfFrenzyRageClassConditionName).ToString();

        protected AHBarbarianPathOfFrenzyRageClassConditionLevel9Builder(string name, string guid) : base(AHBarbarianClassRageClassConditionBuilder.RageClassCondition, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfFrenzyRageClassConditionLevel9Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfFrenzyRageClassConditionLevel9Description";

            Definition.SetAllowMultipleInstances(false);
            Definition.Features.Clear();
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityBludgeoningResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinitySlashingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPiercingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityConditionBullsStrength);
            Definition.Features.Add(AHBarbarianClassRageClassStrengthSavingThrowAffinityBuilder.RageClassStrengthSavingThrowAffinity);
            Definition.Features.Add(AHBarbarianClassRageClassDamageBonusAttackModifierLevel9Builder.RageClassDamageBonusAttackLevel9Modifier); //Level 9 damage

            Definition.Features.Add(AHBarbarianPathOfFrenzyExtraAttackConditionBuilder.PathOfFrenzyExtraAttack);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionConditionAffinitys.ConditionAffinityCharmImmunity); //Add Charm/Frighten immunity
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionConditionAffinitys.ConditionAffinityFrightenedImmunity);
            Definition.SetSubsequentOnRemoval(AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder.PathOfFrenzyHinderedCondition);

            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
            Definition.SetDurationParameter(1);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfFrenzyRageClassConditionLevel9Builder(name, guid).AddToDB();

        public static ConditionDefinition PathOfFrenzyRageConditionLevel9
            = CreateAndAddToDB(AHBarbarianPathOfFrenzyRageClassConditionName, AHBarbarianPathOfFrenzyRageClassConditionNameGuid);
    }



    internal class AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHBarbarianPathOfFrenzyRageClassHinderedConditionName = "AHBarbarianPathOfFrenzyRageClassHinderedCondition";
        private static readonly string AHBarbarianPathOfFrenzyRageClassHinderedConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, AHBarbarianPathOfFrenzyRageClassHinderedConditionName).ToString();

        protected AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder(string name, string guid) : base(DatabaseHelper.ConditionDefinitions.ConditionHindered, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfFrenzyRageClassHinderedConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfFrenzyRageClassHinderedConditionDescription";

            Definition.SetAllowMultipleInstances(false);
            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
            Definition.SetDurationParameter(10);
            Definition.SetAdditionalCondition(DatabaseHelper.ConditionDefinitions.ConditionCursedByBestowCurseStrength);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfFrenzyRageClassHinderedConditionBuilder(name, guid).AddToDB();

        public static ConditionDefinition PathOfFrenzyHinderedCondition
            = CreateAndAddToDB(AHBarbarianPathOfFrenzyRageClassHinderedConditionName, AHBarbarianPathOfFrenzyRageClassHinderedConditionNameGuid);
    }

    internal class AHBarbarianPathOfFrenzyExtraAttackConditionBuilder : BaseDefinitionBuilder<FeatureDefinitionAdditionalAction>
    {
        const string AHBarbarianPathOfFrenzyExtraAttackConditionName = "AHBarbarianPathOfFrenzyExtraAttackCondition";
        private static readonly string AHBarbarianPathOfFrenzyExtraAttackConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, "AHBarbarianPathOfFrenzyExtraAttackCondition").ToString();

        protected AHBarbarianPathOfFrenzyExtraAttackConditionBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAdditionalActions.AdditionalActionHasted, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfFrenzyExtraAttackConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfFrenzyExtraAttackConditionDescription";

            Definition.RestrictedActions.Clear();
            Definition.RestrictedActions.Add(ActionDefinitions.Id.AttackMain);
        }

        public static FeatureDefinitionAdditionalAction CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfFrenzyExtraAttackConditionBuilder(name, guid).AddToDB();

        public static FeatureDefinitionAdditionalAction PathOfFrenzyExtraAttack
            = CreateAndAddToDB(AHBarbarianPathOfFrenzyExtraAttackConditionName, AHBarbarianPathOfFrenzyExtraAttackConditionNameGuid);
    }

    internal class AHBarbarianPathOfFrenzyIntimidatePowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string IntimidatePowerName = "IntimidatePower";
        private static readonly string IntimidatePowerNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, IntimidatePowerName).ToString();

        protected AHBarbarianPathOfFrenzyIntimidatePowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&IntimidatePowerTitle";
            Definition.GuiPresentation.Description = "Feature/&IntimidatePowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.AtWill);
            Definition.SetCostPerUse(0);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Action);

            //Create the prone effect - Weirdly enough the motion form seems to also automatically apply the prone condition
            EffectForm fearEffect = new EffectForm();
            fearEffect.FormType = EffectForm.EffectFormType.Condition;
            fearEffect.ConditionForm = new ConditionForm();
            fearEffect.ConditionForm.ConditionDefinition = DatabaseHelper.ConditionDefinitions.ConditionFrightened;
            fearEffect.SavingThrowAffinity = RuleDefinitions.EffectSavingThrowType.Negates;

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(fearEffect);
            newEffectDescription.SetSavingThrowDifficultyAbility("Strength");
            newEffectDescription.SetDifficultyClassComputation(RuleDefinitions.EffectDifficultyClassComputation.AbilityScoreAndProficiency);
            newEffectDescription.SavingThrowAbility = "Wisdom";
            newEffectDescription.HasSavingThrow = true;
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Round;
            newEffectDescription.DurationParameter = 2;
            newEffectDescription.SetRangeType(RuleDefinitions.RangeType.Distance);
            newEffectDescription.SetRangeParameter(6);
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.Individuals);
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Enemy);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfFrenzyIntimidatePowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower IntimidatePower = CreateAndAddToDB(IntimidatePowerName, IntimidatePowerNameGuid);
    }
}
