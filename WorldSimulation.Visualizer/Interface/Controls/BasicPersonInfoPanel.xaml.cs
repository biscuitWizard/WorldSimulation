using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldSimulation.Caches;
using WorldSimulation.Entities;

namespace WorldSimulation.Visualizer.Interface.Controls
{
    /// <summary>
    /// Interaction logic for BasicPersonInfoPanel.xaml
    /// </summary>
    public partial class BasicPersonInfoPanel : UserControl
    {
        public event EventHandler<ulong> NavigatePersonRequest;

        public BasicPersonInfoPanel()
        {
            InitializeComponent();
        }

        public void LoadPerson(Person person)
        {
            Clear();

            FirstNameLastNameBlock.Text = string.Format("{0}, {1}", person.FirstName, person.FamilyName);
            GenderBlock.Text = person.Gender;
            SexBlock.Text = person.Sex;
            AgeBlock.Text = person.Age.ToString();
            if (person.Partner != null
                && person.Partner.Id.HasValue)
            {
                var hyperlink = new Hyperlink();
                hyperlink.RequestNavigate += HyperlinkOnRequestNavigate;
                hyperlink.Inlines.Add(person.Partner.Name);
                hyperlink.NavigateUri = new Uri("person://" + person.Partner.Id.Value);
                SpouseBlock.Inlines.Add(hyperlink);
            }
            else
            {
                SpouseBlock.Text = "Single";
            }
            ProfessionBlock.Text = person.Profession != null
                ? string.Format("{0} at {1}", person.Profession.Title, person.Profession.Company)
                : "Unemployed";

            int index = 0;
            foreach (var child in person.Children)
            {
                var hyperlink = new Hyperlink();
                hyperlink.RequestNavigate += HyperlinkOnRequestNavigate;
                hyperlink.Inlines.Add(child.Name);
                ChildrenBlock.Inlines.Add(hyperlink);
                if (person.Children.Count > index + 1)
                {
                    ChildrenBlock.Inlines.Add(", ");
                }

                hyperlink.NavigateUri = new Uri("person://" + child.Id.Value);
                
                index++;
            }

            foreach (var flag in person.GetFlags())
            {
                FlagsBlock.Inlines.Add(flag.Name + Environment.NewLine);
            }
        }

        private void HyperlinkOnRequestNavigate(object sender, RequestNavigateEventArgs requestNavigateEventArgs)
        {
            var id = ulong.Parse(requestNavigateEventArgs.Uri.Host);

            if (NavigatePersonRequest != null)
            {
                NavigatePersonRequest.Invoke(this, id);
            }
        }

        public void Clear()
        {
            FirstNameLastNameBlock.Text = string.Empty;
            GenderBlock.Text = string.Empty;
            SexBlock.Text = string.Empty;
            AgeBlock.Text = string.Empty;
            SpouseBlock.Inlines.Clear();
            ProfessionBlock.Text = string.Empty;
            ChildrenBlock.Inlines.Clear();
            FlagsBlock.Inlines.Clear();
        }
    }
}
