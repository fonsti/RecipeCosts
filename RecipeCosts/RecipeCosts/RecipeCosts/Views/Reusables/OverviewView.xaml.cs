using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeCosts.Views.Reusables
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewView : ContentView
    {
        public OverviewView()
        {
            InitializeComponent();
        }
    }
}