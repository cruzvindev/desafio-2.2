using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.models;

public class Consulta
{
    public string CPFPaciente { get; set; }
    public DateTime DataConsulta { get; set; }
    public TimeSpan HoraInicial { get; set; }
    public TimeSpan HoraFinal { get; set; }

    public Consulta(string CPF, DateTime dataConsulta, TimeSpan horaInicial, TimeSpan horaFinal)
    {
        this.CPFPaciente = CPF;
        this.DataConsulta = dataConsulta;
        this.HoraInicial =  horaInicial;
        this.HoraFinal = horaFinal;
    }

    public Consulta(string CPF, DateTime dataConsulta, TimeSpan horaInicial)
    {
        this.CPFPaciente= CPF;
        this.DataConsulta= dataConsulta;
        this.HoraInicial= horaInicial;
    }

    public Consulta() { }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is not Consulta) return false;

        var other = (Consulta)obj;
        return HoraInicial.Equals(other.HoraInicial);
    }

    public override int GetHashCode()
    {
        return HoraInicial.GetHashCode();
    }
}
