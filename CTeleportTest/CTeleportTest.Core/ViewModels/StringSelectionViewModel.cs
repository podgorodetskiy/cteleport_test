using System.Collections.Generic;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CTeleportTest.Core.ViewModels
{
    public class StringSelectionViewModel : MvxViewModel<StringSelectionViewModel.InitParams, string>
    {
        private readonly IMvxNavigationService _navigationService;
        
        private List<string> _items;
        private string _selectedItem;
        private string _title;

        public StringSelectionViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string Title
        {
            get => _title;
            set { this.RaiseAndSetIfChanged(ref _title, value, () => Title); }
        }

        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItem, value, () => SelectedItem);
                if (SelectedItem != null)
                    _navigationService.Close(this, SelectedItem);
            }
        }

        public List<string> Items
        {
            get => _items;
            set { this.RaiseAndSetIfChanged(ref _items, value, () => Items); }
        }

        public override void Prepare(InitParams parameter)
        {
            Title = parameter.Title;
            SelectedItem = parameter.SelectedItem;
            Items = parameter.Items;
        }

        public class InitParams
        {
            public InitParams(string title, string selectedItem, List<string> items)
            {
                Title = title;
                SelectedItem = selectedItem;
                Items = items;
            }

            public string Title { get; }
            public string SelectedItem { get; }
            public List<string> Items { get; }
        }
    }
}