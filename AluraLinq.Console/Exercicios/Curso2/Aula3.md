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

```
var numeros = new List<int>();
numeros.Add (1);

var query 
	= numeros.Select (n => n * 10);

numeros.Add (2);

foreach(var numero in query)
	Console.WriteLine(numero);
```

Escolha a alternativa que representa o resultado desse trecho de código:

a) 
```
10
```

> A consulta é enumerada e avaliada na instrução `foreach`, portanto a linha `numeros.Add (2);`
acrescentou um novo elemento à lista `numeros`, que não consta no resultado dessa alternativa.

b) 
```
20
```

> Falta nesse resultado o elemento 10, resultado de `n => n * 10`, para o elemento n = 1;
 
c) 
```
1
```

> A cláusula `select` retorna a multiplicação dos elementos da lista `numeros` por 10,
> logo a alternativa está incorreta.

d) 
```
10
20
```

> CORRETO: a consulta `query` referencia a variável `numeros`. A definição de consulta `query` só é avaliada quando a consulta é enumerada pela instrução `foreach` na linha `foreach (var numero in query)`, então nesse momento a lista `numeros` já contém 2 elementos.

e) 
```
1
2
```

> A cláusula `select` retorna a multiplicação dos elementos da lista `numeros` por 10,
> logo a alternativa está incorreta.

### 2) Modificando variáveis de uma consulta em tempo de execução ### 

Considere o código abaixo:

```
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

```
1
2
A consulta trouxe 2 elementos.
```

b)

```
10
20
A consulta trouxe 2 elementos.
```

c)
```
10
20
20
40
A consulta trouxe 4 elementos.
```

d) 
```
20
40
A consulta trouxe 2 elementos.
```

e)
```
A consulta trouxe 0 elementos.
```

### 3) Remoção de elementos após a definição de uma consulta ### 

Veja o trecho a seguir:

```
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

b)
```
1
```

c)
```
10
20
```

d)
```
[nenhum resultado]
```

e)
```
[Runtime error: cannot modify "numeros" collection while it is being used by a LINQ query]
```

### 4) Execução adiada ### 

Observe o código abaixo:


var palavras = new List<string>() { "ALURA", "CURSOS" };

var maiusculas = palavras
  .Select (p => p.ToLower());
  
palavras.Clear();

foreach(var palavra in maiusculas)
	Console.WriteLine(palavra);

Console.WriteLine("A consulta trouxe {0} elementos.", maiusculas.Count());

Quando esse trecho é executado, o console contém o seguinte texto:

A consulta trouxe 0 elementos.

Por que a consulta não trouxe elementos, já que o comando palavras.Clear() foi chamado após a declaração dessa consulta? Explique.

### 5) Forçando execução imediata ### 

Observe o código abaixo:

var palavras = new List<string>() { "alura", "cursos" };

var maiusculas = palavras
  .Select (p => p.ToUpper()) 
  .ToList();
  
palavras.Clear();

foreach(var palavra in maiusculas)
	Console.WriteLine(palavra);

Após a execução, você obtém o seguinte resultado:

ALURA
CURSOS

Por que o laço foreach trouxe 2 palavras, em vez de uma lista vazia, já que o código palavras.Clear() deveria ter limpado a lista? O que aconteceu?