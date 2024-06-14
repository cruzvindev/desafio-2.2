using Desafio_Consultorio.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Consultorio.repositories;
public class ClienteRepository
 {
    private readonly SortedSet<Cliente> _clientes = new SortedSet<Cliente>();

    public bool VerificaCpf(string CPF)
    {
        return _clientes.Any(cliente => cliente.CPF == CPF);
    }

    public void Salvar(Cliente cliente)
    {
        this._clientes.Add(cliente);
    }


 }

