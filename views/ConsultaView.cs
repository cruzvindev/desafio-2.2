using Desafio_Consultorio.controllers;
using Desafio_Consultorio.models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.views;

public class ConsultaView
{
    private readonly ConsultaController _consultaController;
    private readonly PacienteController _pacienteController;

    public ConsultaView(ConsultaController consultaController, PacienteController pacienteController)
    {
        _consultaController = consultaController;
        _pacienteController = pacienteController;
    }

    public void AgendarConsultaView()
    {
        Console.WriteLine();
        Console.Write("CPF: ");
        var pacienteCPF = Console.ReadLine();

        int respostaValidacao = _pacienteController.BuscaEValidaCPF(pacienteCPF);
        bool resultadoValidacao = false;

        if (respostaValidacao == 2)
        {
            resultadoValidacao = true;
        }

        while (!resultadoValidacao)
        {
            Console.WriteLine("Erro: Paciente não cadastrado");
            Console.Write("CPF: ");
            pacienteCPF = Console.ReadLine();
            respostaValidacao = _pacienteController.BuscaEValidaCPF(pacienteCPF);

            if (respostaValidacao == 0)
            {
                Console.WriteLine("Erro: paciente não cadastrado");
            }

            if (respostaValidacao == 1)
            {
                Console.WriteLine("Erro: CPF inválido, verifique o dado fornecido e tente novamente !");
            }

            if (respostaValidacao == 2)
            {
                resultadoValidacao = true;
            }
        }

        Console.Write("Data da consulta: ");
        var dataConsulta = Console.ReadLine();
        Console.Write("Hora inicial: ");
        var horaInicial = Console.ReadLine();
        Console.Write("Hora final: ");
        var horaFinal = Console.ReadLine();

        int resultadoAgendamento = this._consultaController.AgendarPaciente(pacienteCPF, dataConsulta, horaInicial, horaFinal);
        bool condicao = true;

        while (condicao)
        {
            if(resultadoAgendamento == 0)
            {
                Console.WriteLine("Erro: Já existe uma consulta agendada nessa data e horário");
                Console.Write("Hora inicial: ");
                horaInicial = Console.ReadLine();
                Console.Write("Hora final: ");
                horaFinal = Console.ReadLine();
                resultadoAgendamento = this._consultaController.AgendarPaciente(pacienteCPF, dataConsulta, horaInicial, horaFinal);
            }

            if(resultadoAgendamento == 1)
            {
                Console.WriteLine("Erro: Os horários iniciais e finais devem definidos de 15 em 15 minutos");
                Console.Write("Hora inicial: ");
                horaInicial = Console.ReadLine();
                Console.Write("Hora final: ");
                horaFinal = Console.ReadLine();
                resultadoAgendamento = this._consultaController.AgendarPaciente(pacienteCPF, dataConsulta, horaInicial, horaFinal);
            }

            if (resultadoAgendamento == 2)
            {
                Console.WriteLine("Erro: O consultório não está aberto nesse horário fornecido");
                Console.Write("Hora inicial: ");
                horaInicial = Console.ReadLine();
                Console.Write("Hora final: ");
                horaFinal = Console.ReadLine();
                resultadoAgendamento = this._consultaController.AgendarPaciente(pacienteCPF, dataConsulta, horaInicial, horaFinal);
            }

            if (resultadoAgendamento == 3)
            {
                Console.WriteLine("Erro: Essa data fornecida já passou");
                Console.Write("Data da consulta: ");
                dataConsulta = Console.ReadLine();
                resultadoAgendamento = this._consultaController.AgendarPaciente(pacienteCPF, dataConsulta, horaInicial, horaFinal);
            }

            if (resultadoAgendamento == 4)
            {
                Console.WriteLine("Erro: Este paciente já tem uma consulta agendada");
                Console.Write("Data da consulta: ");
                dataConsulta = Console.ReadLine();
                Console.Write("Hora inicial: ");
                horaInicial = Console.ReadLine();
                Console.Write("Hora final: ");
                horaFinal = Console.ReadLine();
                resultadoAgendamento = this._consultaController.AgendarPaciente(pacienteCPF, dataConsulta, horaInicial, horaFinal);
            }

            if( resultadoAgendamento == 5)
            {
                Console.WriteLine("Erro: Não foi possível compreender o horário fornecido, tente novamente.");
                Console.Write("Hora inicial: ");
                horaInicial = Console.ReadLine();
                resultadoAgendamento = this._consultaController.AgendarPaciente(pacienteCPF, dataConsulta, horaInicial, horaFinal);
            }

            if(resultadoAgendamento == 6)
            {
                condicao = false;
            }

        }

        Console.WriteLine("Consulta agendada com sucesso !");
    }

