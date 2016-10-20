# Aula 4 #

*Aproveite recursos de programação paralela com Parallel LINQ*
* Medindo tempo de execução de uma consulta com o objeto StopWatch
* Paralelizando execução de consulta com método AsParallel()
* Paralelizando execução de um laço com o método ForAll()

1) Medindo tempo de execução de uma consulta

Considere o seguinte trecho de código:

```
using (var contexto = new AluraTunesEntities())
{
    var query = 
        from alb in contexto.Albums
        join art in contexto.Artistas 
            on alb.ArtistaId equals art.ArtistaId
        where art.Nome == textoBusca
        select alb;

    var result = query.ToList();
}

```

Como você modificaria o código acima para obter o tempo total de execução da consulta?

**a**
```
var result = query.ToList();
Console.WriteLine("A consulta executou em {0:hh\\:mm\\:ss}: ", query.ExecutionTime);
```

> A variável de consulta `query` é uma sequência de elementos do tipo Album, o qual não expõe
> uma propriedade chamada **ExecutionTime**.

**b**
```
var result = query.ToList();
Console.WriteLine("A consulta executou em {0:hh\\:mm\\:ss}: ", result.ExecutionTime);
```

> A variável `result` é uma lista (System.Collections.Generic.List) e não implementa uma propriedade
> `ExecutionTime`.

**c**

```
Stopwatch sw = Stopwatch.StartNew();
var query = 
    from alb in contexto.Albums
    join art in contexto.Artistas 
        on alb.ArtistaId equals art.ArtistaId
    where art.Nome == textoBusca
    select alb;
TimeSpan elapsed = sw.Elapsed;
Console.WriteLine("A consulta executou em {0:hh\\:mm\\:ss}: ", stopwatch.Elapsed);
```

> O objeto `StopWatch` é utilizado para medir o tempo decorrido, porém essa alternativa apresenta
> a medição do tempo de criação da consulta `query`, e não da sua execução, como pede o enunciado.  

**d**

```
Stopwatch sw = Stopwatch.StartNew();
var result = query.ToList();
TimeSpan elapsed = sw.Elapsed;
Console.WriteLine("A consulta executou em {0:hh\\:mm\\:ss}: ", stopwatch.Elapsed);
```

> CORRETO. O objeto `StopWatch` é utilizado para medir o tempo decorrido, e aqui ele está sendo
> aplicado para medir o tempo de execução da consulta `query`, que ocorre quando o método
> `ToList()` é invocado.

**e**

`var result = query.ToList().ExecutionTime;`

> Não existe uma propriedade `ExecutionTime` no `objeto System.Collections.Generic.List`. 

2) Paralelizando execução de consulta



3) Paralelizando execução de um laço foreach
4) 