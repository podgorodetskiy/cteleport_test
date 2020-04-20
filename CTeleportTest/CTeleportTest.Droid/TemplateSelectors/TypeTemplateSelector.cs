using System;
using System.Collections.Generic;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;

namespace CTeleportTest.Droid.TemplateSelectors
{
    public class TypeTemplateSelector : IMvxTemplateSelector
    {
        private readonly Dictionary<Type, int> _typeMapping;

        public TypeTemplateSelector(Dictionary<Type, int> typeMapping)
        {
            ItemTemplateId = Android.Resource.Layout.SimpleListItem1;
            _typeMapping = typeMapping;
        }

        public int GetItemViewType(object forItemObject)
        {
            try
            {
                return _typeMapping[forItemObject.GetType()];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ItemTemplateId;
            }
        }

        public int GetItemLayoutId(int fromViewType)
        {
            return fromViewType;
        }

        public int ItemTemplateId { get; set; }
    }
}