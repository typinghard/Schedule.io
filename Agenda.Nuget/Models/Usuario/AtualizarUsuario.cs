using System;
using System.Collections.Generic;
using System.Text;

namespace ScheduleIo.Nuget.Models
{
    public class AtualizarUsuario
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public AtualizarUsuario(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
