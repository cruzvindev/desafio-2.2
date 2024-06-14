using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.models;

public class Cliente : IComparable<Cliente>
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }

    public Cliente(string Nome, string CPF, DateTime DataNascimento)
    {
        this.Nome = Nome;
        this.CPF = CPF;
        this.DataNascimento = DataNascimento;
    }

    public Cliente() { }

    public int CompareTo(Cliente other)
    {
        if (other == null) return 1;
        return string.Compare(this.CPF, other.CPF, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is not Cliente) return false;

        var other = (Cliente)obj;
        return CPF.Equals(other.CPF);
    }

    public override int GetHashCode()
    {
        return CPF.GetHashCode();
    }
}

