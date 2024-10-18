# Microservices Solution Overview

Este repositório contém uma coleção de soluções .NET para gerenciamento de motocicletas, locações, entregadores e notificações. A estrutura é baseada em microservices, com um gateway expositor (BFFService) que centraliza as rotas dos serviços.

## Para Executar a aplicação
**Utilizando Docker você navegará até a raiz do projeto no seu terminal e executara e seguintes comandos**

**Passo 1**
```sh
   docker network create mtt-network
```
**Passo 2**

```sh
   docker-compose build --no-cache 
```
**Passo 3**

```sh
   docker-compose up -d  
```
## Estrutura das Soluções

### RentalMotorcycle

- **RentalMotorcycle**: Projeto principal da aplicação de locação de motocicletas.
- **RentalMotorcycle.Application**: Contém os handlers e comandos para as operações de locação.
- **RentalMotorcycle.Domain**: Contém os modelos de domínio.
- **RentalMotorcycle.Infrastructure**: Contém a implementação dos repositórios e o contexto do banco de dados.

### MotorcycleService

- **MotorcycleService**: Projeto principal da aplicação de gerenciamento de motocicletas.
- **MotorcycleService.Application**: Contém os handlers e comandos para as operações de motocicletas.
- **MotorcycleService.Domain**: Contém os modelos de domínio.
- **MotorcycleService.Infrastructure**: Contém a implementação dos repositórios e o contexto do banco de dados.

### DeliveryPilots

- **DeliveryPilots**: Projeto principal da aplicação de gerenciamento de entregadores.
- **DeliveryPilots.Application**: Contém os handlers e comandos para as operações de entregadores.
- **DeliveryPilots.Domain**: Contém os modelos de domínio.
- **DeliveryPilots.Infrastructure**: Contém a implementação dos repositórios e o contexto do banco de dados.

### NotificationService

- **NotificationService**: Projeto principal da aplicação de notificações.
- **NotificationService.Service**: Contém os serviços de notificação.
- **NotificationService.Domain**: Contém os modelos de domínio.
- **NotificationService.Infrastructure**: Contém a implementação dos repositórios e o contexto do banco de dados.

### BFFService

O BFFService atua como um gateway expositor para os microservices acima, centralizando as rotas dos serviços.

## Rotas dos Controllers

### MotosController

- **POST /motos**: Adiciona uma nova moto.
- **GET /motos**: Obtém todas as motos.
- **PUT /motos/{id}/placa**: Atualiza a placa de uma moto.
- **GET /motos/{id}**: Obtém uma moto pelo ID.
- **DELETE /motos/{id}**: Deleta uma moto pelo ID.

### LocaçãoController

- **POST /locacao**: Adiciona uma nova locação.
- **GET /locacao/{id}**: Obtém uma locação pelo ID.
- **PUT /locacao/{id}/devolucao**: Atualiza a data de devolução de uma locação.

### EntegadoresController

- **POST /entregadores**: Cadastra um novo entregador.
- **POST /entregadores/{id}/cnh**: Adiciona a imagem da CNH de um entregador.

## Pré-requisitos

## Rotas das notificações

### MotosController

- **GET notification/admin**: Obtém notificações motos cadastradas e do ano numa lista conjunta.
- **GET notification/{id}/user**: Obtém todas as motos.


- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [PostgreSQL](https://www.postgresql.org/download/)
- [RabbitMQ](https://www.rabbitmq.com/download.html)

## Configuração

### Banco de Dados

Certifique-se de que o PostgreSQL está instalado e configurado corretamente. Atualize a string de conexão no arquivo `appsettings.json` conforme necessário.

### Docker

Utilize Docker para facilitar a configuração e execução dos serviços.

### RabbitMQ

Configure o RabbitMQ para gerenciar as filas de mensagens entre os serviços.

## Executando a Aplicação

1. Clone o repositório.
2. Configure os arquivos `appsettings.json` conforme necessário.
3. Execute os comandos `dotnet build` e `dotnet run` para iniciar os serviços.

## Testes

Cada solução contém testes unitários para garantir a qualidade do código. Execute os testes com o comando `dotnet test`.
