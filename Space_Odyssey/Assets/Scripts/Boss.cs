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
    private int healthPoints =2;

    [SerializeField]
    private float tirodelay = 3f;

    private bool canShoot = true;




    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            InvokeRepeating(nameof(Tiro), tirodelay, tirodelay);
            canShoot = false;
        }
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