using Desafio_Consultorio.models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.repositories;
public class PacienteRepository
 {
    private readonly SortedSet<Paciente> _pacientes = new SortedSet<Paciente>();

    public bool VerificaCpf(string CPF)
    {
        return _pacientes.Any(cliente => cliente.CPF == CPF);
    }

    public void Salvar(Paciente paciente)
    {
        this._pacientes.Add(paciente);
    }

    //Utilizando a biblioteca Immutable da própria Microsoft
    public ImmutableSortedSet<Paciente> BuscaPacientes() 
    {
        return this._pacientes.ToImmutableSortedSet();
    }

    public void ExcluirPaciente(string pacienteCPF)
    {
        _pacientes.RemoveWhere(paciente => paciente.CPF == pacienteCPF);
    }

    public Paciente BuscaPaciente(string CPF)
    {
        return this._pacientes.FirstOrDefault(paciente => paciente.CPF == CPF);
    }
 }

