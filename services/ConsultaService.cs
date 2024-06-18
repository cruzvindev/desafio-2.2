using Desafio_Consultorio.models;
using Desafio_Consultorio.repositories;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Desafio_Consultorio.services;

public class ConsultaService
{
    private readonly ConsultaRepository _consultaRepository;

    public ConsultaService(ConsultaRepository repository)
    {
        this._consultaRepository = repository;
    }

    public int AgendarConsulta(Consulta consulta)
    {
        var todasConsultas = this._consultaRepository.TodasConsultas();

        if (!VerificaHorarios(consulta, todasConsultas))
        {
            return 0;
        }

        else if (!HorarioValido(consulta.HoraInicial, consulta.HoraFinal))
        {
            return 1;
        }
        if(!ConsultorioAberto(consulta.HoraInicial, consulta.HoraFinal))
        {
            return 2;
        }

        if (!ValidaData(consulta.DataConsulta))
        {
            return 3;
        }

        if (!PodeAgendarConsulta(consulta))
        {
            return 4;
        }


        this._consultaRepository.SalvarConsulta(consulta);
        return 6;
    }


    public bool VerificaHorarios(Consulta agendamento, ImmutableList<Consulta> consultas)
    {
        foreach (var consulta in consultas)
        {
            if (consulta.DataConsulta == agendamento.DataConsulta && ((agendamento.HoraInicial < consulta.HoraFinal
                && agendamento.HoraFinal > consulta.HoraInicial)
                || (agendamento.HoraInicial == consulta.HoraInicial && agendamento.HoraFinal == consulta.HoraFinal)))
            {
                return false; //Detectou um conflito de horários
            }
        }

        if (agendamento.HoraFinal > agendamento.HoraInicial && agendamento.DataConsulta > DateTime.Now ||
            DateTime.Now == agendamento.DataConsulta && agendamento.HoraInicial > DateTime.Now.TimeOfDay)
        {
            return true;
        }

        return true;
    }


    public bool HorarioValido(TimeSpan horarioInicial, TimeSpan horarioFinal)
    {
        return horarioInicial.Minutes % 15 == 0 && horarioFinal.Minutes % 15 == 0;
    }

    public bool ConsultorioAberto(TimeSpan horarioInicial, TimeSpan horarioFinal)
    {
        TimeSpan horarioAbertura = new TimeSpan(8, 0, 0);
        TimeSpan horarioFechamento = new TimeSpan(19, 0, 0);

        // Verifica se ambos os horários estão dentro do intervalo de funcionamento
        if (horarioInicial >= horarioAbertura && horarioInicial <= horarioFechamento &&
            horarioFinal >= horarioAbertura && horarioFinal <= horarioFechamento)
        {
            return true;
        }

        return false;
    }
     public bool PodeAgendarConsulta(Consulta pacienteConsulta)
      {
            var consultasPaciente = _consultaRepository.ObterConsultasFuturasPorCPF(pacienteConsulta.CPFPaciente);

            // Verifica se já existe um agendamento futuro
            if (consultasPaciente.Count >= 1)
            {
                return false;
            }

            return true;
       }

      public DateTime ConverteData(string data)
      {
            DateTime dataConvertida;
            DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataConvertida);
            return dataConvertida;
       }

      public bool ValidaData(DateTime dataConsulta)
       {
            if (dataConsulta < DateTime.Now)
            {
                return false;
            }

            return true;
       }

      public bool CancelarConsulta(Consulta consulta)
      {
        var consultasBuscadas = this._consultaRepository.ObterConsultasFuturasDePaciente(consulta);
        if (consultasBuscadas.Count() > 0)
        {
            this._consultaRepository.ExcluirConsultasFuturas(consulta.CPFPaciente);
            return true;
        }

        return false;
       }

    public ImmutableList<Consulta> ConsultasFuturasPaciente(string CPF)
    {
        return this._consultaRepository.ObterConsultasFuturasPorCPF(CPF);
    }

    public ImmutableList<Consulta> TodasConsultas()
    {
        return this._consultaRepository.TodasConsultas();
    }

    public bool ValidaDatas(string dataInicial, string dataFinal)
    {
        DateTime dataInicialConvertida;
        DateTime dataFinalConvertida;

        if (!DateTime.TryParseExact(dataInicial, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataInicialConvertida) ||
            !DateTime.TryParseExact(dataFinal, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataFinalConvertida))
        {
            return false;
        }

        if (dataFinalConvertida < dataInicialConvertida)
        {
            return false;
        }

        return true;
    }

    public ImmutableList<Consulta> BuscaAgendamentosPorData(DateTime dataInicial, DateTime dataFinal)
    {
        var consultas = this._consultaRepository.TodasConsultas();

        return consultas.Where(consulta => consulta.DataConsulta >= dataInicial && consulta.DataConsulta <= dataFinal).ToImmutableList();
    }
}


