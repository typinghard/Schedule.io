# Schedule.io 📅

[![Build status](https://ci.appveyor.com/api/projects/status/55gx7f88centbbaw/branch/master?svg=true)](https://ci.appveyor.com/project/typinghard/schedule-io/branch/master)

Schedule.io é um nuget feito para auxiliar e encurtar o tempo de desenvolvimento de aplicações que possuem qualquer necessidade ligada a uma agenda entregando uma estrutura de fácil uso e moderna a medida que é atualizada.
 
O objetivo desse projeto é fazer com que os desenvolvedores não precisem perder tempo ou esforço desenvolvendo funções e regras que qualquer agenda teria que ter, como controle de choque de horários, criação de agendas, eventos e usuários, envio de notificações (em breve), entre outras.
 
Schedule.io tem suporte a .**Net Core 2.0+** e aos banco de dados **MongoDb**, **RavenDb** e **SqlServer**.

# Instalação
```c#
PM > Install-Package Schedule.io -Version 1.0.0
``` 
> O Schedule.io já vem com a compatibilidade de utiliação do SqlServer, para utilizar outros bancos, é só instalar os respectivos pacotes.

## Banco de Dados
### MongoDB
```c#
PM > Install-Package Schedule.io.MongoDB -Version 1.0.0
``` 
### RavenDB
```c#
PM > Install-Package Schedule.io.RavenDB -Version 1.0.0
``` 

Para instruções de instalação e exemplos, acesse nossa [Wiki](https://github.com/typinghard/Schedule.io/wiki/Home).

# Autores

* [Elvis Souza](https://www.linkedin.com/in/elvissouza/)
* [Diego Galante](https://www.linkedin.com/in/diego-galante/)
