# Aula 1 #

*Aprenda a criar relatórios com paginação*
* Definindo tamanho de resultados com o método Take()
* Saltando elementos de uma sequência com método Skip()
* Acessando elementos de uma página de uma sequência de dados

---

### 1) Definindo tamanho de resultados de uma consulta ###

Observe o código da consulta abaixo:

```

using (var contexto = new AluraTunesEntities())
{
		
	var query = from nf in contexto.NotasFiscais
				orderby nf.NotaFiscalId
				select new
				{
					Numero = nf.NotaFiscalId,
					Data = nf.DataNotaFiscal,
					Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
					Total = nf.Total
				};

	foreach (var nf in query)
	{
		Console.WriteLine("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente, nf.Total);
	}
}
```

Ao executar esse programa, você recebe o seguinte resultado:

```
1  01/01/2009 00:00:00  Leonie Köhler  1,98
2  02/01/2009 00:00:00  Bjørn Hansen  3,96
3  03/01/2009 00:00:00  Daan Peeters  5,94
4  06/01/2009 00:00:00  Mark Philips  8,91
5  11/01/2009 00:00:00  John Gordon  13,86
6  19/01/2009 00:00:00  Fynn Zimmermann  0,99
7  01/02/2009 00:00:00  Niklas Schröder  1,98
8  01/02/2009 00:00:00  Dominique Lefebvre  1,98
9  02/02/2009 00:00:00  Wyatt Girard  3,96
10  03/02/2009 00:00:00  Hugh O'Reilly  5,94
11  06/02/2009 00:00:00  Emma Jones  8,91
12  11/02/2009 00:00:00  Leonie Köhler  13,86
13  19/02/2009 00:00:00  Frank Harris  0,99
14  04/03/2009 00:00:00  Jack Smith  1,98
15  04/03/2009 00:00:00  Tim Goyer  1,98
16  05/03/2009 00:00:00  Kathy Chase  3,96
17  06/03/2009 00:00:00  Victor Stevens  5,94
18  09/03/2009 00:00:00  Martha Silk  8,91
19  14/03/2009 00:00:00  Dominique Lefebvre  13,86
20  22/03/2009 00:00:00  Steve Murray  0,99
21  04/04/2009 00:00:00  Mark Taylor  1,98
22  04/04/2009 00:00:00  Luis Rojas  1,98
23  05/04/2009 00:00:00  Puja Srivastava  3,96
24  06/04/2009 00:00:00  Bjørn Hansen  5,94
25  09/04/2009 00:00:00  Eduardo Martins  8,91
26  14/04/2009 00:00:00  Tim Goyer  13,86
27  22/04/2009 00:00:00  Ellie Sullivan  0,99
.
.
.
403  08/11/2013 00:00:00  Diego Gutiérrez  8,91
404  13/11/2013 00:00:00  Helena Holý  25,86
405  21/11/2013 00:00:00  Dan Miller  0,99
406  04/12/2013 00:00:00  Kathy Chase  1,98
407  04/12/2013 00:00:00  John Gordon  1,98
408  05/12/2013 00:00:00  Victor Stevens  3,96
409  06/12/2013 00:00:00  Robert Brown  5,94
410  09/12/2013 00:00:00  Madalena Sampaio  8,91
411  14/12/2013 00:00:00  Terhi Hämäläinen  13,86
412  22/12/2013 00:00:00  Manoj Pareek  1,99

```

Como você modificaria a variável `query` acima, para que a consulta trouxesse somente 10 elementos?

---

a) `query = query.Skip(10);`

>O método `Skip()` serve para pular elementos, porém sem obter esses elementos.

---

b) `query = query.List(10);`

>Não existe um método `List` na biblioteca de LINQ.

---

c) `query = query.ToList(10);`

>O método `ToList()` não possui uma sobrecarga (overload) que aceita um número limite de elementos como argumento.

---

d) `query = query.Take(10);`

>*CORRETO. O método `Take()` retorna um número especificado de elementos contíguos (vizinhos) a partir do início de uma sequência.*

---
e) `query = query.Where(q => q.Index < 10);`

>O método `Where()` é usado para filtrar dados, porém o parâmetro `q` da expressão lambda é do tipo anônimo e não possui a propriedade `Index` (as propriedades disponíveis são: Numero, Data, Cliente, Total).

---

---

---


### 2) Saltando elementos de uma sequência ###

Considere o seguinte *array* contendo nomes de meses:

var meses = new [] {
"Janeiro"
, "Fevereiro"
, "Março"
, "Abril"
, "Maio"
, "Junho"
, "Julho"
, "Agosto"
, "Setembro"
, "Outubro"
, "Novembro"
, "Dezembro" };

