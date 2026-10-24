// Classe que representa o grafo do parque turistico.
// Grafo nao direcionado e ponderado, armazenado em lista de adjacencia.

public class Aresta
{
    public int Destino;
    public int Peso;

    public Aresta(int destino, int peso)
    {
        Destino = destino;
        Peso = peso;
    }
}

public class Grafo
{
    private int numVertices;
    private string[] nomes;
    private List<Aresta>[] adjacencia;

    public Grafo(string[] nomesVertices)
    {
        numVertices = nomesVertices.Length;
        nomes = nomesVertices;
        adjacencia = new List<Aresta>[numVertices];

        for (int i = 0; i < numVertices; i++)
        {
            adjacencia[i] = new List<Aresta>();
        }
    }

    public int NumVertices
    {
        get { return numVertices; }
    }

    public string Nome(int v)
    {
        return "V" + v + "-" + nomes[v];
    }

    // Como o grafo e nao direcionado, a trilha e inserida nos dois sentidos
    public void AdicionarAresta(int origem, int destino, int peso)
    {
        adjacencia[origem].Add(new Aresta(destino, peso));
        adjacencia[destino].Add(new Aresta(origem, peso));
    }

    // Imprime a lista de adjacencia com o peso de cada ligacao
    public void ExibirListaAdjacencia()
    {
        for (int i = 0; i < numVertices; i++)
        {
            Console.Write(Nome(i) + " -> ");

            for (int j = 0; j < adjacencia[i].Count; j++)
            {
                Aresta a = adjacencia[i][j];
                Console.Write(Nome(a.Destino) + " (" + a.Peso + " km)");

                if (j < adjacencia[i].Count - 1)
                    Console.Write(", ");
            }
            Console.WriteLine();
        }
    }

    // Imprime cada trilha uma unica vez (u < v), com o peso
    public void ExibirArestas()
    {
        int total = 0;
        for (int i = 0; i < numVertices; i++)
        {
            foreach (Aresta a in adjacencia[i])
            {
                if (i < a.Destino)
                {
                    Console.WriteLine("  " + Nome(i) + " -- " + Nome(a.Destino) + " : " + a.Peso + " km");
                    total++;
                }
            }
        }
        Console.WriteLine("Total: " + numVertices + " vertices e " + total + " arestas.");
    }

    // ---------------- DIJKSTRA ----------------
    // Calcula a menor distancia da origem ate todos os outros vertices.
    // distancia[v] guarda o menor custo encontrado ate v
    // anterior[v] guarda de qual vertice viemos para chegar em v (para montar o caminho)
    public void Dijkstra(int origem, out int[] distancia, out int[] anterior)
    {
        distancia = new int[numVertices];
        anterior = new int[numVertices];
        bool[] fechado = new bool[numVertices];

        for (int i = 0; i < numVertices; i++)
        {
            distancia[i] = int.MaxValue;
            anterior[i] = -1;
            fechado[i] = false;
        }
        distancia[origem] = 0;

        for (int passo = 0; passo < numVertices; passo++)
        {
            // escolhe o vertice aberto com a menor distancia
            int u = -1;
            for (int i = 0; i < numVertices; i++)
            {
                if (!fechado[i] && distancia[i] != int.MaxValue)
                {
                    if (u == -1 || distancia[i] < distancia[u])
                        u = i;
                }
            }

            if (u == -1)
                break; // nao ha mais vertices alcancaveis

            fechado[u] = true;

            // relaxamento das arestas que saem de u
            foreach (Aresta a in adjacencia[u])
            {
                int v = a.Destino;
                if (!fechado[v] && distancia[u] + a.Peso < distancia[v])
                {
                    distancia[v] = distancia[u] + a.Peso;
                    anterior[v] = u;
                }
            }
        }
    }

    // Monta o caminho da origem ate o destino usando o vetor anterior
    public string MontarCaminho(int destino, int[] anterior)
    {
        List<int> caminho = new List<int>();
        int atual = destino;

        while (atual != -1)
        {
            caminho.Add(atual);
            atual = anterior[atual];
        }
        caminho.Reverse();

        string texto = "";
        for (int i = 0; i < caminho.Count; i++)
        {
            texto += "V" + caminho[i];
            if (i < caminho.Count - 1)
                texto += " -> ";
        }
        return texto;
    }

    // ---------------- PRIM ----------------
    // Monta a Arvore Geradora Minima a partir do vertice inicial.
    // A cada passo escolhe a aresta mais leve que liga um vertice
    // ja na arvore a um vertice que ainda nao esta nela.
    public List<int[]> Prim(int inicio)
    {
        bool[] naArvore = new bool[numVertices];
        int[] custo = new int[numVertices];   // menor aresta que liga v a arvore
        int[] pai = new int[numVertices];     // vertice da arvore ligado a v
        List<int[]> arestasAGM = new List<int[]>();

        for (int i = 0; i < numVertices; i++)
        {
            custo[i] = int.MaxValue;
            pai[i] = -1;
        }
        custo[inicio] = 0;

        for (int passo = 0; passo < numVertices; passo++)
        {
            // escolhe o vertice fora da arvore com a menor aresta de ligacao
            int u = -1;
            for (int i = 0; i < numVertices; i++)
            {
                if (!naArvore[i] && custo[i] != int.MaxValue)
                {
                    if (u == -1 || custo[i] < custo[u])
                        u = i;
                }
            }

            if (u == -1)
                break; // grafo desconexo

            naArvore[u] = true;

            if (pai[u] != -1)
                arestasAGM.Add(new int[] { pai[u], u, custo[u] });

            // atualiza o custo dos vizinhos que ainda nao estao na arvore
            foreach (Aresta a in adjacencia[u])
            {
                if (!naArvore[a.Destino] && a.Peso < custo[a.Destino])
                {
                    custo[a.Destino] = a.Peso;
                    pai[a.Destino] = u;
                }
            }
        }

        return arestasAGM;
    }
}
