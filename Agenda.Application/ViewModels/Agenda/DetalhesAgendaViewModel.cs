﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
    public class DetalhesAgendaViewModel
    {
        public string Id { get; set; }
        [DisplayName("Nome")]
        public string Titulo { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Público")]
        public bool Publico { get; set; }
    }
}
