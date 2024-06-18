using Desafio_Consultorio.controllers;
using Desafio_Consultorio.models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.views;
public class PacienteView
{

    private readonly PacienteController _pacienteController;
    private readonly ConsultaController _consultaController;
    private readonly ConsultaView _consultaView;
    private readonly string MSG_ERRO_DATA = "\nErro: Data inválida, verifique o dado fornecido e tente novamente !";
    private readonly string MSG_ERRO_IDADE = "\nErro: paciente deve ter pelo menos 13 anos.";

    public PacienteView(PacienteController clienteController, ConsultaView consultaView, ConsultaController consultaController)
    {
        this._pacienteController = clienteController;
        this._consultaView = consultaView;
        this._consultaController = consultaController;
    }

    public void MenuPrincipal()
    {
        Console.WriteLine();
        Console.WriteLine("Menu Principal");
        Console.WriteLine("1 - Cadastro de pacientes");
        Console.WriteLine("2 - Agenda");
        Console.WriteLine("3 - Fim");
        var valorDigitado = Console.ReadLine();
        int valorConvertido;

        while (!int.TryParse(valorDigitado, out valorConvertido))
        {
            Console.WriteLine("Não foi possível compreender o valor digitado, tente novamente: \n");
            MenuPrincipal();
        }

        switch (valorConvertido)
        {
            case 1:
                MenuCadastroPaciente();
                break;
            case 2:
                MenuAgenda();
                break;
        }
    }

    public void MenuCadastroPaciente()
    {
        Console.WriteLine("\nMenu do Cadastro de Pacientes");
        Console.WriteLine("1 - Cadastrar novo paciente");
        Console.WriteLine("2 - Excluir Paciente");
        Console.WriteLine("3 - Listar pacientes (ordenado por CPF)");
        Console.WriteLine("4 - Listar pacientes (ordenado por nome)");
        Console.WriteLine("5 - Voltar p/ menu principal");

        var valorDigitado = Console.ReadLine();
        int valorConvertido;

        while (!int.TryParse(valorDigitado, out valorConvertido))
        {
            Console.WriteLine("Não foi possível compreender o valor digitado, tente novamente: \n");
            MenuPrincipal();
        }

        switch (valorConvertido)
        {
            case 1:
                CadastrarPaciente();
                break;
            case 2:
                ExcluirPaciente();
                break;
            case 3:
                ListarPacientes(1);
                break;
            case 4:
                ListarPacientes(2);
                break;
            case 5:
                MenuPrincipal();
                break;
        }
    }

    public void CadastrarPaciente()
    {
        Console.WriteLine();
        Console.Write("CPF: ");
        var cpf = Console.ReadLine();
        Console.Write("Nome: ");
        var nome = Console.ReadLine();
        Console.Write("Data de nascimento: ");
        var dataNascimento = Console.ReadLine();

        Paciente cliente = new Paciente();

        while (!_pacienteController.ValidaCPFPaciente(cpf))
        {
            Console.Write("Insira um CPF válido: ");
            cpf = Console.ReadLine();
        }

        while (_pacienteController.ValidaExistenciaPaciente(cpf))
        {
            Console.WriteLine("\nErro: CPF já  cadastrado\n");
            Console.Write("Insira um CPF válido: ");
            cpf = Console.ReadLine();
        }
        cliente.CPF = cpf;

        while (!_pacienteController.ValidaNomePaciente(nome))
        {
            Console.WriteLine("\nErro: Nome inválido, verifique o dado fornecido e tente novamente !");
            Console.Write("Insira um nome válido: ");
            nome = Console.ReadLine();
        }
        cliente.Nome = nome;

        bool condicaoData = false;

        while (!condicaoData)
        {

            if (_pacienteController.ValidaDataNascimentoPaciente(dataNascimento) == 0)
            {
                Console.WriteLine(MSG_ERRO_DATA);
                condicaoData = false;
            }

            if (_pacienteController.ValidaDataNascimentoPaciente(dataNascimento) == 1)
            {
                Console.WriteLine(MSG_ERRO_IDADE);
                condicaoData = false;
            }

            if (_pacienteController.ValidaDataNascimentoPaciente(dataNascimento) == 3)
            {
                condicaoData = true;
                break;
            }

            Console.Write("Por favor insira uma data de nascimento válida: ");
            dataNascimento = Console.ReadLine();
        }

        cliente.DataNascimento = this._pacienteController.ConverteDataNascimento(dataNascimento);

        this._pacienteController.CadastrarPaciente(cliente);
        Console.WriteLine("Paciente cadastrado com sucesso !");


        MenuCadastroPaciente();
    }

