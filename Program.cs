// Atividade Pratica 02 - Algoritmos em Grafos (PUC Minas Betim)
// Parque turistico: menores caminhos (Dijkstra) e Arvore Geradora Minima (Prim)

class Program
{
    static void Main(string[] args)
    {
        // ----- Montagem do grafo do parque -----
        string[] pontos =
        {
            "CentroVisitantes",   // V0
            "MiranteDoSol",       // V1
            "CachoeiraGrande",    // V2
            "AreaDescanso",       // V3
            "Restaurante",        // V4
            "LagoAzul",           // V5
            "MiranteDaSerra",     // V6
            "CachoeiraEscondida"  // V7
        };

        Grafo parque = new Grafo(pontos);

        // trilhas (origem, destino, distancia em km)
        parque.AdicionarAresta(0, 1, 4);
        parque.AdicionarAresta(0, 3, 2);
        parque.AdicionarAresta(0, 4, 5);
        parque.AdicionarAresta(1, 2, 3);
        parque.AdicionarAresta(1, 3, 6);
        parque.AdicionarAresta(2, 5, 4);
        parque.AdicionarAresta(2, 6, 7);
        parque.AdicionarAresta(3, 4, 1);
        parque.AdicionarAresta(3, 5, 8);
        parque.AdicionarAresta(4, 5, 3);
        parque.AdicionarAresta(4, 7, 9);
        parque.AdicionarAresta(5, 6, 2);
        parque.AdicionarAresta(5, 7, 6);
        parque.AdicionarAresta(6, 7, 1);

        // ----- Estrutura do grafo -----
        Console.WriteLine("==============================================");
        Console.WriteLine("   PARQUE TURISTICO - GRAFO DE TRILHAS");
        Console.WriteLine("==============================================");
        Console.WriteLine();
        Console.WriteLine("Lista de adjacencia (vizinho e distancia):");
        parque.ExibirListaAdjacencia();
        Console.WriteLine();
        Console.WriteLine("Trilhas existentes:");
        parque.ExibirArestas();
        Console.WriteLine();

        // ----- Leitura do vertice de origem -----
        int n = parque.NumVertices;
        Console.Write("Informe o vertice de origem (0 a " + (n - 1) + "): ");
        string entrada = Console.ReadLine();
        int origem;

        if (!int.TryParse(entrada, out origem) || origem < 0 || origem >= n)
        {
            Console.WriteLine("Entrada invalida. Usando V0 como origem padrao.");
            origem = 0;
        }

        // ----- Dijkstra -----
        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine(" DIJKSTRA - menores caminhos a partir de " + parque.Nome(origem));
        Console.WriteLine("==============================================");

        int[] distancia;
        int[] anterior;
        parque.Dijkstra(origem, out distancia, out anterior);

        Console.WriteLine(string.Format("{0,-22} {1,-8} {2}", "Destino", "Custo", "Caminho"));
        Console.WriteLine(new string('-', 60));

        for (int v = 0; v < n; v++)
        {
            if (v == origem)
                continue;

            if (distancia[v] == int.MaxValue)
            {
                Console.WriteLine(string.Format("{0,-22} {1,-8} {2}", parque.Nome(v), "-", "sem caminho"));
            }
            else
            {
                Console.WriteLine(string.Format("{0,-22} {1,-8} {2}",
                    parque.Nome(v), distancia[v] + " km", parque.MontarCaminho(v, anterior)));
            }
        }

        // ----- Prim -----
        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine(" PRIM - Arvore Geradora Minima (inicio em " + parque.Nome(origem) + ")");
        Console.WriteLine("==============================================");

        List<int[]> agm = parque.Prim(origem);
        int soma = 0;

        for (int i = 0; i < agm.Count; i++)
        {
            int u = agm[i][0];
            int v = agm[i][1];
            int peso = agm[i][2];
            soma += peso;
            Console.WriteLine(string.Format("{0}. {1,-20} -- {2,-22} {3} km",
                i + 1, parque.Nome(u), parque.Nome(v), peso));
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine("Arestas na AGM: " + agm.Count + " (n - 1 = " + (n - 1) + ")");
        Console.WriteLine("Soma total dos pesos: " + soma + " km");
        Console.WriteLine();
        Console.WriteLine("Pressione ENTER para sair...");
        Console.ReadLine();
    }
}
