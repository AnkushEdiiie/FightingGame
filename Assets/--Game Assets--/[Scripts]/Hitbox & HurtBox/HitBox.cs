using UnityEngine;

public class HitBox : MonoBehaviour
{
    public HandlersDetails Handlers;

    private void OnTriggerEnter(Collider other)
    {
        HurtBox hurtBox = other.GetComponent<HurtBox>();

        if (hurtBox != null)
        {

            if (GameManager_Old.instance.GameMode == GameType.P1vsComp)
            {
                Handlers = hurtBox._currentPlayer._details();

                if (Handlers != null && Handlers._Player_A_Handler.CompareTag("P1"))  // Hit by Player
                {
                    Handlers._damageHandler.SetDamageToAI(Handlers._Player_A_Handler, Handlers._AI_Handler, GameManager_Old.instance._charB_index, transform.position);
                }

                if (Handlers != null && Handlers._AI_Handler.CompareTag("P2"))   // Hit by AI
                {
                    Handlers._damageHandler.SetDamageToPlayer(Handlers._AI_Handler, Handlers._Player_A_Handler, GameManager_Old.instance._charA_index, transform.position);
                }

            }

            if(GameManager_Old.instance.GameMode == GameType.P1vsP2)
            {
                Handlers = hurtBox._currentPlayer._details();   // Hit by Player A

                if(Handlers != null && Handlers._Player_A_Handler.CompareTag("P1"))
                {
                    Handlers._damageHandler.SetDamageToBothPlayer(Handlers._Player_A_Handler, Handlers._Player_B_Handler, GameManager_Old.instance._charB_index, transform.position);
                }

                if(Handlers != null && Handlers._Player_B_Handler.CompareTag("P2")) // Hit by Player B
                {
                    Handlers._damageHandler.SetDamageToBothPlayer(Handlers._Player_B_Handler, Handlers._Player_A_Handler, GameManager_Old.instance._charA_index, transform.position);
                }

            }
        }
    }
}