    public void ListarPacientes(int opcao)
    {
        var pacientes = _pacienteController.ListarPacientes(opcao);
        if (pacientes == null)
        {
            Console.WriteLine("Erro: Não há pacientes cadastrados");
            Console.WriteLine();
            MenuCadastroPaciente();
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-20} {1,-40} {2,-20} {3,-5}", "CPF", "Nome", "Dt.Nasc.", "Idade");
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            foreach (var item in pacientes)
            {
                string dataFormatada = item.DataNascimento.ToString("dd/MM/yyyy");
                var idade = item.GetIdade();
                Console.WriteLine("{0,-20} {1,-40} {2,-20} {3,-5}", item.CPF, item.Nome, dataFormatada, idade);

                var consultasFuturas = _consultaController.BuscaTodosAgendamentosPaciente(item.CPF);
                if (consultasFuturas.Any())
                {
                    string padding = new string(' ', 20);  
                    foreach (var consulta in consultasFuturas)
                    {
                        string horarioInicial = consulta.HoraInicial.ToString(@"hh\:mm");
                        string horarioFinal = consulta.HoraFinal.ToString(@"hh\:mm");
                        Console.WriteLine($"{padding}Agendado para: {consulta.DataConsulta:dd/MM/yyyy}");
                        Console.WriteLine($"{padding}{horarioInicial} às {horarioFinal}");
                    }
                }
            }

            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            MenuCadastroPaciente();
        }
    }


    public void MenuAgenda()
    {
        Console.WriteLine();
        Console.WriteLine("Agenda");
        Console.WriteLine("1 - Agendar Consulta");
        Console.WriteLine("2 - Cancelar Agendamento");
        Console.WriteLine("3 - Listar Agenda");
        Console.WriteLine("4 - Voltar p/ menu principal");

        var valorDigitado = Console.ReadLine();
        int valorConvertido;

        while (!int.TryParse(valorDigitado, out valorConvertido))
        {
            Console.WriteLine("Não foi possível compreender o valor digitado, tente novamente: \n");
            MenuPrincipal();
        }

        switch (valorConvertido)
        {
            case 1:
                this._consultaView.AgendarConsultaView();
                MenuAgenda();
                break;
            case 2:
                this._consultaView.CancelarAgendamentoView();
                break;
            case 3:
                this._consultaView.ListarAgenda();
                break;
            case 4:
                MenuPrincipal();
                break;
        }
    }

    public void ExcluirPaciente()
    {
        Console.WriteLine();
        Console.Write("CPF: ");
        var pacienteCPF = Console.ReadLine();

        int respostaValidacao = _pacienteController.BuscaEValidaCPF(pacienteCPF);
        bool condicao = true;
       
        while (condicao)
        {
            if(respostaValidacao == 0)
            {
                Console.WriteLine("Erro: Paciente não cadastrado");
                Console.Write("CPF: ");
                pacienteCPF = Console.ReadLine();
                respostaValidacao = _pacienteController.BuscaEValidaCPF(pacienteCPF);
            }
      
            if (respostaValidacao == 1)
            {
                Console.WriteLine("Erro: CPF inválido, verifique o dado fornecido e tente novamente !");
                Console.Write("CPF: ");
                pacienteCPF = Console.ReadLine();
                respostaValidacao = _pacienteController.BuscaEValidaCPF(pacienteCPF);
            }

            if (respostaValidacao == 2)
            {
                condicao = false;
            }
        }

        while (!this._pacienteController.ExcluirPaciente(pacienteCPF))
        {
            Console.WriteLine("Erro: paciente está agendado");
        }

        Console.WriteLine("Paciente exluído com sucesso!");
        MenuCadastroPaciente(); 
    }
}
