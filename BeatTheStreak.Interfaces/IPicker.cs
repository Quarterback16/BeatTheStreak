using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
    public interface IPicker
    {
		BatterReport Choose(DateTime gameDate, int numberRequired);
		IPickerOptions GetOptions();
    }
}
