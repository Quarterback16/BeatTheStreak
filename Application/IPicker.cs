using Application.Outputs;
using System;

namespace Application
{
    public interface IPicker
    {
        BatterReport Choose(DateTime gameDate, int numberRequired);
    }
}
