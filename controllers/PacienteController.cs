using Desafio_Consultorio.models;
using Desafio_Consultorio.repositories;
using Desafio_Consultorio.services;
using Desafio_Consultorio.views;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.controllers;
public class PacienteController
{
    private readonly PacienteService _pacienteService;
    private readonly PacienteView _pacienteView;

    public PacienteController( PacienteService clienteService)
    {
        _pacienteService = clienteService;
    }

    public void CadastrarPaciente(Paciente paciente)
    {
        this._pacienteService.SalvarCliente(paciente);
    }

    public bool ValidaCPFPaciente(string  CPF)
    {
        return this._pacienteService.validaCpf(CPF);
    }

    public bool ValidaExistenciaPaciente(string CPF)
    {
        return this._pacienteService.verificaExistenciaCpf(CPF);
    }

    public bool ValidaNomePaciente(string nome)
    {
        return this._pacienteService.validaNome(nome);
    }

    public int ValidaDataNascimentoPaciente(string dataNascimento)
    {
        return this._pacienteService.validaData(dataNascimento);
    }

    public DateTime ConverteDataNascimento(string dataNascimento)
    {
        return this._pacienteService.ConverteData(dataNascimento);
    }

    public ISet<Paciente> ListarPacientes(int opcaoListagem)
    {
        return this. _pacienteService.BuscaPacientes(opcaoListagem);
    }

    public int BuscaEValidaCPF(string CPF)
    {
        if (!this._pacienteService.verificaExistenciaCpf(CPF))
        {
            return 0;
        }

        if (!this._pacienteService.validaCpf(CPF))
        {
            return 1;
        }

        return 2;
    }

    public bool ExcluirPaciente(string CPF)
    {
        return this._pacienteService.ExcluiPaciente(CPF);
    }

    public Paciente BuscaPaciente(string CPF)
    {
        return this._pacienteService.BuscaPaciente(CPF);
    }
}

