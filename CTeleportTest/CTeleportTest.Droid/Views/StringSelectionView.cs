using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.Design;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace CTeleportTest.Droid.Views
{
    [MvxDialogFragmentPresentation]
    public class StringSelectionView : MvxBottomSheetDialogFragment<Core.ViewModels.StringSelectionViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.StringSelectionView, null);
            return view;
        }
    }
}