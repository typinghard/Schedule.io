﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Agenda.Application.ViewModels
{
    public class DetalhesUsuarioViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("E-mail")]
        public string UsuarioEmail { get; set; }
    }
}
