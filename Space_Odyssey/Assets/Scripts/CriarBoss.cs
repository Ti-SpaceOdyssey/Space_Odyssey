using UnityEngine;

public class CriarBoss : MonoBehaviour
{
    public static bool bossMorto = true;
    [SerializeField]
    private GameObject bossPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(GerarBoss), 2f, 60f); // Chama o método GerarBoss a cada 2 minutos
    }

    private void GerarBoss()
    {
        if (!bossMorto) // Verifica se o boss anterior já foi derrotado
        {
            Debug.Log("Ainda há um boss ativo. Não é possível gerar outro boss.");
            return;
        }

        bossPrefab.layer = 6;

        Instantiate(bossPrefab, transform.position, transform.rotation);
        Gerar.bossCriado = true; // Atualiza a variável para desativar a geração de inimigos em todos os objetos Gerar
        bossMorto = false;
    }

    private void OnDestroy()
    {
        bossMorto = true;
        Gerar.bossCriado = false;
    }
}
