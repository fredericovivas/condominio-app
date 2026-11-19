# CondoManage

Aplicação desktop para gestão de um condomínio residencial, desenvolvida em C# (Windows Forms, .NET Framework 4.8) com base de dados MySQL.

O CondoManage permite ao administrador gerir apartamentos, condóminos, pagamentos, eventos, intervenções técnicas e notificações num único sítio. Reuniões, manutenções e prazos de pagamento são todos guardados como *eventos*, o que liga o controlo financeiro ao planeamento no tempo.

> Projeto académico desenvolvido no âmbito de um TeSP em Information Systems and Programming Technologies. Foi pensado para um único utilizador (o administrador): não tem login nem suporte multiutilizador.

## Funcionalidades

| Módulo | O que faz |
|---|---|
| **Apartamentos** | Regista apartamentos por bloco, piso e número. A combinação bloco + número só pode existir uma vez. |
| **Condóminos** | Regista condóminos (nome, email, telefone) e associa cada um a um apartamento. Vários condóminos podem partilhar o mesmo apartamento. |
| **Eventos** | Cria reuniões, manutenções e prazos de pagamento com data, hora e descrição. |
| **Pagamentos** | Regista um pagamento de um condómino associado a um evento de prazo de pagamento, com valor e estado (*Paid* / *In debt*). Inclui *Mark as Paid* e *View Debtors*; os pagamentos em dívida aparecem a vermelho. |
| **Intervenções** | Regista intervenções técnicas associadas a um evento, com descrição, data e custo opcional. |
| **Notificações** | Guarda o histórico de notificações administrativas por condómino. As mensagens ficam apenas registadas: não são enviadas por email nem SMS. |

Inclui ainda:

- **Dashboard** no ecrã principal: totais de apartamentos, condóminos e eventos, número de pagamentos em dívida com o valor total em falta, e lista dos próximos eventos. Atualiza sempre que se fecha uma janela de um módulo.
- **Pesquisa em tempo real** em todos os módulos: a grelha filtra enquanto se escreve.
- **Validação de dados** com mensagens específicas e, quando um registo não pode ser apagado por ter dados associados, uma mensagem clara em vez do erro técnico da base de dados.

Apartamentos, Condóminos e Eventos permitem criar, editar e apagar. Os Pagamentos permitem criar, marcar como pago e apagar. As Intervenções e as Notificações permitem criar e apagar.

## Arquitetura

```
CondominioApp/
├── Forms/          Ecrãs Windows Forms, um por módulo
├── Models/         Classes das entidades e enums
├── Repositories/   Acesso a dados, um repositório por entidade
├── Helpers/        DatabaseConnection (lê a connection string do App.config)
├── FormMain.cs     Menu principal e dashboard
└── Program.cs      Ponto de entrada
```

- Os **Forms** tratam apenas da apresentação e da interação com o utilizador; todo o SQL está nos **Repositories**.
- As queries são parametrizadas (`MySqlCommand` com parâmetros) para prevenir SQL injection.
- As credenciais da base de dados ficam no `App.config`, que está excluído do controlo de versões.
- Os nomes no código estão em português (por exemplo, `Condomino`); a interface está em inglês.

## Base de dados

Seis tabelas no esquema `condominio_db`:

| Tabela | Descrição | Referencia |
|---|---|---|
| `apartamento` | Apartamentos | — |
| `condomino` | Condóminos | `apartamento` |
| `evento` | Eventos (tipo: `reuniao`, `manutencao`, `prazo_pagamento`) | — |
| `pagamento` | Pagamentos (estado: `pago`, `em_divida`) | `condomino`, `evento` |
| `intervencao` | Intervenções | `evento` |
| `notificacao` | Notificações | `condomino` |

Todas as chaves estrangeiras usam `ON DELETE RESTRICT`, por isso não é possível apagar um registo de que outros dependem. A tabela `apartamento` tem uma restrição `UNIQUE (bloco, numero)`.

## Como executar

**Requisitos:** Windows, Visual Studio com a carga de trabalho *Desenvolvimento para desktop com .NET* (.NET Framework 4.8) e MySQL Server 8.0 com MySQL Workbench.

1. **Clona** o repositório.
2. **Cria a base de dados:** abre `database/schema.sql` no MySQL Workbench e executa-o.
3. **(Recomendado) Cria um utilizador MySQL dedicado:**
   ```sql
   CREATE USER 'condominio_user'@'localhost' IDENTIFIED BY 'escolhe-uma-password-forte';
   GRANT ALL PRIVILEGES ON condominio_db.* TO 'condominio_user'@'localhost';
   ```
4. **Configura a ligação:** copia `CondominioApp/App.example.config` para `CondominioApp/App.config` e substitui `SEU_UTILIZADOR` e `SUA_PASSWORD` pelo teu utilizador e password do MySQL.
5. **Abre** `CondominioApp.slnx` (ou `CondominioApp/CondominioApp.csproj`) no Visual Studio e carrega em **F5**. O NuGet restaura o pacote `MySql.Data` automaticamente.

## Tecnologias

- C# / .NET Framework 4.8
- Windows Forms
- MySQL 8.0 com MySQL Connector/NET (`MySql.Data`)
- Git e GitHub

## Possíveis evoluções

- Migração para uma base de dados SQLite embutida (sem servidor separado)
- Envio real de notificações por email e SMS
- Relatórios em PDF (devedores, histórico de pagamentos, intervenções)
- Login do administrador
- Gráficos no dashboard
- Versão web para acesso a partir de vários dispositivos

## Autor

Frederico Vivas

## Licença

Distribuído sob a [licença MIT](LICENSE).