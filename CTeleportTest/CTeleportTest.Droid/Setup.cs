using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.ViewModels;

namespace CTeleportTest.Droid
{
    public class Setup : MvxAppCompatSetup
    {
        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
        
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            
            typeof(Android.Support.Constraints.ConstraintLayout).Assembly,
            typeof(MvxRecyclerView).Assembly,
            typeof(MvxRecyclerViewHolder).Assembly
        };
    }
}
