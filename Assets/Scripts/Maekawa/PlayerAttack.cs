using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // o“ü‚è‚·‚é‚Æ•¡”‰ñŒÄ‚Î‚ê‚©‚Ë‚È‚¢‚Ì‚Å
        // “G‚É–³“GŠÔ‚ğì‚é‚©d•¡‚³‚¹‚È‚¢‚æ‚¤‚É‚·‚é
        IDamagable damagable = other.GetComponent<IDamagable>();

        if (damagable != null)
            damagable.AddDamage(10);
    }
}
