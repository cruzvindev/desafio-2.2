using Desafio_Consultorio.models;
using Desafio_Consultorio.services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.controllers;

public class ConsultaController
{

    private readonly ConsultaService _consultaService;

    public ConsultaController(ConsultaService consultaService)
    {
        this._consultaService = consultaService;
    }

    public int AgendarPaciente(string CPF, string dataConsulta, string horaInicial, string horaFinal)
    {
        var dataConvertida = this._consultaService.ConverteData(dataConsulta);
        try
        {
            var horaInicialConvertida = TimeSpan.Parse(horaInicial);
            var horaFinalConvertida = TimeSpan.Parse(horaFinal);
            Consulta consulta = new Consulta(CPF, dataConvertida, horaInicialConvertida, horaFinalConvertida);

            return this._consultaService.AgendarConsulta(consulta);
        } catch(Exception ex)
        {
            return 4;
        }
    }

    public bool CancelarAgendamentoPaciente(string CPF, string dataConsulta, string horaInicial)
    {
        var dataConvertida = this._consultaService.ConverteData(dataConsulta);
        var horaInicialConvertida = TimeSpan.Parse(horaInicial);

        Consulta consulta = new Consulta(CPF, dataConvertida, horaInicialConvertida);

        return this._consultaService.CancelarConsulta(consulta);
    }

    public ImmutableList<Consulta> BuscaTodosAgendamentosPaciente(string CPF)
    {
        return this._consultaService.ConsultasFuturasPaciente(CPF);
    }

    public ImmutableList<Consulta> BuscaTodosAgendamentos() => this._consultaService.TodasConsultas();

    public bool ValidaData(string dataInicial, string dataFinal) => this._consultaService.ValidaDatas(dataInicial, dataFinal);

    public ImmutableList<Consulta> BuscaTodosAgendamentosPorPeriodo(DateTime dataInicial, DateTime dataFinal) => this._consultaService.BuscaAgendamentosPorData(dataInicial, dataFinal);
   
    public DateTime ConverteData(string data) => this._consultaService.ConverteData(data);
}
