using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleIo.Nuget.Interfaces;
using ScheduleIo.Nuget.Models;

namespace Agenda.UI.Web.Controllers
{
    public class AgendaController : Controller
    {
        IAgendaService _agendaService;
        IUsuarioService _usuarioService;
        ILocalService _localService;
        IEventoService _eventoService;

        public AgendaController(IAgendaService agendaService,
                                IUsuarioService usuarioService,
                                ILocalService localService,
                                IEventoService eventoService)
        {
            _agendaService = agendaService;
            _usuarioService = usuarioService;
            _localService = localService;
            _eventoService = eventoService;
        }


        // GET: Agenda
        public IActionResult Index()
        {
            var Id = Guid.Empty;
            try
            {
                //var agenda = new NovaAgenda($"Agenda - {DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second}", "Descrição simples", true);
                var agenda = new ScheduleIo.Nuget.Models.Agenda()
                {
                    Titulo = "Agenda da Limpeza",
                    Descricao = "",
                    Publico = false
                };
                var agendaId = _agendaService.Gravar(agenda);


                var usuarioId = _usuarioService.Gravar(new Usuario()
                {
                    Email = "email@mail.com"
                });


                var evento = new Evento()
                {
                    AgendaId = agendaId,
                    IdentificadorExterno = string.Empty,
                    Titulo = "Limpar a casa",
                    Descricao = string.Empty

                };

                var eventoId = _eventoService.Gravar(new Evento()
                {
                    AgendaId = agendaId,
                    IdentificadorExterno = string.Empty,
                    Titulo = "Limpar a casa",
                    Descricao = string.Empty,
                    Convites = new List<Convite>()
                    {
                        new Convite()
                        {
                            UsuarioId = usuarioId,
                            Permissoes = new PermissoesConvite()
                            {
                                ConvidaUsuario=true,
                                ModificaEvento=true,
                                VeListaDeConvidados= true,
                            }
                        }
                    },
                    Local = null,
                    DataInicio = DateTime.Now,
                    DataFinal = DateTime.MinValue,
                    DataLimiteConfirmacao = DateTime.Now,
                    QuantidadeMinimaDeUsuarios = 2,
                    OcupaUsuario = false,
                    Publico = false,
                    Frequencia = Domain.Enums.EnumFrequencia.Nao_Repete,
                    Tipo = new TipoEvento()
                    {
                        Nome = "Limpeza",
                        Descricao = string.Empty
                    }
                });



                //List<ScheduleIo.Nuget.Models.NovaAgenda> listaAgendas = new List<ScheduleIo.Nuget.Models.NovaAgenda>();
                //listaAgendas.Add(new ScheduleIo.Nuget.Models.NovaAgenda($"Agenda - {DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second}", "Descrição simples", true));
                //listaAgendas.Add(new ScheduleIo.Nuget.Models.NovaAgenda($"Agenda - {(DateTime.Now.Hour + 1) + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second}", "Descrição simples", true));

                //foreach (var item in listaAgendas)
                //    _agendaService.Criar(item);



                //Id = Guid.Parse("eb658ecc-c887-4a06-a4f6-257d128ffc65");
                //Id = Guid.Parse("a83812a5-567a-42ca-b914-f398df67914b");
                Id = agendaId;

                //var teste = _agendaService.Obter(Id);
                //var par = teste.Id;
                //var par1 = teste.CriadoAs;

                //var atualizar = new AtualizarAgenda(teste.Id, teste.Titulo, teste.Descricao + ". Alterando para privado!", !teste.Publico);
                //_agendaService.Editar(atualizar);

                ////teste = _agendaService.Obter(agendaId);

                //_agendaService.Excluir(Id);


                return Content($"Agenda Criada, atualizada e excluida com sucesso! Id da agenda: {Id}");
                //teste = _agendaService.Obter(Id);
            }
            catch (Agenda.Domain.Core.DomainObjects.ScheduleIoException ex)
            {
                var retorno = string.Join(", ", ex.ScheduleIoMessages.Select(x => x).ToList());
                Console.WriteLine(retorno);

                return Content(retorno);
            }
        }

        // GET: Agenda/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Agenda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agenda/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Agenda/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Agenda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Agenda/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Agenda/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}