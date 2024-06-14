using Desafio_Consultorio.controllers;
using Desafio_Consultorio.repositories;
using Desafio_Consultorio.services;
using Desafio_Consultorio.views;

ClienteRepository repository = new ClienteRepository();
ClienteService service = new ClienteService(repository);
ClienteController controller = new ClienteController(service);
ClienteView view = new ClienteView(controller);

view.MenuPrincipal();
