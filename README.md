# Curso testes em C# com NUnit

[Introduction to .NET Testing with NUnit 3](https://app.pluralsight.com/library/courses/nunit-3-dotnet-testing-introduction/exercise-files)

## How it Works
`NUnit` irá criar uma instância da classe de teste para cada Classe, ao contrário do `XUnit` que irá criar uma instância de teste para cada método a ser testado.

### Life Cycle

**Test class**: Uma instância por execução

**One-time Setup**: Executa uma única vez, antes do primeiro teste

**Setup**: Executado antes de cada método teste

**Tear down**: Executado após cada método de teste

**One-time Tear Down**: Executado uma vez, após todos os testes terem sido efetuados

**Dispose**: Antes do método ser "Descartado"

![](https://i.imgur.com/tqBNkRz.png)

## Visualizar Testes no Visual Studio
Visual Studio: Test -> Windows -> Test Explorer

Visão:
![](https://i.imgur.com/gBmh0uh.png)

## Terminal

Comandos:
```shell

# Executa todos os testes
dotnet test

# Lista testes sem executá-los
dotnet test --list-tests

# Mostrar comandos disponíveis
dotnet test \?

# Executar testes por categoria
dotnet test --filter "TestCategory=Product Comparison"
```

[Documentação](http://bit.ly/psdotnettest)

## Testes de Qualidades

Testes de qualidade são:
- Rápidos
- Repetiveis (mediante um mesmo valor, retorna o mesmo resultado independente de quantas vezes executado)
- Isolados (um teste não deve interferir no outro)
- Confiáveis (o resultado de um teste deve ser confiável)
- Valor (o teste deve agregar valor ao projeto e testar se as regras estão corretamente funcionando. não devemos criar testes que não façam sentido)

## Asserts

Efetuam uma ação e retornam se o resultado foi o esperado ou não. Um teste só é válido se todos os Asserts passarem.

Um teste pode ter múltiplos asserts, mas apenas, se todas as asserts é para testar um mesmo Comportamento. Cada teste deve testar apenas um comportamento.

[Lista de constraints](http://bit.ly/nunit3asserts)

## Mensagens

Você pode definir mensagens de erro para testes cujo funcionamento seja um pouco "obscuro", porém, no geral, o próprio nome deve deixar claro o que está sendo testado.