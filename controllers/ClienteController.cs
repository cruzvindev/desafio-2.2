using Desafio_Consultorio.models;
using Desafio_Consultorio.services;
using Desafio_Consultorio.views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.controllers;
public class ClienteController
{
    private readonly ClienteService _clienteService;

    public ClienteController( ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    public void CadastrarCliente(string CPF, string nome, string dataNacimento)
    {
        Cliente cliente = new Cliente();
        
        while (!_clienteService.validaCpf(CPF))
        {
            Console.WriteLine("Erro: CPF inválido, verifique o dado fornecido e tente novamente !");
            CPF = Console.ReadLine();
        }

        while (_clienteService.verificaExistenciaCpf(CPF)) {
            Console.WriteLine("Erro: CPF já  cadastrado\n");
            CPF = Console.ReadLine();
        }
        cliente.CPF = CPF;

        while (!_clienteService.validaNome(nome))
        {
            Console.WriteLine("Erro: Nome inválido, verifique o dado fornecido e tente novamente !");
            nome = Console.ReadLine();  
        }
        cliente.Nome = nome;

        while (!_clienteService.validaData(dataNacimento))
        {
            dataNacimento = Console.ReadLine();
        }
        cliente.DataNascimento = _clienteService.ConverteData(dataNacimento);

        _clienteService.SalvarCliente(cliente);
        Console.WriteLine("Paciente cadastrado com sucesso !");
    }

}

