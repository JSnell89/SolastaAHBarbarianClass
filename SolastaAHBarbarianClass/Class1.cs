using SolastaModApi;
using SolastaModApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolastaAHBarbarianClass
{
    //To be deleted:
    internal class AHBarbarianClassRageClassPowerAdditionalUse1Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RageClassPowerName = "AHBarbarianClassRageClassPower";
        const string RageClassPowerNameGuid = "c3161de8-853d-4566-923b-56687a91d918";

        protected AHBarbarianClassRageClassPowerAdditionalUse1Builder(string name, string guid) : base(AHBarbarianClassPathOfBearRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.SetFixedUsesPerRecharge(4);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassRageClassPowerTitle");
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassRageClassPowerAdditionalUse1Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(RageClassPowerName, RageClassPowerNameGuid);
    }

    internal class AHBarbarianClassRageClassPowerAdditionalUse2Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RageClassPowerName = "AHBarbarianClassRageClassPower";
        const string RageClassPowerNameGuid = "4b980e9f-25d7-4358-9c39-1a685b4c8dd8";

        protected AHBarbarianClassRageClassPowerAdditionalUse2Builder(string name, string guid) : base(AHBarbarianClassRageClassPowerBuilder.RageClassPower, name, guid)
        {
            Definition.GuiPresentation.Title = "Feature/&AHBarbarianClassRageClassPowerAdditionalUseTitle";
            Definition.GuiPresentation.Description = "Feature/&AHBarbarianClassRageClassPowerAdditionalUseDescription";
            Definition.SetFixedUsesPerRecharge(1);
            Definition.SetShortTitleOverride("Feature/&AHBarbarianClassRageClassPowerTitle");
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassRageClassPowerAdditionalUse2Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(RageClassPowerName, RageClassPowerNameGuid);
    }

    internal class AHBarbarianClassPathOfBearRageClassPowerAdditionalLevel9Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RageClassPowerName = "AHBarbarianClassPathOfBearRageClassPower";
        const string RageClassPowerNameGuid = "df8b31a9-ee2b-49dc-a614-3d74dca798bf";

        protected AHBarbarianClassPathOfBearRageClassPowerAdditionalLevel9Builder(string name, string guid) : base(AHBarbarianClassPathOfBearRageClassPowerAdditionalLevel6Builder.RageClassPower, name, guid)
        {
            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassPathOfBearRageClassPowerAdditionalLevel6Builder.RageClassPower);
            Definition.SetFixedUsesPerRecharge(4);

            //Create the power attack effect
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
            => new AHBarbarianClassPathOfBearRageClassPowerAdditionalLevel9Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(RageClassPowerName, RageClassPowerNameGuid);
    }




    internal class AHBarbarianClassPathOfBearRageClassPowerAdditionalLevel6Builder : BaseDefinitionBuilder<FeatureDefinitionPower>
    {
        const string RageClassPowerName = "AHBarbarianClassPathOfBearRageClassPower";
        const string RageClassPowerNameGuid = "d08d334c-c05d-4177-86c8-43fb947323d8";

        protected AHBarbarianClassPathOfBearRageClassPowerAdditionalLevel6Builder(string name, string guid) : base(AHBarbarianClassPathOfBearRageClassPowerBuilder.RageClassPower, name, guid)
        {
            FeatureDefinitionPowerExtensions.SetOverriddenPower(Definition, AHBarbarianClassPathOfBearRageClassPowerBuilder.RageClassPower);
            Definition.SetFixedUsesPerRecharge(4);
        }

        public static FeatureDefinitionPower CreateAndAddToDB(string name, string guid)
            => new AHBarbarianClassPathOfBearRageClassPowerAdditionalLevel6Builder(name, guid).AddToDB();

        public static FeatureDefinitionPower RageClassPower
            = CreateAndAddToDB(RageClassPowerName, RageClassPowerNameGuid);
    }
}
