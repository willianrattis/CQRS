

# CQRS
## Command and Queries Responsability Segregation

1. [O que é?](#o-que-é) 

2. [Qual problema ele Resolve?](#qual-problema-ele-resolve)

3. [ CQRS funciona sem utilizar o **MediatR**?](#cqrs-funciona-sem-utilizar-o-mediatr)

4. [Utilizando o  **MediatR**](#utilizando-o-mediatr)
<br>

### O que é?
Segregação de Responsabilidade Por Leitura e Escrita.
ou
Separação de Responsabilidades em Consultas e Comandos.

	Segregar => Separar, de forma a separar a responsabilidade.

CQRS é um padrão arquitetural onde o **foco principal** é separar os meios de leitura e escrita de dados.

***

<br>

### Qual problema ele Resolve?

Pensando desta forma, quando sabemos o que é **CQRS**, parece até óbvio, afinal nossas aplicações quase nunca são balanceadas, ou tem muita **leitura**, ou tem muita **escrita**.

	E-Comerce é um bom cenário para a utilização de CQRS

Particularmente, vejo que na maioria dos casos temos mais leitura do que escrita, como um exemplo de um **_E-Comerce_**, onde navegamos por horas (Leitura apenas) e só fechamos o pedido uma vez (Escrita).

>  #### Primeiro Problema a ser combatido 
>
> _Alguém já viu uma aplicação que **busca todos os dados no  banco**, depois faz um `new` e cria um objeto novo para filtrar estes dados em memória e depois devolve para o cliente?_
>
> - Vai utilizar mais infraestrutura 
> - Vai utilizar mais processamento e memória 



### Solução Proposta

_Criar **Queries** acertivas para leitura de um dado ajuda a você não ter que filtrar os dados na memória e não ter um custo a mais indo no banco buscar dados de que você não precisa devolver para o cliente._

-   Diminuí o custo da aplicação de fazer a busca
-   Diminui o custo de processamento
-   Diminui o custo de infraestrutura

Conclusão com este tipo de arquitetura tendemos a utilizar melhor os recursos. 

***
<br>

### CQRS funciona sem utilizar o MediatR?

A resposta para essa pergunta é SIM. O **MediatR** é apenas um padrão para abstração de comunicação. 

### Então como ficaria na prática? 

#### Estrutura do Projeto

```shell
  .
  ├── Ecommerce
  │   ├── Controllers
  │   │	└── ClienteController.cs
  │   ├── Domain
  │   │		├──  Commands
  │   │		│	├──  Requests
  │   │		│	│	└──  CriarClienteRequest.cs
  │   │		│	└──  Responses
  │   │		│		└──  CriarClienteResponse.cs
  │   │		└──  Handlers
  │   │				└──  CriarClienteHandler.cs
  │   │				└──  ICriarClienteHandler.cs
  │   └── Properties
  │   	└──  launchSettings.json
  ├── Program.cs
  ├── Ecommerce.csproj
  ├── Startup.cs
  ├── appsettings.json
  └── README.md
```

#### Sem utilizarmos o **MediatR**

Primeiro nós precisaríamos resgistrar o nosso serviço no contêiner de injeção de dependências do asp.net core indicando que `CriarClienteHandler` resolve a interface `ICriarClienteHandler`
```csharp
public  void  ConfigureServices(IServiceCollection  services)
{
	services.AddControllers();
	services.AddTransient<ICriarClienteHandler, CriarClienteHandler>();
}
```

Criaremos o nosso request, digamos que nosso comando enviará **nome** e **e-mail**. 

```csharp
using  System;

namespace  ECommerce.Domain.Commands.Requests
{
	public  class  CriarClienteRequest
	{
		public  string  Nome { get; set; }
		public  string  Email { get; set; }
	}
}
```

Criaremos o nosso response, digamos que após nosso comando ser processado nós queremos que a resposta contenha um **Id** e uma **data de cadastro**.

```csharp
using System;

namespace ECommerce.Domain.Commands.Responses
{
	public class CriarClienteResponse
	{
		public Guid Id { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public DateTime Date { get; set; }
	}
}
```

Criaremos o nosso **handler** que irá capturar a nossa **request**. E aqui está uma parte importante, vale ressaltar que seria muito mais cômodo do que validar nossas **entidade** seja validar os nossos **comandos**. 

```csharp
using  System;
using  ECommerce.Domain.Commands.Responses;
using  ECommerce.Domain.Commands.Requests;
using  MediatR;
using  System.Threading.Tasks;
using  System.Threading;
  
namespace  ECommerce.Domain.Handlers
{
	public  class  CriarClienteHandler : ICriarClienteHandler
	{
		public  CriarClienteResponse  Handle(CriarClienteRequest  request)
		{
			// Verifica se o Cliente já esta cadastrado
			// Valida os dados
			// Insere o cliente
			// Enviar E-mail de boas vindas

			return  new  CriarClienteResponse
			{
				Id = Guid.NewGuid(),
				Nome =  "Willian",
				Email =  "willian.rattis@email.com",
				Date = DateTime.Now
			};
		}
	}
}
```

Criaremos a nossa Controller.

Sem utilizar o **MediatR** nós teríamos que ligar o **handler** diretamente ao **comando**, ou seja, `[FromServices]ICreateCustomerHandler handler`, `[FromBody]CreateCustomerRequest  command` a grande aderência ao **MediarR** vem do fato de podermos abstrair o **Command** do **Handler**. 


>_Estaremos utilizando o `[FromServices]` para fazer a injeção de dependência, mas nada nos impediria de útilizar o método padrão via `Construtor` ._

```csharp
using  Microsoft.AspNetCore.Mvc;
using  ECommerce.Domain.Commands.Requests;
using  ECommerce.Domain.Handlers;

namespace  ECommerce.Controllers
{
	[ApiController]
	[Route("clientes")]
	public  class  ClienteController : ControllerBase
	{
	
		[HttpPost]
		[Route("cadastrar")]
		public  IActionResult  Create(
			[FromServices]ICriarClienteHandler handler,
			[FromBody]CriarClienteRequest command)
		{
			var  response  = handler.Handle(command);
				
			return  Ok(response);
		}
	}
}
```

*** 

<br>

#### Utilizando o **MediatR**

Primeiramente vamos adicionar os pacotes abaixo.

 _Destacando que o pacote `DependencyInjection` é dependente do `MediatR`, logo instalando o segundo ele acaba instalando o primeiro._

> dotnet add package **MediatR** --version 10.0.1
dotnet add package **MediatR.Extensions.Microsoft.DependencyInjection** --version 10.0.1

<br>

Segundo passo é que utilizando o **MediatR** nós não temos mais a necessidade de resolver aquela Interface `ICriarClienteHandler`, utilizando apenas o `services.AddMediatR()`.

```csharp
public  void  ConfigureServices(IServiceCollection  services)
{
	services.AddControllers();
	services.AddMediatR(Assembly.GetExecutingAssembly());
}
```

Vamos implementar a interface `IRequest<T>` do **MediatR** no nosso request.
 
```csharp
using  System;

namespace  ECommerce.Domain.Commands.Requests
{
	public  class  CriarClienteRequest : IRequest<CriarClienteResponse>
	{
		public  string  Nome { get; set; }
		public  string  Email { get; set; }
	}
}
```

Nosso response fica igual. 

```csharp
using System;

namespace ECommerce.Domain.Commands.Responses
{
	public class CriarClienteResponse
	{
		public Guid Id { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public DateTime Date { get; set; }
	}
}
```

No nosso **Handler** deixaremos de utilizar nossa interface `ICriarClienteHandler` para utilizar `IRequestHandler<T>` do **MediatR**, nós iremos implementar o método `Task<T> Handle(T request, CancellationToken cancellationToken)` da interface do **MediatR**.


> *Nota*: Agora nosso método **Handle** é uma `Task` e tem mais um parâmetro  `CancellationToken`


```csharp
using  System;
using  ECommerce.Domain.Commands.Responses;
using  ECommerce.Domain.Commands.Requests;
using  MediatR;
using  System.Threading.Tasks;
using  System.Threading;
  
namespace  ECommerce.Domain.Handlers
{
	public  class  CriarClienteHandler : IRequestHandler<CriarClienteRequest>
	{
		public Task<CriarClienteResponse> Handle(CriarClienteRequest request,
			CancellationToken cancellationToken)
		{
			// Verifica se o Cliente já esta cadastrado
			// Valida os dados
			// Insere o cliente
			// Enviar E-mail de boas vindas

			var resultado = new  CriarClienteResponse
			{
				Id = Guid.NewGuid(),
				Nome =  "Willian",
				Email =  "willian.rattis@email.com",
				Date = DateTime.Now
			};
			
			return Task.FromResult(resultado)
		}
	}
}
```

Na nossa Controller com **MediatR** veja que agora nós podemos utilizar o método `Send` do **MediatR** passando nosso **comando** e que não temos mais atrelado uma **interface** a um **Comando**, agora para cada Comando que tivermos nós pederemos utilizar a interface `IMediator` o que nos beneficia no reaproveitamento de código. 

>_Estaremos utilizando o `[FromServices]` para fazer a injeção de dependência, mas nada nos impediria de útilizar o método padrão via `Construtor` ._

```csharp
using  Microsoft.AspNetCore.Mvc;
using  ECommerce.Domain.Commands.Requests;
using  ECommerce.Domain.Handlers;

namespace  ECommerce.Controllers
{
	[ApiController]
	[Route("v1/clientes")]
	public  class  ClienteController : ControllerBase
	{

		[HttpPost]
		[Route("cadastrar")]
		public  Task<CriarClienteResponse> Create(
			[FromServices]IMediator mediator,
			[FromBody]CriarClienteRequest command)
		{
			var  response  = mediator.Send(command);
				
			return  Ok(response);
		}
	}
}
```
