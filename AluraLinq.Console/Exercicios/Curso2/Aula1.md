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
