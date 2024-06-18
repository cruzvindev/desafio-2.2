using Desafio_Consultorio.controllers;
using Desafio_Consultorio.repositories;
using Desafio_Consultorio.services;
using Desafio_Consultorio.views;

PacienteRepository pacienteRepository = new PacienteRepository();
ConsultaRepository consultaRepository = new ConsultaRepository();
PacienteService pacienteService = new PacienteService(pacienteRepository, consultaRepository);
PacienteController pacienteController = new PacienteController(pacienteService);
ConsultaService consultaService = new ConsultaService(consultaRepository);
ConsultaController consultaController = new ConsultaController(consultaService);
ConsultaView consultaView = new ConsultaView(consultaController, pacienteController);
PacienteView pacienteView = new PacienteView(pacienteController, consultaView, consultaController);

pacienteView.MenuPrincipal();

