using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Helpers;
using WeatherApp.Model;

namespace WeatherApp.ViewModels
{
    public class FormViewModel : BaseViewModel
    {
      private string _name;
      private string _description;
      private string _grade;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public string Grade
        {
            get { return _grade; }
            set
            {
                _grade = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveData { get; set; }

        public FormViewModel()
        {
            SaveData = new Command(SaveForm);
        }

        public void SaveForm()
        {
            var review = new Review()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Description = Description,
                Grade = Grade
            };

            DataHelper dt = new DataHelper();
            dt.Save(review);
        }
    }
}
