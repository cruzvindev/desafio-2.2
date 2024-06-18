using Desafio_Consultorio.models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.repositories;

public class ConsultaRepository
{

    private readonly List<Consulta> consultas = new List<Consulta>();

    public void SalvarConsulta(Consulta consulta)
    {
        consultas.Add(consulta);
    }

    public ImmutableList<Consulta> TodasConsultas()
    {
        return consultas.ToImmutableList();
    }

    public ImmutableList<Consulta> ObterConsultasPassadasPorCPF(string cpf)
    {
        DateTime hoje = DateTime.Now.Date;

        var agendamentosAnteriores = consultas
            .Where(c => c.CPFPaciente == cpf && c.DataConsulta < hoje) 
            .ToImmutableList();
         
        return agendamentosAnteriores;
    }

    public ImmutableList<Consulta> ObterConsultasFuturasPorCPF(string cpf)
    {
        DateTime hoje = DateTime.Now.Date;

        var agendamentosAnteriores = consultas
            .Where(c => c.CPFPaciente == cpf && c.DataConsulta > hoje)
            .ToImmutableList();

        return agendamentosAnteriores;
    }

    public void ExcluirConsultas(string pacienteCPF)
    {
        consultas.RemoveAll(consulta => consulta.CPFPaciente == pacienteCPF);
    }

    public void ExcluirConsultasFuturas(string pacienteCPF)
    {
        DateTime hoje = DateTime.Now.Date;
        consultas.RemoveAll(consulta => consulta.CPFPaciente == pacienteCPF && consulta.DataConsulta > hoje);
    }

    public ImmutableList<Consulta> ObterConsultasFuturasDePaciente(Consulta consultaBuscada)
    {

        DateTime hoje = DateTime.Now.Date;

        var agendamentosFuturos = consultas
            .Where(consultaAtual => consultaAtual.CPFPaciente == consultaBuscada.CPFPaciente && 
            consultaAtual.DataConsulta == consultaBuscada.DataConsulta &&
            consultaAtual.HoraInicial == consultaBuscada.HoraInicial && consultaAtual.DataConsulta > hoje)
            .ToImmutableList();

        return agendamentosFuturos;
    }

}