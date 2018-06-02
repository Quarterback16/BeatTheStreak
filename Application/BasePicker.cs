using BeatTheStreak.Interfaces;
using System.Collections.Generic;

namespace Application
{
    public class BasePicker
    {
        protected readonly ILineupRepository _lineupRepository;
        protected readonly IPitcherRepository _pitcherRepository;

        public string PickerName { get; set; }

		public IPickerOptions PickerOptions { get; set; }
        public BasePicker(
            ILineupRepository lineupRepository,
            IPitcherRepository pitcherRepository)
        {
            _lineupRepository = lineupRepository;
            _pitcherRepository = pitcherRepository;
            PickerOptions = new PickerOptions(new Dictionary<string, string>());
        }

    }
}
