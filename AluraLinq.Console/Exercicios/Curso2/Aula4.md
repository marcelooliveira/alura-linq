# Aula 4 #

*Aproveite recursos de programação paralela com Parallel LINQ*
* Medindo tempo de execução de uma consulta com o objeto StopWatch
* Paralelizando execução de consulta com método AsParallel()
* Paralelizando execução de um laço com o método ForAll()

**1) Medindo tempo de execução de uma consulta**

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

**2) Paralelizando execução de consulta - Prática**

Considere a seguinte consulta LINQ:

```
var queryCodigos =
listaFaixas
.Select(f => new
{
    Arquivo = string.Format("{0}\\{1}.jpg", Imagens, f.FaixaId),
    Imagem = barcodWriter.Write(string.Format("aluratunes.com/faixa/{0}", f.FaixaId))
});
```

Supondo que o método `barcodWriter.Write` exija um grande esforço computacional, como você
modificaria o código acima para utilizar LINQ paralelo?

**a**

```
var queryCodigos =
listaFaixas
.Select(f => new
{
    Arquivo = string.Format("{0}\\{1}.jpg", Imagens, f.FaixaId),
    Imagem = barcodWriter.Write(string.Format("aluratunes.com/faixa/{0}", f.FaixaId))
})
.AsParallel();
```

> O método `AsParallel()` especifica que o restante da consulta deve ser paralelizada. Porém,
> quando o método é utilizado neste exemplo, o resultado já tinha sido obtido através do método
> `Select`. Ou seja, como o método `AsParallel` é o último comando da consulta, ele perdeu o
> seu propósito. O correto seria invocar o método AsParallel depois da origem de dados
> (`listaFaixas`).

**b**

```
var queryCodigos =
listaFaixas
.Select(f => new
{
    Arquivo = string.Format("{0}\\{1}.jpg", Imagens, f.FaixaId),
    Imagem = barcodWriter.Write(string.Format("aluratunes.com/faixa/{0}", f.FaixaId))
});

queryCodigos = queryCodigos.AsParallel();
```

> O método `AsParallel()` especifica que o restante da consulta deve ser paralelizada. Porém,
> quando o método é utilizado neste exemplo, o resultado já tinha sido obtido através do método
> `Select`. Ou seja, como o método `AsParallel` é o último comando da consulta, ele perdeu o
> seu propósito. O correto seria invocar o método AsParallel depois da origem de dados
> (`listaFaixas`).

**c**

```
var queryCodigos =
new Parallel(listaFaixas
.Select(f => new
{
    Arquivo = string.Format("{0}\\{1}.jpg", Imagens, f.FaixaId),
    Imagem = barcodWriter.Write(string.Format("aluratunes.com/faixa/{0}", f.FaixaId))
}));
```

> A biblioteca LINQ não possui uma classe `Parallel` que possa ser usada para paralelizar uma
> consulta.

**d**

```
var queryCodigos =
listaFaixas
.AsParallel()
.Select(f => new
{
    Arquivo = string.Format("{0}\\{1}.jpg", Imagens, f.FaixaId),
    Imagem = barcodWriter.Write(string.Format("aluratunes.com/faixa/{0}", f.FaixaId))
});
```

> CORRETO: o método `AsParallel()` é aplicado a um objeto do tipo `IEnumerable` (`listaFaixas`) 
> e especifica que o restante da consulta deve ser paralelizada, se possível.  

**e**

```
var queryCodigos =
listaFaixas
.Paralelize()
.Select(f => new
{
    Arquivo = string.Format("{0}\\{1}.jpg", Imagens, f.FaixaId),
    Imagem = barcodWriter.Write(string.Format("aluratunes.com/faixa/{0}", f.FaixaId))
});
```

> Não existe um método de extensão chamado `Paralelize` que possa ser aplicado à origem de dados
> `listaFaixas`. 

**3) Paralelizando execução de consulta - Teoria**

Qual a principal diferença entre o LINQ e o LINQ paralelo? Explique com suas palavras.

> A principal diferença é que PLINQ tentar usar completamente todos os processadores no sistema. 
> Ele faz isso particionando a fonte de dados em segmentos, e então executando a consulta em cada 
> segmento em segmentos separados de trabalho paralelamente em vários processadores. Em muitos casos, 
> a execução em paralelo significa que a consulta é executada de forma significativamente mais rápida.

**4) Paralelizando execução de um laço foreach**

Considere o código abaixo, que usa o método `AsParallel()` para paralelizar a consulta:

```
var queryCodigos =
listaFaixas
.AsParallel()
.Select(f => new
{
    Arquivo = string.Format("{0}\\{1}.jpg", Imagens, f.FaixaId),
    Imagem = barcodWriter.Write(string.Format("aluratunes.com/faixa/{0}", f.FaixaId))
});

foreach (var item in queryCodigos)
{
    item.Imagem.Save(item.Arquivo, ImageFormat.Jpeg);
}
```

Dentro do laço `foreach`, cada imagem gerada anteriormente é salva em arquivo, em série. Como você
otimizaria a execução desse laço `foreach` para que a gravação dos arquivos fosse feita em
paralelo?

**a**

```
forall (var item in queryCodigos)
{
    item.Imagem.Save(item.Arquivo, ImageFormat.Jpeg);
}
```

> Não existe uma instrução `forall` na linguagem C#.

**b**

```
foreach (var item in queryCodigos.AsParallel())
{
    item.Imagem.Save(item.Arquivo, ImageFormat.Jpeg);
}
```

> A consulta `queryCodigos` já havia sido paralelizada com o método `AsParallel`.

**c**

```
queryCodigos.AsParallel(item => 
{
    item.Imagem.Save(item.Arquivo, ImageFormat.Jpeg);
});
```

> Não existe um método de extensão `AsParallel` na biblioteca de LINQ que possa ser aplicado a 
> um tipo `IQueryable` como `queryCodigos`. 

**d**

```
queryCodigos.ForAll(item => 
{
    item.Imagem.Save(item.Arquivo, ImageFormat.Jpeg);
});
```

> CORRETO: O método `ForAll` faz com que cada método `Save` seja invocado em paralelo.

**e**

```
queryCodigos.ForEach(item => 
{
    item.Imagem.Save(item.Arquivo, ImageFormat.Jpeg);
});
```

> Não existe um método de extensão `ForEach` na biblioteca de LINQ que possa ser aplicado a 
> um tipo `IQueryable` como `queryCodigos`. 

