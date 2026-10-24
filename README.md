# Atividade Prática 02 – Algoritmos em Grafos

PUC Minas Betim – Algoritmos em Grafos (Profª Roselene Henrique)
Alunos: Thiago Grandim das Merces e Miguel de Paula Gonçalves

Programa em C# que representa um parque turístico como um grafo **não direcionado, ponderado e conexo**
(8 pontos de interesse, 14 trilhas, pesos em km) e executa:

- **Dijkstra**: menores distâncias e caminhos a partir de um ponto de origem informado pelo usuário;
- **Prim**: Árvore Geradora Mínima (trilhas suficientes para manter todos os pontos interligados com a menor distância total).

## Vértices

| Vértice | Ponto de interesse |
|---|---|
| V0 | Centro de Visitantes |
| V1 | Mirante do Sol |
| V2 | Cachoeira Grande |
| V3 | Área de Descanso |
| V4 | Restaurante |
| V5 | Lago Azul |
| V6 | Mirante da Serra |
| V7 | Cachoeira Escondida |

## Arquivos

- `Grafo.cs` – classe `Grafo` (lista de adjacência), Dijkstra e Prim
- `Program.cs` – montagem do grafo do parque, leitura da origem e exibição dos resultados
- `ParqueGrafos.csproj` – projeto .NET 8

## Como executar

Requer o .NET 8 SDK.

```
dotnet run
```

Informe o vértice de origem (0 a 7). Uma entrada inválida usa V0 como origem padrão.