Assinale a alternativa que contém a definição da consulta necessária para se obter os nomes dos
meses do segundo semestre.

a) `var query = meses.After(6);

> A biblioteca LINQ não possui nenhum método chamado `After()`.

b) `var query = meses.Take(6);`

> A chamada `meses.Take(6)` irá obter os 6 primeiros elementos da sequência, e não os seis últimos, que correspondem ao segundo semestre.

c) `var query = meses.Where(m => String.Compare(m, "Julho") > 0);`

> A expressão `Where(m => String.Compare(m, "Julho") > 0)` filtra os meses cujo nome aparece
no dicionário após a palavra "Julho", o que não foi pedio no enunciado.  

d) `var query = meses.Skip(6);`

> CORRETO: o método `Skip()` pula os 6 elementos iniciais, que correspondem ao primeiro semestre, logo a consulta irá retornar os elementos restantes, ou seja, os nomes dos meses do segundo semestre.

e) `var query = meses.Where(m => m.Number > 6);`

> A origem de dados `meses` é um *array* de strings que não possuem uma propriedade chamada `Number` que possa ser acessada através da expressão lambda do método `Where`;

---

---

---

### 3) Acessando elementos de uma página ###

Considere o código da consulta abaixo:

```

using (var contexto = new AluraTunesEntities())
{
		
	var query = from nf in contexto.NotasFiscais
				orderby nf.NotaFiscalId
				select new
				{
					Numero = nf.NotaFiscalId,
					Data = nf.DataNotaFiscal,
					Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
					Total = nf.Total
				};

	foreach (var nf in query)
	{
		Console.WriteLine("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente, nf.Total);
	}
}
```

Qual o código necessário para obter os elementos da terceira página de um relatório sobre a consulta acima, considerando que cada página contém 20 elementos.

a) `var pagina = query.Skip(40);` 

> O código `Skip(40)` pula as duas primeiras páginas de dados corretamente. Porém, como o método `Take()` não é chamado, a consulta traz todos os outros elementos da sequência, e não só os da terceira página.

b) `var pagina = query.Skip(20).Take(20);` 

> O código `Skip(20)` está pulando somente os elementos da primeira página.

c) `var pagina = query.Skip(20).Take(10);`

> O código `Skip(20)` está pulando somente os elementos da primeira página.
 
d) `var pagina = query.Skip(40).Take(20);` 

> CORRETO: Como cada página do relatório possui 20 elementos, e queremos obter os elementos da terceira página, então devemos utilizar o método `Skip()` para pular os elementos das duas primeiras páginas, ou seja, 2 x 20 = 40:
> `query.Skip(40)`
> 
> Em seguida, precisamos obter os elementos da página, ou seja, os 20 elementos seguintes da sequência:
> `Take(20)`

e) `var pagina = query.Take(20).Skip(40);` 

> O método `Take` dessa opção está trazendo os 20 primeiros elementos da sequência, e não os da terceira página.

--- 

--- 

--- 

### 4) Obtendo uma sequência de elementos a partir de uma posição ###

Considere o seguinte código C#:

```
using (var contexto = new AluraTunesEntities())
{
		
	var query = from c in contexto.Clientes
				orderby c.ClienteId
				select new { c.ClienteId, Nome = c.PrimeiroNome + " " + c.Sobrenome};

		foreach (var cliente in query)
		{
			Console.WriteLine("{0}\t{1}", pos++, cliente.Nome);
		}
		Console.WriteLine();
}

```

Essa consulta retorna o resultado abaixo:

```
1  Luís Gonçalves
2  Leonie Köhler
3  François Tremblay
4  Bjørn Hansen
5  František Wichterlová
6  Helena Holý
7  Astrid Gruber
8  Daan Peeters
9  Kara Nielsen
10  Eduardo Martins
11  Alexandre Rocha
12  Roberto Almeida
13  Fernanda Ramos
14  Mark Philips
15  Jennifer Peterson
16  Frank Harris
17  Jack Smith
18  Michelle Brooks
19  Tim Goyer
20  Dan Miller
21  Kathy Chase
22  Heather Leacock
23  John Gordon
24  Frank Ralston
25  Victor Stevens
26  Richard Cunningham
27  Patrick Gray
28  Julia Barnett
29  Robert Brown
30  Edward Francis
31  Martha Silk
32  Aaron Mitchell
33  Ellie Sullivan
34  João Fernandes
35  Madalena Sampaio
36  Hannah Schneider
37  Fynn Zimmermann
38  Niklas Schröder
39  Camille Bernard
40  Dominique Lefebvre
41  Marc Dubois
42  Wyatt Girard
43  Isabelle Mercier
44  Terhi Hämäläinen
45  Ladislav Kovács
46  Hugh O'Reilly
47  Lucas Mancini
48  Johannes Van der Berg
49  Stanisław Wójcik
50  Enrique Muñoz
51  Joakim Johansson
52  Emma Jones
53  Phil Hughes
54  Steve Murray
55  Mark Taylor
56  Diego Gutiérrez
57  Luis Rojas
58  Manoj Pareek
59  Puja Srivastava
```

Modifique o código acima para gerar o novo resultado:

```
9  Kara Nielsen
10  Eduardo Martins
11  Alexandre Rocha
12  Roberto Almeida
13  Fernanda Ramos
14  Mark Philips
15  Jennifer Peterson
16  Frank Harris
17  Jack Smith
```

> ### Opinião da Alura ###
> A listagem acima começa a partir do nono elemento, logo devemos pular oito elementos da sequência.
> 
> `Skip(8)`
>
> E a consulta traz 9 elementos, que devemos obter através do método `Take()`.
>
> Assim, definimos uma nova origem de dados, a partir da query de clientes:
>
> `var dadosPagina = query.Skip(8).Take(9);`
>
> Por fim, modificamos a instrução `foreach` para obter os dados a partir da nova variável de consulta `dadosPagina`:
>
> `foreach (var cliente in dadosPagina)`





---

---

---

### 5) Desenvolvendo algoritmo de paginação ###

Observe o código da consulta abaixo:

```

using (var contexto = new AluraTunesEntities())
{
		
	var query = from nf in contexto.NotasFiscais
				orderby nf.NotaFiscalId
				select new
				{
					Numero = nf.NotaFiscalId,
					Data = nf.DataNotaFiscal,
					Cliente = nf.Cliente.PrimeiroNome + " " + nf.Cliente.Sobrenome,
					Total = nf.Total
				};

	foreach (var nf in query)
	{
		Console.WriteLine("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente, nf.Total);
	}
}
```

Ao executar esse programa, você recebe o seguinte resultado:

```
1  01/01/2009 00:00:00  Leonie Köhler  1,98
2  02/01/2009 00:00:00  Bjørn Hansen  3,96
3  03/01/2009 00:00:00  Daan Peeters  5,94
4  06/01/2009 00:00:00  Mark Philips  8,91
5  11/01/2009 00:00:00  John Gordon  13,86
6  19/01/2009 00:00:00  Fynn Zimmermann  0,99
7  01/02/2009 00:00:00  Niklas Schröder  1,98
8  01/02/2009 00:00:00  Dominique Lefebvre  1,98
9  02/02/2009 00:00:00  Wyatt Girard  3,96
10  03/02/2009 00:00:00  Hugh O'Reilly  5,94
11  06/02/2009 00:00:00  Emma Jones  8,91
12  11/02/2009 00:00:00  Leonie Köhler  13,86
13  19/02/2009 00:00:00  Frank Harris  0,99
14  04/03/2009 00:00:00  Jack Smith  1,98
15  04/03/2009 00:00:00  Tim Goyer  1,98
16  05/03/2009 00:00:00  Kathy Chase  3,96
17  06/03/2009 00:00:00  Victor Stevens  5,94
18  09/03/2009 00:00:00  Martha Silk  8,91
19  14/03/2009 00:00:00  Dominique Lefebvre  13,86
20  22/03/2009 00:00:00  Steve Murray  0,99
21  04/04/2009 00:00:00  Mark Taylor  1,98
22  04/04/2009 00:00:00  Luis Rojas  1,98
23  05/04/2009 00:00:00  Puja Srivastava  3,96
24  06/04/2009 00:00:00  Bjørn Hansen  5,94
25  09/04/2009 00:00:00  Eduardo Martins  8,91
26  14/04/2009 00:00:00  Tim Goyer  13,86
27  22/04/2009 00:00:00  Ellie Sullivan  0,99
.
.
.
403  08/11/2013 00:00:00  Diego Gutiérrez  8,91
404  13/11/2013 00:00:00  Helena Holý  25,86
405  21/11/2013 00:00:00  Dan Miller  0,99
406  04/12/2013 00:00:00  Kathy Chase  1,98
407  04/12/2013 00:00:00  John Gordon  1,98
408  05/12/2013 00:00:00  Victor Stevens  3,96
409  06/12/2013 00:00:00  Robert Brown  5,94
410  09/12/2013 00:00:00  Madalena Sampaio  8,91
411  14/12/2013 00:00:00  Terhi Hämäläinen  13,86
412  22/12/2013 00:00:00  Manoj Pareek  1,99

```

Como você modificaria o código, de forma a trazer o relatório abaixo?

```
Página: 1

1  01/01/2009 00:00:00  Leonie Köhler  1,98
2  02/01/2009 00:00:00  Bjørn Hansen  3,96
3  03/01/2009 00:00:00  Daan Peeters  5,94
4  06/01/2009 00:00:00  Mark Philips  8,91
5  11/01/2009 00:00:00  John Gordon  13,86

Página: 2

6  19/01/2009 00:00:00  Fynn Zimmermann  0,99
7  01/02/2009 00:00:00  Niklas Schröder  1,98
8  01/02/2009 00:00:00  Dominique Lefebvre  1,98
9  02/02/2009 00:00:00  Wyatt Girard  3,96
10  03/02/2009 00:00:00  Hugh O''Reilly  5,94

Página: 3

11  06/02/2009 00:00:00  Emma Jones  8,91
12  11/02/2009 00:00:00  Leonie Köhler  13,86
13  19/02/2009 00:00:00  Frank Harris  0,99
14  04/03/2009 00:00:00  Jack Smith  1,98
15  04/03/2009 00:00:00  Tim Goyer  1,98

Página: 4

16  05/03/2009 00:00:00  Kathy Chase  3,96
17  06/03/2009 00:00:00  Victor Stevens  5,94
18  09/03/2009 00:00:00  Martha Silk  8,91
19  14/03/2009 00:00:00  Dominique Lefebvre  13,86
20  22/03/2009 00:00:00  Steve Murray  0,99

Página: 5

21  04/04/2009 00:00:00  Mark Taylor  1,98
22  04/04/2009 00:00:00  Luis Rojas  1,98
23  05/04/2009 00:00:00  Puja Srivastava  3,96
24  06/04/2009 00:00:00  Bjørn Hansen  5,94
25  09/04/2009 00:00:00  Eduardo Martins  8,91

.
.
.

Página: 81

401  04/11/2013 00:00:00  Hugh O''Reilly  3,96
402  05/11/2013 00:00:00  Enrique Muñoz  5,94
403  08/11/2013 00:00:00  Diego Gutiérrez  8,91
404  13/11/2013 00:00:00  Helena Holý  25,86
405  21/11/2013 00:00:00  Dan Miller  0,99

Página: 82

406  04/12/2013 00:00:00  Kathy Chase  1,98
407  04/12/2013 00:00:00  John Gordon  1,98
408  05/12/2013 00:00:00  Victor Stevens  3,96
409  06/12/2013 00:00:00  Robert Brown  5,94
410  09/12/2013 00:00:00  Madalena Sampaio  8,91

Página: 83

411  14/12/2013 00:00:00  Terhi Hämäläinen  13,86
412  22/12/2013 00:00:00  Manoj Pareek  1,99
```

---

> ### Opinião da Alura ###
> 
> Pelo resultado do relatório acima, verificamos que cada página contém 5 elementos. Então declaramos uma constante com o valor 5:
>
> `const int TAMANHO_PAGINA = 5;`
> 
> E então descobrimos o tamanho do resultado da consulta:
>
> `int totalPaginas = query.Count();`
>
> Em seguida, podemos definir um laço com uma instrução `for`. Começando pela página 1, devemos pegar todas as páginas até a página final, ou seja, enquanto o cálculo da posição do primeiro elemento da página não extrapolar a quantidade de elementos do relatório:
>

```
for (var pagina = 1; (pagina - 1) * TAMANHO_PAGINA < totalPaginas; pagina++)
    {

	}
```

> Em seguida, criamos uma consulta com o método `Skip()` para pular os elementos das páginas anteriores, e o método `Take()` para pegar a quantidade de elementos da página atual apenas: 
>

```
var dadosPagina = query
					.Skip(TAMANHO_PAGINA * (pagina - 1))
					.Take(TAMANHO_PAGINA);
```

> O restante do código pode ser feito para exibir o número da página e varrer os dados da página e imprimi-los:

```
	Console.WriteLine("Página: {0}", pagina);
	Console.WriteLine();
	foreach (var nf in dadosPagina)
	{
		Console.WriteLine("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente, nf.Total);
	}
	Console.WriteLine();
```

E, finalmente, teríamos o código abaixo:

```
	const int TAMANHO_PAGINA = 5;
	int totalPaginas = query.Count();
	
	for (var pagina = 1; TAMANHO_PAGINA * (pagina - 1) < totalPaginas; pagina++)
	{
		var dadosPagina = query
							.Skip(TAMANHO_PAGINA * (pagina - 1))
							.Take(TAMANHO_PAGINA);
	
		Console.WriteLine("Página: {0}", pagina);
		Console.WriteLine();
		foreach (var nf in dadosPagina)
		{
			Console.WriteLine("{0}\t{1}\t{2}\t{3}", nf.Numero, nf.Data, nf.Cliente, nf.Total);
		}
		Console.WriteLine();
	}
```

---

---

---