    public void CancelarAgendamentoView()
    {
        Console.WriteLine();
        Console.Write("CPF: ");

        var pacienteCPF = Console.ReadLine();

        int respostaValidacao = _pacienteController.BuscaEValidaCPF(pacienteCPF);
        bool resultadoValidacao = false;

        if (respostaValidacao == 2)
        {
            resultadoValidacao = true;
        }

        while (!resultadoValidacao)
        {
            Console.WriteLine("Erro: Paciente não cadastrado");
            Console.Write("CPF: ");
            pacienteCPF = Console.ReadLine();
            respostaValidacao = _pacienteController.BuscaEValidaCPF(pacienteCPF);

            if (respostaValidacao == 0)
            {
                Console.WriteLine("Erro: paciente não cadastrado");
            }

            if (respostaValidacao == 1)
            {
                Console.WriteLine("Erro: CPF inválido, verifique o dado fornecido e tente novamente !");
            }

            if (respostaValidacao == 2)
            {
                resultadoValidacao = true;
            }
        }

        Console.Write("Data da consulta: ");
        var dataConsulta = Console.ReadLine();
        Console.Write("Hora inicial: ");
        var horaInicial = Console.ReadLine();

        while(!this._consultaController.CancelarAgendamentoPaciente(pacienteCPF, dataConsulta, horaInicial))
        {
            Console.WriteLine("Erro: agendamento não encontrado");
            Console.WriteLine();
            Console.Write("Data da consulta: ");
            dataConsulta = Console.ReadLine();
            Console.Write("Hora inicial: ");
            horaInicial = Console.ReadLine();
        }
        Console.WriteLine();
        Console.WriteLine("Agendamento cancelado com sucesso !");
    }

    public void ListarAgenda()
    {
        Console.WriteLine();
        Console.Write("Apresentar a agenda T-Toda ou P-Período: ");
        var opcao = Console.ReadLine().ToLower();
        while (opcao != "t" && opcao != "p")
        {
            Console.Write("Apresentar a agenda T-Toda ou P-Período: ");
            opcao = Console.ReadLine().ToLower();
        }

        IList<Consulta> consultas = new List<Consulta>();

        if (opcao == "t")
        {
            consultas = this._consultaController.BuscaTodosAgendamentos();
        }

        if (opcao == "p")
        {
            Console.Write("Data inicial: ");
            var dataIncial = Console.ReadLine();
            Console.Write("Data final: ");
            var dataFinal = Console.ReadLine();

            while (!this._consultaController.ValidaData(dataIncial, dataFinal))
            {
                Console.WriteLine("Erro: as datas são ivnálidas. Tente novamente: ");
                Console.Write("Data inicial: ");
                dataIncial = Console.ReadLine();
                Console.Write("Data final: ");
                dataFinal = Console.ReadLine();
            }

            var dataInicialConvertida = this._consultaController.ConverteData(dataIncial);
            var dataFinalConvertida = this._consultaController.ConverteData(dataFinal);
            consultas = this._consultaController.BuscaTodosAgendamentosPorPeriodo(dataInicialConvertida, dataFinalConvertida);
        }


        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("{0,-20} {1,-15} {2,-15} {3,-15} {4,-30} {5,-20}", "Data", "H.Ini","H.Fim", "Tempo", "Nome", "Dt.Nasc.");
        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
        foreach (var item in consultas)
        {
           
            var pacientes = this._pacienteController.BuscaPaciente(item.CPFPaciente);
            var dataPaciente = pacientes.DataNascimento.ToString("dd/MM/yyyy");
            var dataConsulta = item.DataConsulta.ToString("dd/MM/yyyy");
            var diferenca = item.HoraFinal - item.HoraInicial;
            var tempoFormatado = string.Format("{0:D2}:{1:D2}", (int)diferenca.TotalHours, diferenca.Minutes);
            var horaInicio = item.HoraInicial.ToString(@"hh\:mm");
            var horaFinal = item.HoraFinal.ToString(@"hh\:mm");
            var nomePaciente = pacientes.Nome;

             Console.WriteLine("{0,-20} {1,-15} {2,-15} {3,-15} {4,-30} {5,-20}", dataConsulta, horaInicio, horaFinal, tempoFormatado, nomePaciente, dataPaciente);
            }
        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
    }

 
 }

