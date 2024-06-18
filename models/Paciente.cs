using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.models;

public class Paciente : IComparable<Paciente>
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    //public List<Consulta> Consultas { get; set; } = new List<Consulta>();

    public Paciente(string Nome, string CPF, DateTime DataNascimento)
    {
        this.Nome = Nome;
        this.CPF = CPF;
        this.DataNascimento = DataNascimento;
    }

    public Paciente() { }

    public int CompareTo(Paciente other)
    {
        if (other == null) return 1;
        return string.Compare(this.CPF, other.CPF, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is not Paciente) return false;

        var other = (Paciente)obj;
        return CPF.Equals(other.CPF);
    }

    public override int GetHashCode()
    {
        return CPF.GetHashCode();
    }

    public int GetIdade()
    {
        DateTime hoje = DateTime.Today;
        int idade = hoje.Year - this.DataNascimento.Year;

        if (this.DataNascimento.Date > hoje.AddYears(-idade)) idade--;

        return idade;
    }
}

