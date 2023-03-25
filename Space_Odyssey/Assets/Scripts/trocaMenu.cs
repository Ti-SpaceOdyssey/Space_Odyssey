using UnityEngine;
using UnityEngine.SceneManagement;

public class trocaMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject painelOpcoes;
    
    [SerializeField]
    private GameObject painelInicial;

        void Start()
    {

        painelOpcoes.SetActive(false);
    }

    public void AbrirOpcoes (){
        painelInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void ConfirmarOpcoes (){
        painelOpcoes.SetActive(false);
        painelInicial.SetActive(true);
    }

}
