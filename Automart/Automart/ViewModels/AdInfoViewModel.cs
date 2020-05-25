using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.ViewModels
{
    public class AdInfoViewModel
    {
        public int Id { get; private set; }
        public string Label_1{ get; private set; }
        public string Label_2 { get; private set; }
        public string Label_3 { get; private set; }

        public AdInfoViewModel(AdViewModel AdVM)
        {
            this.Id = AdVM.Id;
            this.Label_1 = $"{AdVM.Mark} {AdVM.Model} ({AdVM.Power} л.с.) {AdVM.DvigType} {AdVM.DriveUnit} {AdVM.Year} год {AdVM.Mileage} км";
            this.Label_2 = $"VIN: {AdVM.VIN}";
            this.Label_3 = $"{AdVM.KPP}, {AdVM.Kuzov}, {AdVM.DvigType}, {AdVM.Volume} л., {AdVM.Power} л.с.";
        }
    }
}
