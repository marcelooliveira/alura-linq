# Aula 5 #

*Integre resultados de stored procedures em consultas LINQ*
* Problema do mundo real: consultas LINQ to Entities num cenário com stored procedures
* Usando stored procedures SQL Server como uma fonte de dados qualquer

**1. Configurando uma aplicação para utilizar stored procedures em consultas**
 
Você é membro de uma equipe de software que utiliza SQL Server, Entity Framework e consultas 
LINQ. Uma colega da sua equipe acaba de desenvolver uma stored procedure do SQL Server chamada 
`ps_Itens_Por_Cliente`, que recebe o id de um cliente e produz uma listagem detalhada de vendas por 
aquele cliente. Os dados dessa stored procedure deverão ser utilizados em diversas consultas 
LINQ. A stored procedure possui o seguinte script de criação:

```
CREATE PROCEDURE[dbo].[ps_Itens_Por_Cliente] @clienteId int = 0
AS
BEGIN

    SELECT
    i.FaixaId,
    i.ItemNotaFiscalId,
    i.NotaFiscalId,
    i.PrecoUnitario,
    i.Quantidade,
    i.PrecoUnitario* i.Quantidade As Total,
    n.DataNotaFiscal,
    f.Nome
    FROM ItemNotaFiscal i
    JOIN NotaFiscal n ON i.NotaFiscalId = n.NotaFiscalId
    JOIN Faixa f ON f.FaixaId = i.FaixaId
    WHERE n.ClienteId = @clienteId
END
```

Quais são os passos necessários para que as consultas LINQ possam utilizar essa stored procedure 
como origem de dados?

**a**

1. Compilar o script da stored procede no banco de dados SQL Server
2. Consumir a stored procedures numa consulta LINQ, passando como argumento o id do cliente,
e acessando-a através do `contexto` do Entity Framework, como se faz com qualquer entidade
do EF, assim como no seguinte exemplo:

```
int clienteId = 17;

using (var contexto = new AluraTunesEntities())
{
    var query = from p in contexto.ps_Vendas_Por_Cliente(clienteId)
                where <filtro_da_consulta>
                select p;
```

> Para os passos da alternativa funcionarem, deve-se atualiza o modelo do Entity Framwork a
> partir do banco de dados após a execução do script da stored procedure.

**b**

1. Criar uma nova consulta LINQ com exatamente a mesma lógica da stored procedure.
2. Utilizar essa nova consulta LINQ quando necessário

> O enunciado exige que a stored procedure seja utilizada tal como ela foi criada, portanto 
> não é adequado traduzi-la para uma consulta LINQ. em vez disso, devemos consumi-la diretamente
> dentro das consultas LINQ. 

**c**

1. Criar uma string contendo o script da consulta da stored procedure:

```
string script = @"SELECT
    i.FaixaId,
    i.ItemNotaFiscalId,
    i.NotaFiscalId,
    i.PrecoUnitario,
    i.Quantidade,
    i.PrecoUnitario* i.Quantidade As Total,
    n.DataNotaFiscal,
    f.Nome
    FROM ItemNotaFiscal i
    JOIN NotaFiscal n ON i.NotaFiscalId = n.NotaFiscalId
    JOIN Faixa f ON f.FaixaId = i.FaixaId
    WHERE n.ClienteId = @clienteId";
```

2. Consumir a stored procedure usando o script acima através do método `Execute()` do contexto:

```
int clienteId = 17;

using (var contexto = new AluraTunesEntities())
{
    var query = from p in contexto.Execute(script)
                where <filtro_da_consulta>
                select p;
```

> Não existe um método `Execute` no objeto do contexto do Entity Framework, que possa ser usado
> numa consulta LINQ.

**d**

1. Compilar o script da stored procede no banco de dados SQL Server
2. Atualizar o modelo do entity framework a partir do banco de dados
3. Consumir a stored procedures numa consulta LINQ, passando como argumento o id do cliente,
e acessando-a através do `contexto` do Entity Framework, como se faz com qualquer entidade
do EF, assim como no seguinte exemplo:

```
int clienteId = 17;

using (var contexto = new AluraTunesEntities())
{
    var query = from p in contexto.ps_Vendas_Por_Cliente(clienteId)
                where <filtro_da_consulta>
                select p;
```

> CORRETO. Após compilar a procedure e atualizar o modelo do Entity Framework a partir do 
> banco de dados, você pode consumir a stored procedure a partir do contexto do Entity Framework.

**e**

1. Compilar o script da stored procede no banco de dados SQL Server
2. Atualizar o modelo do entity framework a partir do banco de dados
3. Criar um método `ps_Vendas_Por_Cliente`, com o mesmo nome da stored procedure,
para ser usado como proxy da stored procedure:

public DBSet<Vendas> ps_Vendas_Por_Cliente(int clienteId)
{
}

4. Consumir a stored procedures numa consulta LINQ, passando como argumento o id do cliente,
e acessando-a através do novo método `ps_Vendas_Por_Cliente()` criado para servir como proxy da
stored procedure:

```
int clienteId = 17;

using (var contexto = new AluraTunesEntities())
{
    var query = from p in ps_Vendas_Por_Cliente(clienteId)
                where <filtro_da_consulta>
                select p;
```

> Não é necessário criar um método de proxy para se consumir uma stored procedure numa consulta
> LINQ através do Linq to Entities.



