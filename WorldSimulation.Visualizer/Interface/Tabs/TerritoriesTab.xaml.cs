using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using WorldSimulation.Worlds;

namespace WorldSimulation.Visualizer.Interface.Tabs
{
    /// <summary>
    /// Interaction logic for TerritoriesTab.xaml
    /// </summary>
    public partial class TerritoriesTab : UserControl
    {
        private Territory _loadedRoot;

        public TerritoriesTab()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads a territory tree structure.
        /// </summary>
        /// <param name="territoryRoot">The territory root.</param>
        public void LoadTerritoryRoot(Territory territoryRoot)
        {
            _loadedRoot = territoryRoot;
            TerritoryTreeView.Items.Clear();
            PopulateTerritory(territoryRoot, TerritoryTreeView);
        }

        private void PopulateTerritory(Territory territory, ItemsControl parent)
        {
            var item = new TreeViewItem { Header = territory.Name, Tag = territory, IsExpanded = true};
            parent.Items.Add(item);
            foreach (var child in territory.GetTerritories())
            {
                this.PopulateTerritory(child, item);
            }
        }

        private void TerritoryTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var tvi = (TreeViewItem)TerritoryTreeView.SelectedItem;
            var territory = (Territory)tvi.Tag;
        }
    }
}
