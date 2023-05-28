using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Boss : MonoBehaviour
{
    [Header("Tiro")]
    [SerializeField]
    private Transform LugarTiro;

    [SerializeField]
    private GameObject prefabTiro;

    [SerializeField]
    private GameObject flashObject;

    [SerializeField]
    private GameObject hitPrefab;

    [SerializeField]
    private GameObject explosaoPrefab;

    [SerializeField]
    private  int healthPoints =2;

    [SerializeField]
    private float tirodelay = 3f;

    private bool canShoot = true;

    [Header("Movimento")]
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float upperLimit = 5f;
    [SerializeField]
    private float lowerLimit = -5f;

    private bool movingUp = false; // Alterado para false

    private bool enteringScreen = true; // Variável para controlar a animação inicial

    private Vector3 entryStartPosition; // Posição inicial para a animação de entrada
    private Vector3 entryEndPosition; // Posição final para a animação de entrada

    private float entryAnimationDuration = 2f; // Duração da animação de entrada
    private float entryAnimationTimer = 0f; // Tempo decorrido da animação de entrada

    // Start is called before the first frame update
    void Start()
    {
        entryStartPosition = new Vector3(50.02f, 2.88554f, 0f); // Defina a posição inicial da animação de entrada
        entryEndPosition = transform.position; // A posição final é a posição atual do chefe

        StartCoroutine(EntryAnimation()); // Iniciar a animação de entrada
    }

    // Coroutine para a animação de entrada
    private IEnumerator EntryAnimation()
    {
        while (entryAnimationTimer < entryAnimationDuration)
        {
            entryAnimationTimer += Time.deltaTime;

            // Interpolação linear entre a posição inicial e a posição final
            float t = entryAnimationTimer / entryAnimationDuration;
            transform.position = Vector3.Lerp(entryStartPosition, entryEndPosition, t);

            yield return null;
        }

        enteringScreen = false; // A animação de entrada foi concluída, permitir o movimento normal
    }

    // Update is called once per frame
    void Update()
    {
        if (!enteringScreen && canShoot) // Somente atire quando a animação de entrada estiver concluída
        {
            InvokeRepeating(nameof(Tiro), tirodelay, tirodelay);
            canShoot = false;
        }
        Move();
    }

    private void Tiro()
    {
        GameObject novoTiro = Instantiate(prefabTiro, LugarTiro.position, LugarTiro.rotation);

        //A*
        AIPath aiPath = novoTiro.AddComponent<AIPath>();
        aiPath.orientation = OrientationMode.YAxisForward;
        aiPath.gravity = Vector3.zero; // Remover a gravidade
        aiPath.radius = 0.08f;
        aiPath.maxSpeed = 0.90f;
        aiPath.pickNextWaypointDist = 2f; //1f;
        aiPath.endReachedDistance = 0.1f;
        aiPath.maxAcceleration = 10f;
        aiPath.whenCloseToDestination = CloseToDestinationMode.ContinueToExactDestination;
        aiPath.slowdownDistance = 0f;

        GameObject playerGameObject = GameObject.Find("Player");
        if (playerGameObject != null)
        {
            Transform playerTransform = playerGameObject.transform;
            AIDestinationSetter aiDestinationSetter = novoTiro.AddComponent<AIDestinationSetter>();
            aiDestinationSetter.target = playerTransform;
        }
        flashObject.SetActive(true);
    }

     private void Move()
    {
        if (enteringScreen) // Se ainda estiver na animação de entrada, não execute o movimento normal
            return;

        if (movingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            if (transform.position.y >= upperLimit)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            if (transform.position.y <= lowerLimit)
            {
                movingUp = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bala") && gameObject.tag == "Boss" || other.CompareTag("Meteoro") && gameObject.tag == "EnemyTiro")
        {
            // hit

            Instantiate(hitPrefab, other.transform.position, hitPrefab.transform.rotation);
            Destroy(other.gameObject);

            // Update Health Points

            healthPoints--;

            // Se saude igual ou menor que 0 -> destruir

            if (healthPoints <= 0)
            {
                ControleExplosao.Instance.Create(transform.position, transform.rotation);
                Destroy(gameObject);
            
                CriarBoss.bossMorto = true; // false;

                Pontuacao.editPontos = Pontuacao.editPontos + 100;
            }
        }
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
}
