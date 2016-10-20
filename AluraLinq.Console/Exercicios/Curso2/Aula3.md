# Aula 3 #

*Entenda execução adiada e execução imediata nas consultas LINQ*
* Entendendo o momento de execução de uma consulta
* Como as consutas LINQ são afetadas pelas mudanças de variáveis que ela utiliza
* Forçando uma execução imediata
* Usando execução imediata para reaproveitar o cache de dados

---

---

---

### 1) Adicionando elementos após a definição de uma consulta ### 

Considere o trecho de código abaixo:

```C#
var numeros = new List<int>();
numeros.Add (1);

var query 
	= numeros.Select (n => n * 10);

numeros.Add (2);

foreach(var numero in query)
	Console.WriteLine(numero);
```

Escolha a alternativa que apresenta o resultado desse trecho de código:

a) 
```C#
10
```

> A consulta é enumerada e avaliada na instrução `foreach`, portanto a linha `numeros.Add (2);`
acrescentou um novo elemento à lista `numeros`, que não consta no resultado dessa alternativa.

b) 
```C#
20
```

> Falta nesse resultado o elemento 10, resultado de `n => n * 10`, para o elemento n = 1;
 
c) 
```C#
1
```

> A cláusula `select` retorna a multiplicação dos elementos da lista `numeros` por 10,
> logo a alternativa está incorreta.

d) 
```C#
10
20
```

> CORRETO: a consulta `query` referencia a variável `numeros`. A definição de consulta `query` só é avaliada quando a consulta é enumerada pela instrução `foreach` na linha `foreach (var numero in query)`, então nesse momento a lista `numeros` já contém 2 elementos.

e) 
```C#
1
2
```

> A cláusula `select` retorna a multiplicação dos elementos da lista `numeros` por 10,
> logo a alternativa está incorreta.

### 2) Modificando variáveis de uma consulta em tempo de execução ### 

Considere o código abaixo:

```C#
int[] numeros = { 1, 2 };

int fator = 10;
IEnumerable<int> query = numeros.Select (n => n * fator);

fator = 20;

foreach(var numero in query)
	Console.WriteLine(numero);
	
Console.WriteLine("A consulta trouxe {0} elementos.", query.Count());
```

Qual o resultado aparece no console após a execução do trecho acima?

a)

```C#
1
2
A consulta trouxe 2 elementos.
```
> Essa opção traz os elementos da fonte de dados `numeros`, mas não leva em consideração o fator de multiplicação.

b)

```C#
10
20
A consulta trouxe 2 elementos.
```

> A consulta é avaliada e executada na linha `foreach(var numero in query)`, e nesse momento a variável `fator` tem valor igual a 20, logo
> o resultado deveria ser: 

```
20
40
```


c)
```
10
20
20
40
A consulta trouxe 4 elementos.
```

> A origem de dados tem 2 elementos, logo o resultado deveria trazer somente 2 elementos.

d) 
```
20
40
A consulta trouxe 2 elementos.
```

> CORRETO: os dois elementos da origem de dados (1, 2) devem ser multiplicados pelo fator 20.

e)
```
A consulta trouxe 0 elementos.
```

> A origem de dados não foi modificada, e a consulta não possui filtro, portanto o resultado deveria
> trazer 2 elementos.

### 3) Remoção de elementos após a definição de uma consulta ### 

Veja o trecho a seguir:

```C#
var numeros = new List<int>() { 1, 2 };

var query = numeros.Select (n => n * 10);

numeros.Clear();

foreach(var numero in query)
	Console.WriteLine(numero);

```

Qual alternativa mostra corretamente o resultado do laço foreach?

a)
 
```
1
2
```

> somente na linha `foreach(var numero in query)` a consulta `query` é avaliada, e nesse ponto todos os elementos da origem de dados `numeros` já tinham sido removidos
> pela linha `numeros.Clear();`, portanto nenhum resultado deveria ser mostrado.
 
b)
       
```
1
```

> somente na linha `foreach(var numero in query)` a consulta `query` é avaliada, e nesse ponto todos os elementos da origem de dados `numeros` já tinham sido removidos
> pela linha `numeros.Clear();`, portanto nenhum resultado deveria ser mostrado.

c)

```C#
10
20
```

> somente na linha `foreach(var numero in query)` a consulta `query` é avaliada, e nesse ponto todos os elementos da origem de dados `numeros` já tinham sido removidos
> pela linha `numeros.Clear();`, portanto nenhum resultado deveria ser mostrado.

d)
```C#
[nenhum resultado]
```
> CORRETO: somente na linha `foreach(var numero in query)` a consulta `query` é avaliada, e nesse ponto todos os elementos da origem de dados `numeros` já tinham sido removidos
> pela linha `numeros.Clear();`

e)
```C#
[Runtime error: cannot modify "numeros" collection while it is being used by a LINQ query]
```

> O trecho de código roda corretamente. Nenhuma mensagem de erro deveria ser mostrada.

### 4) Execução adiada ### 

Observe o código abaixo:

```C#
var palavras = new List<string>() { "ALURA", "CURSOS" };

var maiusculas = palavras
  .Select (p => p.ToLower());
  
palavras.Clear();

foreach(var palavra in maiusculas)
	Console.WriteLine(palavra);

Console.WriteLine("A consulta trouxe {0} elementos.", maiusculas.Count());
```

Quando esse trecho é executado, o console contém o seguinte texto:

`A consulta trouxe 0 elementos.`

Por que a consulta não trouxe elementos, já que o comando palavras.Clear() foi chamado após a declaração dessa consulta? Explique.

> Porque a consulta `maiusculas` havia sido declarada, mas ainda não havia sido executada. Ela só é
> executada na linha  `foreach(var palavra in maiusculas)`, e nesse momento a origem de dados `palavras`
> já tinha sido esvaziada pela linha `palavras.Clear();`.

### 5) Forçando execução imediata ### 

Observe o código abaixo:

```C#
var palavras = new List<string>() { "alura", "cursos" };

var maiusculas = palavras
  .Select (p => p.ToUpper()) 
  .ToList();
  
palavras.Clear();

foreach(var palavra in maiusculas)
	Console.WriteLine(palavra);
```

Após a execução, você obtém o seguinte resultado:

```
ALURA
CURSOS
```

Por que o laço foreach trouxe 2 palavras, em vez de uma lista vazia, já que o código 
    `palavras.Clear()` deveria ter limpado a lista? O que aconteceu?

> Porque a instrução `Console.WriteLine()` dentro do laço `foreach` está imprimindo os elementos
> da lista `maiusculas`, que é uma **lista em memória** (e não uma **variável de consulta**) que 
> foi instanciada no momento em que a lista `palavras` ainda continha os dados iniciais. Depois que a lista `maiusculas` é criada, ela não
> é mais afetada pelo esvaziamento da lista `palavras`. 