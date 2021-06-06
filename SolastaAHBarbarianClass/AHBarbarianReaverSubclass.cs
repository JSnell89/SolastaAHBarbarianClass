using SolastaModApi;
using SolastaModApi.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using static FeatureDefinitionSavingThrowAffinity;

namespace SolastaAHBarbarianClass
{
    public static class AHBarbarianSubclassPathOfTheReaver
    {
        public static Guid AHBarbarianSubclassPathOfTheReaverGuid = new Guid("a21286d4-bb03-40ea-afa9-0ee1324a8ba4");

        const string AHBarbarianSubClassPathOfTheReaverName = "AHBarbarianSubclassPathOfTheReaver";
        private static readonly string AHBarbarianSubClassPathOfTheBearNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianSubClassPathOfTheReaverName).ToString();

        public static CharacterSubclassDefinition Build()
        {
            var subclassGuiPresentation = new GuiPresentationBuilder(
                    "Subclass/&AHBarbarianSubclassPathOfTheReaverDescription",
                    "Subclass/&AHBarbarianSubclassPathOfTheReaverTitle")
                    .SetSpriteReference(DatabaseHelper.CharacterSubclassDefinitions.RoguishDarkweaver.GuiPresentation.SpriteReference)
                    .Build();

            CharacterSubclassDefinition definition = new CharacterSubclassDefinitionBuilder(AHBarbarianSubClassPathOfTheReaverName, AHBarbarianSubClassPathOfTheBearNameGuid)
                    .SetGuiPresentation(subclassGuiPresentation)
                    .AddFeatureAtLevel(AHBarbarianClassPathOfTheReaverRageClassPowerBuilder.RageClassPower, 3) // Special rage and increase rage count to 3 - D4 necrotic damage per attack (very similar damage to the 5e Zealot if multiple attacks hit - This could be changed to once per turn instead like ColossusSlayer though, but this opens up for synergy with the level 6 feature of extra attack on kill!)
                    .AddFeatureAtLevel(AHBarbarianClassPathOfTheReaverRageClassPowerLevel6Builder.RageClassPower, 6) //Up rage count to 4 - Increase Necrotic damage to d6
                    .AddFeatureAtLevel(AHBarbarianPathOfTheReaverExtraAttackOnKillBuilder.PathOfTheReaverExtraAttackOnKill, 6)
                    .AddFeatureAtLevel(AHBarbarianClassPathOfTheReaverRageClassPowerLevel9Builder.RageClassPower, 9) //Up base damage on rage
                    .AddFeatureAtLevel(AHBarbarianClassPathOfTheReaverRageClassPowerLevel10Builder.RageClassPower, 10)//Increase Necrotic damage to d8
                    .AddFeatureAtLevel(AHBarbarianPathOfTheReaverDeathsFearPowerBuilder.DeathsFear, 10) //Extra feature Deaths Fear
                    .AddToDB();

            return definition;
        }
    }

    internal class AHBarbarianClassPathOfTheReaverRageClassPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHBarbarianClassPathOfTheReaverRageClassPowerName = "AHBarbarianClassPathOfTheReaverRageClassPower";
        private static readonly string AHBarbarianClassPathOfTheReaverRageClassPowerNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianClassPathOfTheReaverRageClassPowerName).ToString();

        protected AHBarbarianClassPathOfTheReaverRageClassPowerBuilder(string name, string guid) : base(AHBarbarianClassRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerDescription";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassRageClassPowerBuilder.RageClassPower);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(3); //3 uses at level 3 when this is introduced
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerTitle");

            //Create the rage
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfTheReaverRageClassConditionBuilder.PathOfTheReaverRageCondition;

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
            => new AHBarbarianClassPathOfTheReaverRageClassPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(AHBarbarianClassPathOfTheReaverRageClassPowerName, AHBarbarianClassPathOfTheReaverRageClassPowerNameGuid);
    }

    internal class AHBarbarianClassPathOfTheReaverRageClassPowerLevel6Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHBarbarianClassPathOfTheReaverRageClassPowerName = "AHBarbarianClassPathOfTheReaverRageClassPowerLevel6";
        private static readonly string AHBarbarianClassPathOfTheReaverRageClassPowerNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianClassPathOfTheReaverRageClassPowerName).ToString();

        protected AHBarbarianClassPathOfTheReaverRageClassPowerLevel6Builder(string name, string guid) : base(AHBarbarianClassRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerLevel6Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerLevel6Description";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassPathOfTheReaverRageClassPowerBuilder.RageClassPower);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(4);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerLevel6Title");

            //Create the rage
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfTheReaverRageClassConditionLevel6Builder.PathOfTheReaverRageConditionLevel6;

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
            => new AHBarbarianClassPathOfTheReaverRageClassPowerLevel6Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(AHBarbarianClassPathOfTheReaverRageClassPowerName, AHBarbarianClassPathOfTheReaverRageClassPowerNameGuid);
    }

    internal class AHBarbarianClassPathOfTheReaverRageClassPowerLevel9Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHBarbarianClassPathOfTheReaverRageClassPowerName = "AHBarbarianClassPathOfTheReaverRageClassPowerLevel9";
        private static readonly string AHBarbarianClassPathOfTheReaverRageClassPowerNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianClassPathOfTheReaverRageClassPowerName).ToString();

        protected AHBarbarianClassPathOfTheReaverRageClassPowerLevel9Builder(string name, string guid) : base(AHBarbarianClassRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerLevel9Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerLevel9Description";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassPathOfTheReaverRageClassPowerLevel6Builder.RageClassPower);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(4);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerLevel9Title");

            //Create the rage
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfTheReaverRageClassConditionLevel9Builder.PathOfTheReaverRageConditionLevel9; //Add extra 1 rage damage from base class

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
            => new AHBarbarianClassPathOfTheReaverRageClassPowerLevel9Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(AHBarbarianClassPathOfTheReaverRageClassPowerName, AHBarbarianClassPathOfTheReaverRageClassPowerNameGuid);
    }

    internal class AHBarbarianClassPathOfTheReaverRageClassPowerLevel10Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string AHBarbarianClassPathOfTheReaverRageClassPowerName = "AHBarbarianClassPathOfTheReaverRageClassPowerLevel10";
        private static readonly string AHBarbarianClassPathOfTheReaverRageClassPowerNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianClassPathOfTheReaverRageClassPowerName).ToString();

        protected AHBarbarianClassPathOfTheReaverRageClassPowerLevel10Builder(string name, string guid) : base(AHBarbarianClassRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerLevel10Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerLevel10Description";

            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassPathOfTheReaverRageClassPowerLevel9Builder.RageClassPower);

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.BonusAction);
            Definition.SetCostPerUse(1);
            Definition.SetFixedUsesPerRecharge(4);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassPathOfTheReaverRageClassPowerLevel10Title");

            //Create the rage
            EffectForm rageEffect = new EffectForm();
            rageEffect.ConditionForm = new ConditionForm();
            rageEffect.FormType = EffectForm.EffectFormType.Condition;
            rageEffect.ConditionForm.Operation = ConditionForm.ConditionOperation.Add;
            rageEffect.ConditionForm.ConditionDefinition = AHBarbarianPathOfTheReaverRageClassConditionLevel10Builder.PathOfTheReaverRageConditionLevel10;//Condition doesn't change at level 10

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
            => new AHBarbarianClassPathOfTheReaverRageClassPowerLevel10Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(AHBarbarianClassPathOfTheReaverRageClassPowerName, AHBarbarianClassPathOfTheReaverRageClassPowerNameGuid);
    }


    internal class AHBarbarianPathOfTheReaverRageClassConditionBuilder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHBarbarianPathOfTheReaverRageClassConditionName = "AHBarbarianPathOfTheReaverRageClassCondition";
        private static readonly string AHBarbarianPathOfTheReaverRageClassConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianPathOfTheReaverRageClassConditionName).ToString();

        protected AHBarbarianPathOfTheReaverRageClassConditionBuilder(string name, string guid) : base(AHBarbarianClassRageClassConditionBuilder.RageClassCondition, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfTheReaverRageClassConditionTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfTheReaverRageClassConditionDescription";

            Definition.SetAllowMultipleInstances(false);
            //Already has the base damage resist & damage features
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityNecroticResistance);
            Definition.Features.Add(AHBarbarianPathOfTheReaverExtraDamageBuilder.PathOfTheReaverExtraNecroticDamage);
            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);

            Definition.SetDurationParameter(1);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheReaverRageClassConditionBuilder(name, guid).AddToDB();

        public static ConditionDefinition PathOfTheReaverRageCondition
            = CreateAndAddToDB(AHBarbarianPathOfTheReaverRageClassConditionName, AHBarbarianPathOfTheReaverRageClassConditionNameGuid);
    }

    internal class AHBarbarianPathOfTheReaverRageClassConditionLevel6Builder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHBarbarianPathOfTheReaverRageClassConditionName = "AHBarbarianPathOfTheReaverRageClassConditionLevel6";
        private static readonly string AHBarbarianPathOfTheReaverRageClassConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianPathOfTheReaverRageClassConditionName).ToString();

        protected AHBarbarianPathOfTheReaverRageClassConditionLevel6Builder(string name, string guid) : base(AHBarbarianClassRageClassConditionBuilder.RageClassCondition, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfTheReaverRageClassConditionLevel6Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfTheReaverRageClassConditionLevel6Description";

            Definition.SetAllowMultipleInstances(false);
            //Already has the base damage resist & damage features
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityNecroticResistance);
            Definition.Features.Add(AHBarbarianPathOfTheReaverExtraDamageLevel6Builder.PathOfTheReaverExtraNecroticDamageLevel6);
            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);

            Definition.SetDurationParameter(1);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheReaverRageClassConditionLevel6Builder(name, guid).AddToDB();

        public static ConditionDefinition PathOfTheReaverRageConditionLevel6
            = CreateAndAddToDB(AHBarbarianPathOfTheReaverRageClassConditionName, AHBarbarianPathOfTheReaverRageClassConditionNameGuid);
    }

    internal class AHBarbarianPathOfTheReaverRageClassConditionLevel9Builder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHBarbarianPathOfTheReaverRageClassConditionName = "AHBarbarianPathOfTheReaverRageClassConditionLevel9";
        private static readonly string AHBarbarianPathOfTheReaverRageClassConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianPathOfTheReaverRageClassConditionName).ToString();

        protected AHBarbarianPathOfTheReaverRageClassConditionLevel9Builder(string name, string guid) : base(AHBarbarianClassRageClassConditionBuilder.RageClassCondition, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfTheReaverRageClassConditionLevel9Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfTheReaverRageClassConditionLevel9Description";

            Definition.SetAllowMultipleInstances(false);
            Definition.Features.Clear();
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityBludgeoningResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinitySlashingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPiercingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityConditionBullsStrength);
            Definition.Features.Add(AHBarbarianClassRageClassStrengthSavingThrowAffinityBuilder.RageClassStrengthSavingThrowAffinity);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityNecroticResistance);
            Definition.Features.Add(AHBarbarianClassRageClassDamageBonusAttackModifierLevel9Builder.RageClassDamageBonusAttackLevel9Modifier); //Level 9 base rage damage
            Definition.Features.Add(AHBarbarianPathOfTheReaverExtraDamageLevel6Builder.PathOfTheReaverExtraNecroticDamageLevel6);

            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
            Definition.SetDurationParameter(1);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheReaverRageClassConditionLevel9Builder(name, guid).AddToDB();

        public static ConditionDefinition PathOfTheReaverRageConditionLevel9
            = CreateAndAddToDB(AHBarbarianPathOfTheReaverRageClassConditionName, AHBarbarianPathOfTheReaverRageClassConditionNameGuid);
    }

    internal class AHBarbarianPathOfTheReaverRageClassConditionLevel10Builder : BaseDefinitionBuilder<ConditionDefinition>
    {
        const string AHBarbarianPathOfTheReaverRageClassConditionName = "AHBarbarianPathOfTheReaverRageClassConditionLevel10";
        private static readonly string AHBarbarianPathOfTheReaverRageClassConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianPathOfTheReaverRageClassConditionName).ToString();

        protected AHBarbarianPathOfTheReaverRageClassConditionLevel10Builder(string name, string guid) : base(AHBarbarianClassRageClassConditionBuilder.RageClassCondition, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfTheReaverRageClassConditionLevel10Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfTheReaverRageClassConditionLevel10Description";

            Definition.SetAllowMultipleInstances(false);
            Definition.Features.Clear();
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityBludgeoningResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinitySlashingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityPiercingResistance);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityConditionBullsStrength);
            Definition.Features.Add(AHBarbarianClassRageClassStrengthSavingThrowAffinityBuilder.RageClassStrengthSavingThrowAffinity);
            Definition.Features.Add(DatabaseHelper.FeatureDefinitionDamageAffinitys.DamageAffinityNecroticResistance);
            Definition.Features.Add(AHBarbarianClassRageClassDamageBonusAttackModifierLevel9Builder.RageClassDamageBonusAttackLevel9Modifier); //Level 9 base rage damage
            Definition.Features.Add(AHBarbarianPathOfTheReaverExtraDamageLevel10Builder.PathOfTheReaverExtraNecroticDamageLevel10);

            Definition.SetDurationType(RuleDefinitions.DurationType.Minute);
            Definition.SetDurationParameter(1);
        }

        public static ConditionDefinition CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheReaverRageClassConditionLevel10Builder(name, guid).AddToDB();

        public static ConditionDefinition PathOfTheReaverRageConditionLevel10
            = CreateAndAddToDB(AHBarbarianPathOfTheReaverRageClassConditionName, AHBarbarianPathOfTheReaverRageClassConditionNameGuid);
    }

    internal class AHBarbarianPathOfTheReaverExtraAttackOnKillBuilder : BaseDefinitionBuilder<FeatureDefinitionAdditionalAction>
    {
        const string AHBarbarianPathOfTheReaverExtraAttackConditionName = "AHBarbarianPathOfTheReaverExtraAttackCondition";
        private static readonly string AHBarbarianPathOfTheReaverExtraAttackConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianPathOfTheReaverExtraAttackConditionName).ToString();

        protected AHBarbarianPathOfTheReaverExtraAttackOnKillBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAdditionalActions.AdditionalActionHunterHordeBreaker, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfTheReaverExtraAttackOnKillTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfTheReaverExtraAttackOnKillDescription";

            Definition.RestrictedActions.Clear();
        }

        public static FeatureDefinitionAdditionalAction CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheReaverExtraAttackOnKillBuilder(name, guid).AddToDB();

        public static FeatureDefinitionAdditionalAction PathOfTheReaverExtraAttackOnKill
            = CreateAndAddToDB(AHBarbarianPathOfTheReaverExtraAttackConditionName, AHBarbarianPathOfTheReaverExtraAttackConditionNameGuid);
    }

    internal class AHBarbarianPathOfTheReaverDeathsFearPowerBuilder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string IntimidatePowerName = "AHBarbarianPathOfTheReaverDeathsFearPower";
        private static readonly string IntimidatePowerNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfFrenzy.AHBarbarianSubclassPathOfFrenzyGuid, IntimidatePowerName).ToString();

        protected AHBarbarianPathOfTheReaverDeathsFearPowerBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionPowers.PowerFighterSecondWind, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfTheReaverDeathsFearPowerTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfTheReaverDeathsFearPowerDescription";

            Definition.SetRechargeRate(RuleDefinitions.RechargeRate.LongRest);
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetCostPerUse(1);
            Definition.SetActivationTime(RuleDefinitions.ActivationTime.Action);

            //Create the prone effect - Weirdly enough the motion form seems to also automatically apply the prone condition
            EffectForm fearEffect = new EffectForm();
            fearEffect.FormType = EffectForm.EffectFormType.Condition;
            fearEffect.ConditionForm = new ConditionForm();
            fearEffect.ConditionForm.ConditionDefinition = DatabaseHelper.ConditionDefinitions.ConditionFrightened;
            fearEffect.SavingThrowAffinity = RuleDefinitions.EffectSavingThrowType.Negates;
            fearEffect.SaveOccurence = RuleDefinitions.TurnOccurenceType.EndOfTurn;
            fearEffect.CanSaveToCancel = true;

            //Add to our new effect
            EffectDescription newEffectDescription = new EffectDescription();
            newEffectDescription.Copy(Definition.EffectDescription);
            newEffectDescription.EffectForms.Clear();
            newEffectDescription.EffectForms.Add(fearEffect);
            newEffectDescription.SetSavingThrowDifficultyAbility("Strength");
            newEffectDescription.SetDifficultyClassComputation(RuleDefinitions.EffectDifficultyClassComputation.AbilityScoreAndProficiency);
            newEffectDescription.SavingThrowAbility = "Wisdom";
            newEffectDescription.HasSavingThrow = true;
            newEffectDescription.DurationType = RuleDefinitions.DurationType.Minute;
            newEffectDescription.DurationParameter = 1;
            newEffectDescription.SetTargetType(RuleDefinitions.TargetType.PerceivingWithinDistance);
            newEffectDescription.SetTargetProximityDistance(30);
            newEffectDescription.SetTargetParameter(6);
            newEffectDescription.SetTargetSide(RuleDefinitions.Side.Enemy);

            Definition.SetEffectDescription(newEffectDescription);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheReaverDeathsFearPowerBuilder(name, guid).AddToDB();

        public static FeatureDefinitionPower DeathsFear = CreateAndAddToDB(IntimidatePowerName, IntimidatePowerNameGuid);
    }

    internal class AHBarbarianPathOfTheReaverExtraDamageBuilder : BaseDefinitionBuilder<FeatureDefinitionAdditionalDamage>
    {
        const string AHBarbarianPathOfTheReaverExtraAttackConditionName = "AHBarbarianPathOfTheReaverExtraDamage";
        private static readonly string AHBarbarianPathOfTheReaverExtraAttackConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianPathOfTheReaverExtraAttackConditionName).ToString();

        protected AHBarbarianPathOfTheReaverExtraDamageBuilder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAdditionalDamages.AdditionalDamageHunterColossusSlayer, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfTheReaverExtraDamageTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfTheReaverExtraDamageDescription";
            Definition.SetCachedName(Definition.GuiPresentation.Title);
            Definition.SetNotificationTag("DeathsTouch");

            Definition.SetTriggerCondition(RuleDefinitions.AdditionalDamageTriggerCondition.AlwaysActive);
            Definition.SetDamageDieType(RuleDefinitions.DieType.D4);
            Definition.SetAdditionalDamageType(RuleDefinitions.AdditionalDamageType.Specific);
            Definition.SetSpecificDamageType("DamageNecrotic");
            Definition.SetLimitedUsage(RuleDefinitions.FeatureLimitedUsage.None);
        }

        public static FeatureDefinitionAdditionalDamage CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheReaverExtraDamageBuilder(name, guid).AddToDB();

        public static FeatureDefinitionAdditionalDamage PathOfTheReaverExtraNecroticDamage
            = CreateAndAddToDB(AHBarbarianPathOfTheReaverExtraAttackConditionName, AHBarbarianPathOfTheReaverExtraAttackConditionNameGuid);
    }

    internal class AHBarbarianPathOfTheReaverExtraDamageLevel6Builder : BaseDefinitionBuilder<FeatureDefinitionAdditionalDamage>
    {
        const string AHBarbarianPathOfTheReaverExtraAttackConditionName = "AHBarbarianPathOfTheReaverExtraDamageLevel6";
        private static readonly string AHBarbarianPathOfTheReaverExtraAttackConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianPathOfTheReaverExtraAttackConditionName).ToString();

        protected AHBarbarianPathOfTheReaverExtraDamageLevel6Builder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAdditionalDamages.AdditionalDamageHunterColossusSlayer, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfTheReaverExtraDamageLevel6Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfTheReaverExtraDamageLevel6Description";
            Definition.SetCachedName(Definition.GuiPresentation.Title);
            Definition.SetNotificationTag("DeathsTouch");

            Definition.SetTriggerCondition(RuleDefinitions.AdditionalDamageTriggerCondition.AlwaysActive);
            Definition.SetDamageDieType(RuleDefinitions.DieType.D6);
            Definition.SetAdditionalDamageType(RuleDefinitions.AdditionalDamageType.Specific);
            Definition.SetSpecificDamageType("DamageNecrotic");
            Definition.SetLimitedUsage(RuleDefinitions.FeatureLimitedUsage.None);
        }

        public static FeatureDefinitionAdditionalDamage CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheReaverExtraDamageLevel6Builder(name, guid).AddToDB();

        public static FeatureDefinitionAdditionalDamage PathOfTheReaverExtraNecroticDamageLevel6
            = CreateAndAddToDB(AHBarbarianPathOfTheReaverExtraAttackConditionName, AHBarbarianPathOfTheReaverExtraAttackConditionNameGuid);
    }

    internal class AHBarbarianPathOfTheReaverExtraDamageLevel10Builder : BaseDefinitionBuilder<FeatureDefinitionAdditionalDamage>
    {
        const string AHBarbarianPathOfTheReaverExtraAttackConditionName = "AHBarbarianPathOfTheReaverExtraDamageLevel10";
        private static readonly string AHBarbarianPathOfTheReaverExtraAttackConditionNameGuid = GuidHelper.Create(AHBarbarianSubclassPathOfTheReaver.AHBarbarianSubclassPathOfTheReaverGuid, AHBarbarianPathOfTheReaverExtraAttackConditionName).ToString();

        protected AHBarbarianPathOfTheReaverExtraDamageLevel10Builder(string name, string guid) : base(DatabaseHelper.FeatureDefinitionAdditionalDamages.AdditionalDamageHunterColossusSlayer, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianPathOfTheReaverExtraDamageLevel10Title";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianPathOfTheReaverExtraDamageLevel10Description";
            Definition.SetCachedName(Definition.GuiPresentation.Title);
            Definition.SetNotificationTag("DeathsTouch");

            Definition.SetTriggerCondition(RuleDefinitions.AdditionalDamageTriggerCondition.AlwaysActive);
            Definition.SetDamageDieType(RuleDefinitions.DieType.D8);
            Definition.SetAdditionalDamageType(RuleDefinitions.AdditionalDamageType.Specific);
            Definition.SetSpecificDamageType("DamageNecrotic");
            Definition.SetLimitedUsage(RuleDefinitions.FeatureLimitedUsage.None);
        }

        public static FeatureDefinitionAdditionalDamage CreateAndAddToDB(string name, string guid)
            => new AHBarbarianPathOfTheReaverExtraDamageLevel10Builder(name, guid).AddToDB();

        public static FeatureDefinitionAdditionalDamage PathOfTheReaverExtraNecroticDamageLevel10
            = CreateAndAddToDB(AHBarbarianPathOfTheReaverExtraAttackConditionName, AHBarbarianPathOfTheReaverExtraAttackConditionNameGuid);
    }
}
