# Aula 6 #

*Estude, analise e brinque com consultas através do LinqPad*
* Problema do mundo real: a dificuldade de depurar, analisar e testar consultas LINQ em projetos grandes
* Rodando consultas LINQ to Objects no LinqPad
* Usando método Dump() para exibir facilmente o resultado de uma consulta
* Obtendo a sintaxe de método equivalente à sintaxe de consulta (com método AsQueryable())
* Rodando consultas LINQ to XML no LinqPad
* Definindo um arquivo XML como fonte de dados no LinqPad
* Rodando consultas LINQ to Entities no LinqPad
* Visualizando consulta LINQ e a consulta SQL equivalente


**1) Qual a vantagem de se usar a ferramenta LinqPad para consutas LINQ? (assinale todas as
alternativas corretas)**

**a**

Permite fazer consultas Linq to Objects

> CORRETO: LinqPad possui suporte a Linq to Objects. Basta colocar uma expressão LINQ na janela **Query**
> e executar o código (tecla F5). 

**b**

Permite fazer consultas Linq to XML

> CORRETO: LinqPad permite que o código referencie um arquivo XML para ser usado em consultas Linq to XML.

**c**

Permite fazer consultas Linq to Entities

> CORRETO: LinqPad permite que uma consulta Linq to Entities seja criada. Para isso, é necessário configurar
> uma conexão com um assembly (.EXE ou .dlll) contendo a configuração para o Entity Framework.

**d**

Permite testar qualquer código .Net (C#, VB.NET ou F#)

> CORRETO: LinqPad consegue interpretar código de qualquer linguagem .Net (C#, VB.NET ou F#)

**e**

Permite exibir o resultado formatado

> CORRETO: LinqPad permite exibir uma visualização do conteúdo de consultas LINQ, em forma
> de tabela ou grid.

**2) Exibindo resultados de consultas LINQ**

Imagine que você coloque na janela **Query** do LinqPad o seguinte código C#:

```
var numeros = new int[] {1,2,3,4,5,6,7,8,9,10};

var impares = from n in numeros
			  where n % 2 == 1
			  select n;
```				

Qual a maneira mais fácil de se obter o resultado da consulta `impares` no painel **Resultados**
do LinqPad? 

a

Através de um laço `foreach` contendo uma instrução `Console.WriteLine()` para exibir o conteúdo
do resultado da consulta no console.

> Embora essa abordagem funcione, é mais fácil obter o mesmo resultado através do método 
> de extensão `Dump()`.

b

Adicionando ao final do código a linha: `Console.WriteLine(impares.ToString());`

> O método `ToString` não irá imprimir os dados dos elementos da consulta `impares`, mas sim
> o nome do tipo da consulta `impares`: `System.Linq.Enumerable+WhereArrayIterator 1[System.Int32]`

c

Acrescentando ao final do código a linha: `impares.ToList();`

> O método `ToList()` executa a consulta `impares` e converte seu resultado em uma lista, porém 
> não lança nenhuma informação no painel de resultados do LinqPad.

d

Adicionando ao final do código a linha: `impares.Dump();`.

> CORRETO: O método `Dump();` é um método de extensão poderoso do LinqPad, que permite visualizar
> o conteúdo de objetos, como o resultado de consultas LINQ.

e

Adicionando ao final do código a linha: `numeros.Dump();`.

> Com essa alternativa, serias exibidos os elementos do array `numeros`, mas não os elementos
> da consulta `impares`, como pede o enunciado.  

**3) Obtendo uma sintaxe de método a partir de uma sintaxe de consulta**

Considere o seguinte trecho de código C#, que utiliza sintaxe de consulta numa consulta Linq
to Objects:

```
var numeros = new int[] {1,2,3,4,5,6,7,8,9,10};

var impares = from n in numeros
			  where n % 2 == 1
			  select n;

```

Você deseja obter a versão de sintaxe de método para essa consulta LINQ que utiliza sintaxe
de consulta. Você clica no botão lambda (λ) para ver resultado, mas nada aparece. O que é 
necessário para que o LinqPad exiba na janela lambda (λ) a versão de sintaxe de método?

> É necessário modificar a consulta `impares` para que o array `numeros`
> seja transformado com o método AsQueryable(). Isso permite que o LinqPad exiba o resultado
> da consulta `impares` na janela de resultados.

**4) Configuração de consulta Linq to Entities no LinqPad**

Suponha que você já tenha uma aplicação rodando Entity Framework. O que é necessário 
configurar no LinqPad para rodar uma consulta Linq to Entities aproveitando a configuração
da sua aplicação existente?

a 



b

c

d

Adicionar uma conexão com **Add Connection**, e em Choose data context, escolher
Entity Framework (DbContext V4/V5/V6). Em seguida, em **Path to Custom Assembly**, selecionar
o arquivo **.exe** ou **.dll** onde está localizado o modelo do Entity Framework (arquivo edmx).
E no final, em **Path to application config file** deve ser informado o caminho do arquivo
.config da aplicação.


* Rodando consultas LINQ to Objects no LinqPad

* Usando método Dump() para exibir facilmente o resultado de uma consulta
* Obtendo a sintaxe de método equivalente à sintaxe de consulta (com método AsQueryable())

* Rodando consultas LINQ to XML no LinqPad
* Definindo um arquivo XML como fonte de dados no LinqPad

* Rodando consultas LINQ to Entities no LinqPad


* Visualizando consulta LINQ e a consulta SQL equivalente



