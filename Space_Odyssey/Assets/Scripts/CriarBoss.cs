using System.Collections;
using UnityEngine;
using TMPro;

public class CriarBoss : MonoBehaviour
{
    public static bool bossMorto = true;
    [SerializeField]
    private GameObject bossPrefab;

    [SerializeField]
    private TMP_Text prepareTextMesh; // TextMeshPro do "Prepare-se"
    [SerializeField]
    private TMP_Text countdownTextMesh; // TextMeshPro da contagem regressiva

    private void Start()
    {
        InvokeRepeating(nameof(GerarBoss), 15f, 120f); // Chama o método GerarBoss a cada 2 minutos
    }

    private IEnumerator ShowPrepareAndCountdown()
    {
        prepareTextMesh.gameObject.SetActive(true);
        countdownTextMesh.gameObject.SetActive(true);

        prepareTextMesh.text = "Prepare-se";

        yield return new WaitForSeconds(1f);

        countdownTextMesh.text = "3";
        yield return new WaitForSeconds(1f);

        countdownTextMesh.text = "2";
        yield return new WaitForSeconds(1f);

        countdownTextMesh.text = "1";
        yield return new WaitForSeconds(1f);

        prepareTextMesh.gameObject.SetActive(false);
        countdownTextMesh.text = "";
        yield return new WaitForSeconds(1f);

        
        countdownTextMesh.gameObject.SetActive(false);
    }

    private void GerarBoss()
    {
        if (!bossMorto) // Verifica se o boss anterior já foi derrotado
        {
            Debug.Log("Ainda há um boss ativo. Não é possível gerar outro boss.");
            return;
        }

        bossPrefab.layer = 6;

        StartCoroutine(ShowPrepareAndCountdown()); // Inicia a exibição dos textos "Prepare-se" e a contagem regressiva

        StartCoroutine(CreateBossAfterDelay());
    }

    private IEnumerator CreateBossAfterDelay()
    {
        yield return new WaitForSeconds(4f); // Aguarda 4 segundos (1 segundo para cada número da contagem regressiva)

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
