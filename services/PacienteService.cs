using Desafio_Consultorio.models;
using Desafio_Consultorio.repositories;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.services;
public class PacienteService
{

    private readonly PacienteRepository _pacienteRepository;
    private readonly ConsultaRepository _consultaRepository;

    public PacienteService(PacienteRepository pacienteRepository, ConsultaRepository consultaRepository)
    {
        _pacienteRepository = pacienteRepository;
        _consultaRepository = consultaRepository;
    }

    public ISet<Paciente> BuscaPacientes(int opcaoListagem)
    {
        var clientes = this._pacienteRepository.BuscaPacientes();

        if (opcaoListagem == 1)
        {
            return clientes;
        }

        if (opcaoListagem == 2)
        {
            return clientes.OrderBy(paciente => paciente.Nome).ToHashSet();
        }

        if (clientes.Count() == 0)
        {
            return null;
        }
        return clientes;
    }

    public Paciente BuscaPaciente(string CPF)
    {
        return this._pacienteRepository.BuscaPaciente(CPF);
    }

    public void SalvarCliente(Paciente paciente)
    {
        _pacienteRepository.Salvar(paciente);
    }

    public bool verificaExistenciaCpf(string cpf)
    {
        if (_pacienteRepository.VerificaCpf(cpf))
        {
            return true;
        }
        return false;
    }

    public bool validaCpf(string pacienteCpf)
    {

        if (pacienteCpf.Length == 11)
        {
            bool cond2 = !verificaTodosDigitos(pacienteCpf);
            bool cond3 = validaDigitosCpf(pacienteCpf);

            if (cond2 && cond3)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else { return false; }
    }

    public bool verificaTodosDigitos(string pacienteCpf)
    {
        // Verifica se todos os dígitos são iguais ao primeiro
        bool todosIguais = true;
        char primeiroDigito = pacienteCpf[0];
        for (int i = 1; i < pacienteCpf.Length; i++)
        {
            if (pacienteCpf[i] != primeiroDigito)
            {
                todosIguais = false;
                break;
            }
        }
        return todosIguais;
    }

    public bool validaDigitosCpf(string pacienteCpf)
    {
        int[] digitos = new int[10];
        int aux = 10;
        double primeiroDigitoSoma = 0;
        double segundoDigitoSoma = 0;
        double J = 0;
        double K = 0;

        for (int i = 0; i < 9; i++)
        {
            digitos[i] = (pacienteCpf[i] - '0') * aux;
            aux--;
        }

        for (int i = 0; i < digitos.Length; i++)
        {
            primeiroDigitoSoma += digitos[i];
        }

        // Validação do primeiro dígito verificador J
        if ((primeiroDigitoSoma % 11) == 0 || (primeiroDigitoSoma % 11) == 1)
        {
            J = 0;
        }
        else if ((primeiroDigitoSoma % 11) >= 2 && (primeiroDigitoSoma % 11) <= 10)
        {
            J = 11 - (primeiroDigitoSoma % 11);
        }

        // Validação do segundo dígito verificador K
        aux = 11;
        for (int i = 0; i < 10; i++)
        {
            digitos[i] = (pacienteCpf[i] - '0') * aux;
            aux--;
        }

        for (int i = 0; i < digitos.Length; i++)
        {
            segundoDigitoSoma += digitos[i];
        }

        if ((segundoDigitoSoma % 11) == 0 || (segundoDigitoSoma % 11) == 1)
        {
            K = 0;
        }
        else if ((segundoDigitoSoma % 11) >= 2 && (segundoDigitoSoma % 11) <= 10)
        {
            K = 11 - (segundoDigitoSoma % 11);
        }

        if (((pacienteCpf[pacienteCpf.Length - 1] - '0') == K && (pacienteCpf[pacienteCpf.Length - 2] - '0') == J))
        {
            return true;
        }
        else { return false; }
    }

    public int validaData(string dataNascimento)
    {
        DateTime dataNasc;
        if (!DateTime.TryParseExact(dataNascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNasc))
        {
            return 0;
        }

        DateTime hoje = DateTime.Today;
        int idade = hoje.Year - dataNasc.Year;

        // Se a data de nascimento ainda não chegou este ano, diminui a idade em 1
        if (dataNasc.Date > hoje.AddYears(-idade))
        {
            idade--;
        }

        if (idade < 13)
        {
            return 1;
        }

        return 3;
    }

    public bool validaNome(string pacienteNome)
    {
        if (pacienteNome.Length > 5)
        {
            return true;
        }
        else { return false; }
    }

    public DateTime ConverteData(string dataQualquer)
    {
        DateTime data;
        DateTime.TryParseExact(dataQualquer, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data);
        return data;
    }

    public bool ExcluiPaciente(string pacienteCPF)
    {
        var consultasPassadas = this._consultaRepository.ObterConsultasPassadasPorCPF(pacienteCPF);
        var consultasFuturas = this._consultaRepository.ObterConsultasFuturasPorCPF(pacienteCPF);

        if (consultasFuturas.Count == 0)
        {
            if (consultasPassadas.Count >= 1)
            {
                this._consultaRepository.ExcluirConsultas(pacienteCPF);
            }
            this._pacienteRepository.ExcluirPaciente(pacienteCPF);
            return true;
        }

        return false;
    }

}


