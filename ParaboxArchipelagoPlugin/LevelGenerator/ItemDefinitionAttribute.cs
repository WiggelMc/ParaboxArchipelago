using System;
using System.Collections.Generic;
using System.Reflection;

namespace ParaboxArchipelago.LevelGenerator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ItemDefinitionAttribute : GenerateAttribute.Attribute
    {
        public override void Generate(Type type)
        {
            ItemOption option = null;
            ItemProperties.Single singleItem = null;
            ItemProperties.Progressive progressiveItem = null;
            var seperateItems = new Dictionary<string, ItemProperties.Seperate>();
            foreach (
                var field 
                in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            {
                if (field.HasAttribute<ItemOptionAttribute>())
                {
                    option = (ItemOption) field.GetValue(null);
                }
                else if (field.HasAttribute<SingleItemAttribute>())
                {
                    singleItem = (ItemProperties.Single) field.GetValue(null);
                }
                else if (field.HasAttribute<ProgressiveItemAttribute>())
                {
                    progressiveItem = (ItemProperties.Progressive) field.GetValue(null);
                }
                else if (field.HasAttribute<SeperateItemAttribute>())
                {
                    seperateItems.Add(field.Name, (ItemProperties.Seperate) field.GetValue(null));
                }
            }
            
            if (singleItem == null)
                return;

            singleItem.Name ??= type.Name.PascalCaseToWordCase();
            
            if (progressiveItem == null)
                return;
            
            progressiveItem.Name ??= "Progressive " + singleItem.Name;

            foreach (var entry in seperateItems)
            {
                entry.Value.Name ??= entry.Key.PascalCaseToWordCase();
            }
        }
    }
}