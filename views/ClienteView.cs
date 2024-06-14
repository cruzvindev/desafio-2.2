using Desafio_Consultorio.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.views;
public class ClienteView
{

    private readonly ClienteController _clienteController;

    public ClienteView(ClienteController clienteController)
    {
        _clienteController = clienteController;
    }

    public void MenuPrincipal()
    {
        Console.WriteLine("Menu Principal");
        Console.WriteLine("1 - Cadastro de pacientes");
        Console.WriteLine("2 - Agenda");
        Console.WriteLine("3 - Fim");
        var valorDigitado = Console.ReadLine();
        int valorConvertido;
        
        while(!int.TryParse(valorDigitado, out valorConvertido))
        {
            Console.WriteLine("Não foi possível compreender o valor digitado, tente novamente: \n");
            MenuPrincipal();
        }

        switch (valorConvertido)
        {
            case 1:
                MenuCadastroPaciente();
                break;
        
        }
    }

    public void MenuCadastroPaciente()
    {
        Console.WriteLine("\nMenu do Cadastro de Pacientes");
        Console.WriteLine("1 - Cadastrar novo paciente");
        Console.WriteLine("2 - Listar pacientes");
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

        }
    }

    public void CadastrarPaciente()
    {
        Console.WriteLine("CPF: ");
        var cpf = Console.ReadLine();
        Console.WriteLine("Nome: ");
        var nome = Console.ReadLine();
        Console.WriteLine("Data de nascimento: ");
        var dataNascimento = Console.ReadLine();
        this._clienteController.CadastrarCliente(cpf, nome, dataNascimento); 
    }
}
